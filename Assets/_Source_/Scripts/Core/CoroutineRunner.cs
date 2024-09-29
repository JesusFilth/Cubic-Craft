using System.Collections;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class CoroutineRunner : MonoBehaviour
    {
        private IEnumerator _currentCoroutine;

        public static CoroutineRunner Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void OnDisable()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }

        public void Run(IEnumerator coroutine)
        {
            _currentCoroutine = coroutine;
            StartCoroutine(_currentCoroutine);
        }
    }
}
