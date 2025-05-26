using UnityEngine;

namespace SpellForge.Scripts.Presentation
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float followSpeed = 5f;
        [SerializeField] private bool lookAtTarget = true;
        private Vector3 _offset;

        private void Awake()
        {
            if (target == null)
            {
                return;
            }

            _offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            var targetPosition = target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            if (lookAtTarget)
            {
                transform.LookAt(target);
            }
        }
    }
}