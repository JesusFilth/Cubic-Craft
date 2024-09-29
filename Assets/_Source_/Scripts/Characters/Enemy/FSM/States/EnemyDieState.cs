using System.Collections;
using Source.Scripts.Animations;
using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.States
{
    [RequireComponent(typeof(CharacterAnimation))]
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(EnemyComponentHide))]
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyDieState : EnemyState
    {
        private const float RootingTime = 10f;

        [SerializeField] private bool _isPool = true;

        private CharacterAnimation _animation;
        private EnemyComponentHide _hideComponent;
        private Transform _transform;
        private EnemyMovement _movement;
        private Coroutine _rotting;

        private void Awake()
        {
            _animation = GetComponent<CharacterAnimation>();
            _transform = GetComponent<Transform>();
            _hideComponent = GetComponent<EnemyComponentHide>();
            _movement = GetComponent<EnemyMovement>();
        }

        private void OnEnable()
        {
            _hideComponent.Off();
            _animation.ToDie();

            _movement.SetTarget(null);

            if (_rotting == null)
                _rotting = StartCoroutine(Rooting());
        }

        private void OnDisable()
        {
            if (_rotting != null)
            {
                StopCoroutine(_rotting);
                _rotting = null;
            }
        }

        private IEnumerator Rooting()
        {
            const float RootindDawnY = 2;

            Vector3 originPosition = _transform.position;
            Vector3 targetPosition = new Vector3(_transform.position.x, _transform.position.y - RootindDawnY, _transform.position.z);
            float elapsedTime = 0;

            while (elapsedTime < RootingTime)
            {
                float progress = elapsedTime / RootingTime;
                _transform.position = Vector3.Lerp(originPosition, targetPosition, progress);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _rotting = null;

            if (_isPool)
                gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
    }
}
