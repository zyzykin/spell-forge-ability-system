using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SpellForge.Scripts.AbilitySystem.Consequences
{
    public abstract class Consequence : ScriptableObject
    {
        public UnityEvent<GameObject, AbilityContext> OnExecute;

        protected const string EnemyLayer = "Enemy";

        public virtual async UniTask Execute(GameObject user, AbilityContext context)
        {
            OnExecute.Invoke(user, context);
            await UniTask.CompletedTask;
        }
    }
    
    public class AbilityContext
    {
        public Vector3 Center;
        public GameObject[] Targets;
        public GameObject CurrentTarget;
        private HashSet<float> triggeredTimes;
        public AbilityContext(Vector3 center)
        {
            Center = center;
            Targets = Array.Empty<GameObject>();
            CurrentTarget = null;
            triggeredTimes = new HashSet<float>();
        }
        public bool HasTriggered(float normalizedTime)
        {
            return triggeredTimes.Contains(normalizedTime);
        }
        public void MarkTriggered(float normalizedTime)
        {
            triggeredTimes.Add(normalizedTime);
        }
    }
}