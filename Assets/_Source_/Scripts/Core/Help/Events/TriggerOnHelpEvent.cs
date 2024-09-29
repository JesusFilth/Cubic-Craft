using Source.Scripts.Characters.Player;
using UnityEngine;

namespace Source.Scripts.Core.Help.Events
{
    [RequireComponent(typeof(Collider))]
    public class TriggerOnHelpEvent : HelpEvent
    {
        private void OnTriggerEnter(Collider other)
        {
            if (IsShow == false)
                return;

            if (IsCurrentLevelMode() == false)
                return;

            if (other.TryGetComponent(out Player player))
            {
                IsShow = false;

                ShowHelpWindow();
            }
        }
    }
}
