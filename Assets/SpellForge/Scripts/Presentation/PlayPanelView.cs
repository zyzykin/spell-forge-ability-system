using System.Collections.Generic;
using SpellForge.Scripts.AbilitySystem;
using UnityEngine;

namespace SpellForge.Scripts.Presentation
{
    public class PlayPanelView : MonoBehaviour
    {
        [SerializeField] private List<SpellContainer> spellContainers;
        
        private void OnEnable()
        {
            Components.AbilitySystem abilitySystem = FindObjectOfType<Components.AbilitySystem>();
            Init(abilitySystem.Abilities);
        }
        
        public void Init(List<Ability> abilities)
        {
            if (abilities == null)
            {
                Debug.LogWarning("Abilities list is null in PlayPanelView.Init");
                return;
            }

            if (spellContainers == null || spellContainers.Count == 0)
            {
                Debug.LogWarning("SpellContainers list is null or empty in PlayPanelView.Init");
                return;
            }

            var containerCount = spellContainers.Count;
            var abilityCount = abilities.Count;

            for (int i = 0; i < containerCount && i < abilityCount; i++)
            {
                if (spellContainers[i] != null)
                {
                    spellContainers[i].Init(abilities[i]);
                }
                else
                {
                    Debug.LogWarning($"SpellContainer at index {i} is null in PlayPanelView.Init");
                }
            }
        }
    }
}