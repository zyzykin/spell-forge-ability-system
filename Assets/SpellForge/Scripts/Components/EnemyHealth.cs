using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SpellForge.Scripts.Components
{
    public class EnemyHealth : MonoBehaviour
    {
        public event Action DiedEvent;

        [SerializeField] private Slider healthSlider;
        [SerializeField] private float maxHealth = 100f;

        private float _currentHealth;
        private Tween _takeDamageTween;

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
                OnDiedEvent();
            }
            else
            {
                DoTakeDamageAnimation();
            }
        }

        protected virtual void OnDiedEvent()
        {
            DiedEvent?.Invoke();
        }

        private void DoTakeDamageAnimation()
        {
            if (_takeDamageTween != null)
            {
                _takeDamageTween.Kill();
            }

            _takeDamageTween = transform.DOPunchScale(Vector3.one * .5f, .5f);
        }
    }
}