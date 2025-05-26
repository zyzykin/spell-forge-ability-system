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
        public List<AbilityPhase> Phases;
        
        private float _lastUsedTime;
        private bool _isExecuting;

        public float CooldownProgress
        {
            get
            {
                if (CanExecute()) 
                    return 1f; // Ability is ready
                
                if (_lastUsedTime <= 0f) 
                    return 1f; // Ability has never been used
                
                var elapsed = Time.time - _lastUsedTime;
                return Mathf.Clamp01(elapsed / Cooldown); // Normalize between 0 and 1
            }
        }

        private void OnEnable()
        {
            _lastUsedTime = 0f;
            _isExecuting = false;
        }

        private bool CanExecute()
        {
            return Time.time >= _lastUsedTime + Cooldown && !_isExecuting;
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
                if (phase == null)
                {
                    Debug.LogWarning($"Phase is null in ability {Id}");
                    continue;
                }

                try
                {
                    await phase.Execute(user, context); 
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error executing phase in ability {Id}: {ex.Message}");
                }
            }

            _isExecuting = false;
        }
    }
}