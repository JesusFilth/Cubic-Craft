using Reflex.Attributes;
using Source.Scripts.Animations;
using Source.Scripts.Input;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.Characters.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterAnimation))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _gravity = 9.8f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _magnitude = 0.2f;

        [SerializeField] private float _jumpForce = 15f;

        public UnityEvent OnJump;

        private CharacterController _controller;
        private Vector3 _moveDirection = Vector3.zero;
        private Transform _transform;

        private bool _isWalk;
        private CharacterAnimation _moveAnimation;

        [Inject] private IPlayerInput _input;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _moveAnimation = GetComponent<CharacterAnimation>();
            _transform = transform;
        }

        private void Update()
        {
            SetMoveDirection();

            if (_controller.isGrounded)
            {
                _moveDirection.y = 0;

                if (_input.IsJump())
                {
                    Jump();
                }
            }

            Move();
            Rotate();
        }

        private void SetMoveDirection()
        {
            Vector3 direction = _input.GetDirection();
            CheckChangeWalk(direction.magnitude);

            _moveDirection = new Vector3(direction.normalized.x, _moveDirection.y, direction.normalized.z);
        }

        private void Jump()
        {
            _moveDirection.y = _jumpForce;
            _moveAnimation.ToJump();

            OnJump?.Invoke();
        }

        private void Move()
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
            _controller.Move(_moveDirection * _speed * Time.deltaTime);
        }

        private void Rotate()
        {
            if (_isWalk == false)
                return;

            float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
            _transform.localRotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        private void CheckChangeWalk(float magnitude)
        {
            bool isWalk = magnitude >= _magnitude;

            if (_isWalk == isWalk)
                return;

            _isWalk = isWalk;
            _moveAnimation.ToWalk(_isWalk);
        }
    }
}