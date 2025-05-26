using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpellForge.Scripts.AbilitySystem.Consequences
{
    [CreateAssetMenu(fileName = "RectOverlap", menuName = "AbilitySystem/Consequences/RectOverlap")]
    public class RectOverlap : Consequence
    {
        public Stat Width;
        public Stat Height;
        public Stat Depth;
        public Consequence ChainedConsequence;

        public override async UniTask Execute(GameObject user, AbilityContext context)
        {
            var center = context.Center;
            var halfExtents = new Vector3(Width.Value / 2f, Height.Value / 2f, Depth.Value / 2f);

            var colliders = Physics.OverlapBox(center, halfExtents, Quaternion.identity, LayerMask.GetMask(EnemyLayer));
            context.Targets = colliders.Select(c => c.gameObject).ToArray();

            if (ChainedConsequence != null)
            {
                Debug.Log($"RectOverlap {name}: Processing {context.Targets.Length} targets with ChainedConsequence {ChainedConsequence.name}");
                foreach (var target in context.Targets)
                {
                    context.CurrentTarget = target;
                    await ChainedConsequence.Execute(user, context);
                    Debug.Log($"RectOverlap {name}: Finished processing target {target.name}");
                }
            }
            else
            {
                Debug.Log($"RectOverlap {name}: No ChainedConsequence defined");
            }

            await base.Execute(user, context);
            Debug.Log($"RectOverlap {name}: Execute completed for user {user.name}");
        }
    }
}