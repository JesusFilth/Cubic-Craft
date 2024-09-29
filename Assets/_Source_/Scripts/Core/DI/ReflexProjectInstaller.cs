using Reflex.Core;
using Source.Scripts.Core.StateMashine;
using Source.Scripts.Core.Storage;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.User;
using UnityEngine;

namespace Source.Scripts.Core.DI
{
    public class ReflexProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private LevelStorage _gameLevelStorage;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(
                _gameLevelStorage,
                typeof(IFindLevel),
                typeof(ILevelInfo),
                typeof(IDefaultUser));

            containerBuilder.AddSingleton(
                new UserStorage(),
                typeof(IStateStorage),
                typeof(IGoldStorage),
                typeof(ILevelStorage),
                typeof(IUserStorage));

            containerBuilder.AddSingleton(new GameStateMashine());
        }
    }
}