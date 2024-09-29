using UnityEngine;

namespace Source.Scripts.Views.Game
{
    public class FallowerWorldUI : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _target;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.position = new Vector3(
                _target.position.x + _offset.x,
                _target.position.y + _offset.y,
                _target.position.z + _offset.z);
        }
    }
}
