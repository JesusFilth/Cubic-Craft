using Reflex.Attributes;
using Source.Scripts.Characters.Player;
using Source.Scripts.Views.Game.InterfaceStateMashine;

namespace Source.Scripts.Enviroment.Items
{
    public class SkillItem : Item
    {
        [Inject] private UIStateMashine _gameUI;

        protected override void Use(Player player)
        {
            _gameUI.EnterIn<SkillsUIState>();
        }
    }
}