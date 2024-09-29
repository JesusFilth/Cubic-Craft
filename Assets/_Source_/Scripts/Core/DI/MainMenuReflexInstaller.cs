using Reflex.Core;
using Source.Scripts.Core.Localization;
using Source.Scripts.Views;
using UnityEngine;

namespace Source.Scripts.Core.DI
{
    public class MainMenuReflexInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MessageBox _messageBox;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(new LocalizationTranslate());
            containerBuilder.AddSingleton(_messageBox);
        }
    }
}
