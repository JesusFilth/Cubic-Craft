using UnityEngine;

namespace Source.Scripts.Enviroment.Items
{
    public class ObjectRotate : MonoBehaviour
    {
        [SerializeField] private float _speed = 50f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Rotate(Vector3.up, _speed * Time.deltaTime);
        }
    }
}
