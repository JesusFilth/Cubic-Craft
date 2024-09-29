using Source.Scripts.Characters;
using Source.Scripts.Characters.Player;
using UnityEngine;

namespace Source.Scripts.Enviroment.Items
{
    public class CoinItem : Item
    {
        [Space][SerializeField] private int _count;

        protected override void Use(Player player)
        {
            if (player is IWallet wallet)
            {
                wallet.AddCoin(_count);
            }
        }
    }
}