using UnityEngine;

namespace Source.Scripts.Enviroment.Mineral
{
    public struct MineralMovementSettings
    {
        public MineralMovementSettings(Transform startPoint, Transform endPoint, MineralType type, IMineralCubeViewFinalPosition finalAction)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Type = type;
            FinalAction = finalAction;
        }

        public Transform StartPoint { get; private set; }
        public Transform EndPoint { get; private set; }
        public MineralType Type { get; private set; }

        public IMineralCubeViewFinalPosition FinalAction { get; private set; }
    }
}