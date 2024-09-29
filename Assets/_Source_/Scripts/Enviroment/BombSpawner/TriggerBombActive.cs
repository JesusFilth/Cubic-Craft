using Source.Scripts.Characters.Player;
using UnityEngine;

namespace Source.Scripts.Enviroment.BombSpawner
{
    public class TriggerBombActive : MonoBehaviour
    {
        [SerializeField] private BombSpawner _bombSpawner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _bombSpawner.Create();
            }
        }
    }
}
