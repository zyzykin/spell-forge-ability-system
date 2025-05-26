using System;
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

        public AbilityContext(Vector3 center)
        {
            Center = center;
            Targets = Array.Empty<GameObject>();
            CurrentTarget = null;
        }
    }
}