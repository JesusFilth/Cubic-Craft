using Source.Scripts.Characters;
using Source.Scripts.Characters.Player;
using UnityEngine;

namespace Source.Scripts.Enviroment.Items
{
    public class HealthItem : Item
    {
        [Space][SerializeField] private float _health;

        protected override void Use(Player player)
        {
            if (player.TryGetComponent(out Stats stats))
            {
                stats.AddHealth(_health);
            }
        }
    }
}
