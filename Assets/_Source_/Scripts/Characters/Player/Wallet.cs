using UnityEngine;

namespace Source.Scripts.Characters.Player
{
    public class Wallet
    {
        public int Coin { get; private set; }

        public void AddCoin(int coin)
        {
            Coin = Mathf.Clamp(Coin + coin, 0, int.MaxValue);
        }
    }
}
