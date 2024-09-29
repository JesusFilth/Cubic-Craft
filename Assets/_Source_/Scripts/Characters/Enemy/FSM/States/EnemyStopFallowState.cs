using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.States
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyStopFallowState : EnemyState
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
    }
}
