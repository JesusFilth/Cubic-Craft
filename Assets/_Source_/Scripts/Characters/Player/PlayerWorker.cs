using Source.Scripts.Animations;
using Source.Scripts.Enviroment.Truck;
using UnityEngine;

namespace Source.Scripts.Characters.Player
{
    public abstract class PlayerWorker : MonoBehaviour
    {
        [SerializeField] protected Truck Truck;
        [SerializeField] protected CharacterAnimation Animation;
        [SerializeField] protected PlayerStats Stats;

        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }

        public abstract float GetWorkPower();
    }
}
