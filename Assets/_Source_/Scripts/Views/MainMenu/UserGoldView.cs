using Reflex.Attributes;
using Source.Scripts.Core.Storage.User;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Views.MainMenu
{
    public class UserGoldView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gold;

        [Inject] private IGoldStorage _storage;

        private void OnEnable()
        {
            _storage.GoldChanged += UpdateValue;
        }

        private void OnDisable()
        {
            _storage.GoldChanged -= UpdateValue;
        }

        private void Start()
        {
            UpdateValue(_storage.GetGold());
        }

        private void UpdateValue(int value)
        {
            _gold.text = value.ToString();
        }
    }
}
