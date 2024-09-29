using Source.Scripts.Characters.Enemy.FSM;
using Source.Scripts.Core;
using Source.Scripts.Core.Spawners;
using UnityEngine;

namespace Source.Scripts.Characters.Enemy
{
    [RequireComponent(typeof(EnemyStateMashine))]
    public class Enemy : MonoBehaviour, ISpawnObject
    {
        private Transform _transform;
        private EnemyStateMashine _stateMashine;

        private void Awake()
        {
            _transform = transform;
            _stateMashine = GetComponent<EnemyStateMashine>();

            GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
        }

        public void Init(Transform point)
        {
            _transform.position = point.position;
            _stateMashine.ResetToFirstState();
        }
    }
}
