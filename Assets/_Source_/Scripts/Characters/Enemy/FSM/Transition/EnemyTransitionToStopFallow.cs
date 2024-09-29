using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.Transition
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyTransitionToStopFallow : EnemyTransition
    {
        private EnemyMovement _movement;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovement>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                _movement.SetTarget(null);
                NeedTransit = true;
            }
        }
    }
}
