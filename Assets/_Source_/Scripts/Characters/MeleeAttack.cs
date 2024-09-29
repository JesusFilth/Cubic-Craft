using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.Characters
{
    [RequireComponent(typeof(Stats))]
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _dalay = 1f;
        [SerializeField] private float _sphereHitRadius = 0.4f;
        [SerializeField] private float _lenghtDirection = 1f;

        protected Transform Transform;

        private RaycastHit _hitRay;
        private Coroutine _dalaing;
        private WaitForSeconds _waitForSeconds;

        public UnityEvent OnAttack;

        protected LayerMask LayerMask => _layerMask;

        private void Awake()
        {
            Transform = transform;
            _waitForSeconds = new WaitForSeconds(_dalay);
        }

        private void OnDisable()
        {
            if (_dalaing != null)
            {
                StopCoroutine(_dalaing);
                _dalaing = null;
            }
        }

        public bool TryHit(float damage)
        {
            if (_dalaing != null)
                return false;

            if (TryGetTarget(out Stats target))
            {
                _dalaing = StartCoroutine(Delaing());

                OnAttack?.Invoke();

                Transform.LookAt(target.transform);
                target.TakeDamage(damage);

                return true;
            }

            return false;
        }

        protected virtual bool TryGetTarget(out Stats target)
        {
            if (TryGetCastSphereTarget(out target))
                return true;

            return false;
        }

        protected bool TryGetCastSphereTarget(out Stats target)
        {
            target = null;

            if (Physics.SphereCast(Transform.position, _sphereHitRadius, Transform.forward, out _hitRay, _lenghtDirection, LayerMask, QueryTriggerInteraction.Ignore))
            {
                if (_hitRay.collider.gameObject.TryGetComponent(out Stats stats))
                {
                    if (stats.IsDead() == false)
                    {
                        target = stats;
                        return true;
                    }
                }
            }

            return false;
        }

        private IEnumerator Delaing()
        {
            yield return _waitForSeconds;
            _dalaing = null;
        }
    }
}