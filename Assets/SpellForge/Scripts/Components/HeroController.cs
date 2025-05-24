using UnityEngine;

namespace SpellForge.Scripts.Components
{
    [RequireComponent(typeof(AbilitySystem))]
    public class HeroController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float minRotationSpeed = 1f;
        private AbilitySystem _abilitySystem;

        private void Awake()
        {
            _abilitySystem = GetComponent<AbilitySystem>();
        }

        private void Update()
        {
            // Movement
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveZ = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(moveX, 0f, moveZ).normalized;
            var movement = direction * moveSpeed * Time.deltaTime;
            transform.position += movement;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Update ability system
            _abilitySystem.CheckInput();
        }

    }
}