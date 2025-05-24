using UnityEngine;
using UnityEngine.UI;

namespace SpellForge.Scripts.Components
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            
            if (healthSlider != null)
            {
                healthSlider.value = _currentHealth / maxHealth;
            }
            
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}