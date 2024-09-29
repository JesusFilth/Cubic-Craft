using UnityEngine;

namespace Source.Scripts.Input
{
    public class KeyboardInput : IPlayerInput
    {
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";
        private const KeyCode _jump = KeyCode.Space;
        private const KeyCode _attack = KeyCode.Mouse1;

        public Vector3 GetDirection()
        {
            float horizontal = UnityEngine.Input.GetAxis(Horizontal);
            float vertical = UnityEngine.Input.GetAxis(Vertical);

            return new Vector3(horizontal, 0f, vertical);
        }

        public bool IsAttack()
        {
            return UnityEngine.Input.GetKey(_attack);
        }

        public bool IsJump()
        {
            return UnityEngine.Input.GetKey(_jump);
        }
    }
}
