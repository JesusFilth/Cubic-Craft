using UnityEngine;

namespace Source.Scripts.Characters.Player
{
    public interface IPlayerPosition
    {
        void SetPosition(Transform point);

        Transform GetPosition();
    }
}