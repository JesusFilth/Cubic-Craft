using Reflex.Attributes;
using Source.Scripts.Enviroment.Truck;

namespace Source.Scripts.Core.Help.Events
{
    public class TruckFullHelpEvent : HelpEvent
    {
        [Inject] private ITruckIsFull _truck;

        private void OnEnable()
        {
            _truck.Fulled += Full;
        }

        private void OnDisable()
        {
            _truck.Fulled -= Full;
        }

        private void Full()
        {
            if (IsShow == false)
                return;

            if (IsCurrentLevelMode() == false)
                return;

            IsShow = false;

            ShowHelpWindow();
        }
    }
}