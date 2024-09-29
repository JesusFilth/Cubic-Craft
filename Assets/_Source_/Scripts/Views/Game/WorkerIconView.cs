using System;
using Source.Scripts.Characters.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.Game
{
    public class WorkerIconView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [SerializeField] private Sprite _build;
        [SerializeField] private Sprite _mining;

        private void OnEnable()
        {
            try
            {
                Validate();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
        }

        public void SetIcon(PlayerWorker playerWorker)
        {
            if (playerWorker is PlayerMiner)
                _icon.sprite = _mining;
            else if (playerWorker is PlayerBuilder)
                _icon.sprite = _build;
        }

        private void Validate()
        {
            if (_icon == null)
                throw new ArgumentNullException(nameof(_icon));

            if (_build == null)
                throw new ArgumentNullException(nameof(_build));

            if (_mining == null)
                throw new ArgumentNullException(nameof(_mining));
        }
    }
}
