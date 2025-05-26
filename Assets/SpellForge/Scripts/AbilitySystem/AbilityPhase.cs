using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SpellForge.Scripts.AbilitySystem.Consequences;
using UnityEngine;
using UnityEngine.Serialization;

namespace SpellForge.Scripts.AbilitySystem
{
    [Serializable]
    public class ConsequenceEntry
    {
        public float normalizedTime;
        public List<Consequence> consequences;
    }
    [CreateAssetMenu(fileName = "NewAbilityPhase", menuName = "AbilitySystem/AbilityPhase")]
    public class AbilityPhase : ScriptableObject
    {
        [FormerlySerializedAs("duration")] public Stat Duration;
        [FormerlySerializedAs("consequenceEntries")] [SerializeField] public List<ConsequenceEntry> ConsequenceEntries;
        public Dictionary<float, List<Consequence>> ConsequencesByTime;
        private void OnEnable()
        {
            ConsequencesByTime = new Dictionary<float, List<Consequence>>();
            foreach (var entry in ConsequenceEntries)
            {
                ConsequencesByTime[entry.normalizedTime] = entry.consequences;
            }
        }
        public async UniTask Execute(GameObject user, AbilityContext context)
        {
            var durationValue = Duration.Value;
            var elapsedTime = 0f;
            while (elapsedTime < durationValue)
            {
                var normalizedTime = elapsedTime / durationValue;
                foreach (var timeEntry in ConsequencesByTime)
                {
                    if (normalizedTime >= timeEntry.Key && !context.HasTriggered(timeEntry.Key))
                    {
                        foreach (var consequence in timeEntry.Value)
                        {
                            await consequence.Execute(user, context);
                        }
                        context.MarkTriggered(timeEntry.Key);
                    }
                }
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
        }
    }
}