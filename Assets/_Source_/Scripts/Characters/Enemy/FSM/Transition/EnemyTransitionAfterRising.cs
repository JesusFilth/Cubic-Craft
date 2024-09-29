using Source.Scripts.Characters.Enemy.FSM.States;
using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.Transition
{
    [RequireComponent(typeof(EnemyRisingState))]
    public class EnemyTransitionAfterRising : EnemyTransition
    {
        private EnemyRisingState _risingState;

        private void Awake()
        {
            _risingState = GetComponent<EnemyRisingState>();
        }

        private void Update()
        {
            if (_risingState.IsFinished)
                NeedTransit = true;
        }
    }
}
