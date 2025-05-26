using System;
using SpellForge.Scripts.Components;
using UnityEngine;

namespace SpellForge.Scripts.Presentation
{
    public class GameStateManager : MonoBehaviour
    {
        public static Action GameOverEvent;

        private int _alive;
        private EnemyHealth[] _enemies;

        private void Awake()
        {
            _enemies = FindObjectsOfType<EnemyHealth>();
            _alive = _enemies.Length;
        }

        private void OnEnable()
        {
            foreach (var e in _enemies)
                e.DiedEvent += OnGameOverEvent;
        }

        private void OnDisable()
        {
            foreach (var e in _enemies)
                e.DiedEvent -= OnGameOverEvent;
        }

        private void OnGameOverEvent()
        {
            if (--_alive > 0) return;
            GameOverEvent?.Invoke();
        }
    }
}