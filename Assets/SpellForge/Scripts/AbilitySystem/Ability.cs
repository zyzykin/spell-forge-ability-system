using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SpellForge.Scripts.AbilitySystem.Consequences;
using UnityEngine;

namespace SpellForge.Scripts.AbilitySystem
{
    [CreateAssetMenu(fileName = "Ability", menuName = "AbilitySystem/Ability")]
    public class Ability : ScriptableObject
    {
        public string Id;
        public KeyCode Key;
        public float Cooldown;
        public List<Phase> Phases;

        [Serializable]
        public class Phase
        {
            public float Duration = 1f;
            [Range(0f, 1f)]
            public float NormalizedTime = 0.5f; // 0 to 1
            public List<Consequence> Consequences;
        }
        
        public float CooldownProgress
        {
            get
            {
                if (CanExecute()) return 1f; // Ability is ready
                if (_lastUsedTime <= 0f) return 1f; // Ability has never been used
                float elapsed = Time.time - _lastUsedTime;
                return Mathf.Clamp01(elapsed / Cooldown); // Normalize between 0 and 1
            }
        }

        private float _lastUsedTime;
        private bool _isExecuting;

        private void OnEnable()
        {
            _lastUsedTime = 0f;
            _isExecuting = false;
        }

        public bool CanExecute()
        {
            var canExecute = Time.time >= _lastUsedTime + Cooldown && !_isExecuting;
            Debug.Log($"Ability {Id}: CanExecute = {canExecute}, Time = {Time.time}, LastUsed = {_lastUsedTime}, Cooldown = {Cooldown}, IsExecuting = {_isExecuting}");
            return canExecute;
        }

        public async UniTask Execute(GameObject user)
        {
            if (!CanExecute())
            {
                return;
            }

            _isExecuting = true;
            _lastUsedTime = Time.time; 

            var context = new AbilityContext(user.transform.position);

            foreach (var phase in Phases)
            {
                var elapsed = 0f;
                var consequencesTriggered = false;

                while (elapsed < phase.Duration)
                {
                    elapsed += Time.deltaTime;
                    var normalizedTime = elapsed / phase.Duration;

                    if (!consequencesTriggered && normalizedTime >= phase.NormalizedTime)
                    {
                        consequencesTriggered = true;
                        foreach (var consequence in phase.Consequences)
                        {
                            await consequence.Execute(user, context);
                        }
                    }

                    await UniTask.Yield();
                }
            }

            _isExecuting = false;
        }
    }
}