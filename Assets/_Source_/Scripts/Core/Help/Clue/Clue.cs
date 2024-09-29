using System;
using Source.Scripts.Characters.Player;
using Source.Scripts.Core.Help.Events;
using UnityEngine;

namespace Source.Scripts.Core.Help.Clue
{
    [RequireComponent(typeof(Collider))]
    public class Clue : MonoBehaviour
    {
        [SerializeField] private ClueView _clueView;
        [SerializeField] private HelpEvent _helpEvent;

        private void OnEnable()
        {
            try
            {
                Valildate();
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _clueView.Show(_helpEvent);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _clueView.Hide();
            }
        }

        private void Valildate()
        {
            if (_clueView == null)
                throw new ArgumentNullException(nameof(_clueView));

            if (_helpEvent == null)
                throw new ArgumentNullException(nameof(_helpEvent));
        }
    }
}
