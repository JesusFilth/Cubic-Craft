using System.Collections;
using Source.Scripts.Animations;
using UnityEngine;

namespace Source.Scripts.Characters.Enemy.FSM.States
{
    [RequireComponent(typeof(MeleeAttack))]
    [RequireComponent(typeof(Stats))]
    [RequireComponent(typeof(CharacterAnimation))]
    public class EmemyAttackState : EnemyState
    {
        private const float AttackStartDelay = 0.25f;

        private MeleeAttack _meleeAttack;
        private Stats _stats;
        private CharacterAnimation _animation;

        private Coroutine _attacking;
        private WaitForSeconds _waitForSeconds = new WaitForSeconds(AttackStartDelay);

        private void Awake()
        {
            _meleeAttack = GetComponent<MeleeAttack>();
            _stats = GetComponent<Stats>();
            _animation = GetComponent<CharacterAnimation>();
        }

        private void OnEnable()
        {
            if (_attacking == null)
                _attacking = StartCoroutine(Attacking());
        }

        private void OnDisable()
        {
            if (_attacking != null)
            {
                StopCoroutine(_attacking);
                _attacking = null;
            }
        }

        private IEnumerator Attacking()
        {
            yield return _waitForSeconds;

            while (enabled)
            {
                if (_meleeAttack.TryHit(_stats.Damage))
                {
                    _animation.ToAttack();
                }

                yield return null;
            }

            _attacking = null;
        }
    }
}
