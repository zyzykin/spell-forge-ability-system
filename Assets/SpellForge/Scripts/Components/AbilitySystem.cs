using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SpellForge.Scripts.AbilitySystem;
using UnityEngine;

namespace SpellForge.Scripts.Components
{
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField] private List<Ability> abilities;

        public List<Ability> Abilities => abilities;

        public void CheckInput()
        {
            foreach (var ability in Abilities)
            {
                if (Input.GetKeyDown(ability.Key))
                {
                    ExecuteAbility(ability).Forget();
                }
            }
        }

        private async UniTask ExecuteAbility(Ability ability)
        {
            await ability.Execute(gameObject);
        }
    }
}