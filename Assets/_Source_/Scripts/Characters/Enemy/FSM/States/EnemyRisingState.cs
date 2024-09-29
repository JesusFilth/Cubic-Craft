using System.Collections;
using Source.Scripts.Animations;
using Source.Scripts.Core.Pools;
using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.States
{
    [RequireComponent(typeof(EnemyStats))]
    [RequireComponent(typeof(EnemyComponentHide))]
    [RequireComponent(typeof(CharacterAnimation))]
    public class EnemyRisingState : EnemyState
    {
        private const float RisingTime = 3f;

        private Coroutine _rising;
        private Transform _transform;
        private EnemyStats _stats;
        private CharacterAnimation _animations;
        private EnemyComponentHide _hideComponent;

        [Inject] private EnemyRisingParticlePool _particlePool;

        public bool IsFinished { get; private set; }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _stats = GetComponent<EnemyStats>();
            _hideComponent = GetComponent<EnemyComponentHide>();
            _animations = GetComponent<CharacterAnimation>();
        }

        private void OnEnable()
        {
            IsFinished = false;
            _stats.ResetToDefault();
            _animations.ToIdel();
            _hideComponent.Off();

            if (_rising == null)
                _rising = StartCoroutine(Rising());
        }

        private void OnDisable()
        {
            if (_rising != null)
            {
                StopCoroutine(_rising);
                _rising = null;
            }
        }

        private IEnumerator Rising()
        {
            PoolObject particle = _particlePool.Create(_transform);

            Vector3 originPosition = _transform.position;
            Vector3 startPosition = new Vector3(_transform.position.x, -_transform.position.y, _transform.position.z);
            _transform.position = startPosition;

            float elapsedTime = 0;

            while (elapsedTime < RisingTime)
            {
                float progress = elapsedTime / RisingTime;
                _transform.position = Vector3.Lerp(startPosition, originPosition, progress);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _particlePool.Release(particle);
            _transform.position = originPosition;

            IsFinished = true;
            _hideComponent.On();

            _rising = null;
        }
    }
}
