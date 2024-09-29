using com.cyborgAssets.inspectorButtonPro;
using Reflex.Attributes;
using Source.Scripts.Views.Game.InterfaceStateMashine;
using UnityEngine;

namespace Source.Scripts.Enviroment
{
    public class VictotyScene : MonoBehaviour
    {
        [Inject] private UIStateMashine _stateMashine;

        [ProButton]
        public void Show()
        {
            _stateMashine.EnterIn<ComplededSceneUIState>();
        }
    }
}
