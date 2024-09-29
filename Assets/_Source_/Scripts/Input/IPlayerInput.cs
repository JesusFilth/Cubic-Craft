using UnityEngine;

namespace Source.Scripts.Input
{
    public interface IPlayerInput
    {
        Vector3 GetDirection();

        bool IsJump();

        bool IsAttack();
    }
}
