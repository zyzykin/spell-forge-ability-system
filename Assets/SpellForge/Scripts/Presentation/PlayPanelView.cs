using System.Collections.Generic;
using SpellForge.Scripts.AbilitySystem;
using UnityEngine;

namespace SpellForge.Scripts.Presentation
{
    public class PlayPanelView : MonoBehaviour
    {
        [SerializeField] private List<AbilityContainer> abilityContainers;

        private void OnEnable()
        {
            Components.AbilitySystem abilitySystem = FindObjectOfType<Components.AbilitySystem>();
            Init(abilitySystem.Abilities);
        }

        private void Init(List<Ability> abilities)
        {
            if (abilities == null)
            {
                Debug.LogWarning("Abilities list is null in PlayPanelView.Init");
                return;
            }

            if (abilityContainers == null || abilityContainers.Count == 0)
            {
                Debug.LogWarning("SpellContainers list is null or empty in PlayPanelView.Init");
                return;
            }

            var containerCount = abilityContainers.Count;
            var abilityCount = abilities.Count;

            for (var i = 0; i < containerCount && i < abilityCount; i++)
            {
                if (abilityContainers[i] != null)
                {
                    abilityContainers[i].Init(abilities[i]);
                }
                else
                {
                    Debug.LogWarning($"SpellContainer at index {i} is null in PlayPanelView.Init");
                }
            }
        }
    }
}