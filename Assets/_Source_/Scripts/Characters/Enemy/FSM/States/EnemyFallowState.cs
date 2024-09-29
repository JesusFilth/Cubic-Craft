using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.States
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyFallowState : EnemyState
    {
        private EnemyMovement _movement;

        private void Awake()
        {
            _movement = GetComponent<EnemyMovement>();
        }

        private void Update()
        {
            _movement.StartFallowTarget();
        }
    }
}
