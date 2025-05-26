using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpellForge.Scripts.AbilitySystem.Consequences
{
    [CreateAssetMenu(fileName = "SphereOverlap", menuName = "AbilitySystem/Consequences/SphereOverlap")]
    public class SphereOverlap : Consequence
    {
        public float Radius = 5f;
        public Consequence ChainedConsequence;

        public override async UniTask Execute(GameObject user, AbilityContext context)
        {
            var center = context.Center;

            var colliders = Physics.OverlapSphere(center, Radius, LayerMask.GetMask(EnemyLayer));
            context.Targets = colliders.Select(c => c.gameObject).ToArray();

            if (ChainedConsequence != null)
            {
                Debug.Log($"SphereOverlap {name}: Processing {context.Targets.Length} targets with ChainedConsequence {ChainedConsequence.name}");
                foreach (var target in context.Targets)
                {
                    context.CurrentTarget = target;
                    await ChainedConsequence.Execute(user, context);
                    Debug.Log($"SphereOverlap {name}: Finished processing target {target.name}");
                }
            }
            else
            {
                Debug.Log($"SphereOverlap {name}: No ChainedConsequence defined");
            }

            await base.Execute(user, context);
            Debug.Log($"SphereOverlap {name}: Execute completed for user {user.name}");
        }
    }
}