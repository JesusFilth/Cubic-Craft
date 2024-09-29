using System;
using Reflex.Attributes;
using Source.Scripts.Characters;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Views.Game
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;

        [Inject] private IWallet _wallet;

        private void OnEnable()
        {
            _wallet.CoinChanged += ChangeValue;
        }

        private void OnDisable()
        {
            _wallet.CoinChanged -= ChangeValue;
        }

        private void OnValidate()
        {
            if (_value == null)
                throw new ArgumentNullException(nameof(_value));
        }

        private void ChangeValue(int value)
        {
            _value.text = value.ToString();
        }
    }
}
