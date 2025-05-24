using Cysharp.Threading.Tasks;
using SpellForge.Scripts.Components;
using UnityEngine;

namespace SpellForge.Scripts.AbilitySystem.Consequences
{
    [CreateAssetMenu(fileName = "DealDamage", menuName = "AbilitySystem/Consequences/DealDamage")]
    public class DealDamage : Consequence
    {
        public float Damage = 20f;

        public override async UniTask Execute(GameObject user, AbilityContext context)
        {
            if (context.CurrentTarget != null)
            {
                var health = context.CurrentTarget.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    Debug.Log($"DealDamage {name}: Applying {Damage} damage to {context.CurrentTarget.name}");
                    health.TakeDamage(Damage);
                }
                else
                {
                    Debug.LogWarning($"DealDamage {name}: No EnemyHealth component found on {context.CurrentTarget.name}");
                }
            }
            else
            {
                Debug.LogWarning($"DealDamage {name}: No target specified in context");
            }

            await base.Execute(user, context);
            Debug.Log($"DealDamage {name}: Execute completed for user {user.name}");
        }
    }
}