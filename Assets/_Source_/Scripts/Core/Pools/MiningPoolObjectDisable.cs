using System.Collections;
using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Core.Pools
{
    public class MiningPoolObjectDisable : MonoBehaviour
    {
        [SerializeField] private float _delay = 1;
        [SerializeField] private PoolObject _object;

        private Coroutine _waiting;
        private WaitForSeconds _waitForSeconds;

        [Inject] private MiningParticlePool _pool;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_delay);
            GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
        }

        private void OnEnable()
        {
            if (_waiting == null)
                _waiting = StartCoroutine(Waiting());
        }

        private void OnDisable()
        {
            if (_waiting != null)
            {
                StopCoroutine(_waiting);
                _waiting = null;
            }
        }

        private IEnumerator Waiting()
        {
            yield return _waitForSeconds;

            _pool.Release(_object);
        }
    }
}
