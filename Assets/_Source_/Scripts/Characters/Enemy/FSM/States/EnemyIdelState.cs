using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.States
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyIdelState : EnemyState
    {
        private EnemyMovement _movement;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovement>();
        }

        private void OnEnable()
        {
            _movement.StopFallowTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _movement.SetTarget(player.transform);
            }
        }
    }
}
