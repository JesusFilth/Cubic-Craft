using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.Transition
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyTransitionToAttack : EnemyTransition
    {
        [SerializeField] private float _minDistance = 1.0f;

        private EnemyMovement _movement;
        private Transform _transform;

        public float Distance => _minDistance;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovement>();
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (_movement.PlayerTarget == null)
                return;

            if (Vector3.Distance(_transform.position, _movement.PlayerTarget.position) <= _minDistance)
            {
                NeedTransit = true;
            }
        }
    }
}
