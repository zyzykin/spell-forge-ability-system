using SpellForge.Scripts.AbilitySystem;
using UnityEngine;
using UnityEngine.UI;

namespace SpellForge.Scripts.Presentation
{
    public class AbilityContainer : MonoBehaviour
    {
        [SerializeField] private Image filledImage;

        private Ability _ability;

        public void Init(Ability ability)
        {
            _ability = ability;
        }

        private void Update()
        {
            if (!Mathf.Approximately(filledImage.fillAmount, 1f - _ability.CooldownProgress))
            {
                filledImage.fillAmount = 1f - _ability.CooldownProgress;
            }
        }
    }
}