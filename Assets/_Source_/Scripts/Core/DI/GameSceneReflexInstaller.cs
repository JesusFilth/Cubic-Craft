using Source.Scripts.Characters;
using Source.Scripts.Characters.Player;
using Source.Scripts.Core.GameSession;
using Source.Scripts.Core.Localization;
using Source.Scripts.Core.Pools;
using Source.Scripts.Core.Spawners;
using Source.Scripts.Enviroment.Mineral;
using Source.Scripts.Enviroment.Skills;
using Source.Scripts.Enviroment.Tample;
using Source.Scripts.Enviroment.Truck;
using Source.Scripts.Input;
using Source.Scripts.Sounds;
using Source.Scripts.Views;
using Source.Scripts.Views.Game;
using Source.Scripts.Views.Game.InterfaceStateMashine;
using Agava.WebUtility;
using Reflex.Core;
using UnityEngine;

namespace Source.Scripts.Core.DI
{
    public class GameSceneReflexInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Truck _truck;
        [SerializeField] private PlayerMeleeAttack _playerMeleeAttack;

        [SerializeField] private MobileInput _mobileInput;
        [SerializeField] private WorkerView _workerViewDesktop;
        [SerializeField] private WorkerView _workerViewMobale;
        [SerializeField] private TampleProgressView _templeProgressView;
        [SerializeField] private UIStateMashine _gameLevelUI;

        [SerializeField] private SpawnEnemyPool _enemyPool;
        [SerializeField] private SpawnItemPool _itemPool;
        [SerializeField] private MineralCubeViewPool _mineralCubeViewPool;
        [SerializeField] private EnemyRisingParticlePool _enemyRisingParticlePool;
        [SerializeField] private MiningParticlePool _miningParticlePool;

        [SerializeField] private GameLevelSounds _sounds;

        [SerializeField] private OnGameLevelLoaded _gameLevelLoaded;
        [SerializeField] private SkillStorage _skillStorage;

        [SerializeField] private GameLevelConteinerDI _gameLevelConteinerDI;

        [SerializeField] private MessageBox _messageBox;
        [SerializeField] private VictoryCamera _victoryCamera;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_victoryCamera);
            containerBuilder.AddSingleton(_gameLevelConteinerDI);

            InstallInput(containerBuilder);
            InstallWorkerView(containerBuilder);

            containerBuilder.AddSingleton(
                _gameLevelLoaded,
                typeof(ILevelSpawnItemSetting),
                typeof(ILevelMineralOreSetting),
                typeof(ILevelEnemySettings),
                typeof(ILevelTempleSetting),
                typeof(ICurrentLevelInfo),
                typeof(ILevelSkillSetting),
                typeof(ILevelBombSettings));

            containerBuilder.AddSingleton(_gameLevelUI);

            containerBuilder.AddSingleton(
                _sounds,
                typeof(IItemSounds),
                typeof(IFinalGameSounds),
                typeof(IFlooLavaSound),
                typeof(IMineralWorkSounds),
                typeof(ITempleBuildSounds));

            containerBuilder.AddSingleton(_player, typeof(IWallet), typeof(IPlayerPosition), typeof(IPlayerStats));
            containerBuilder.AddSingleton(_truck, typeof(ITruckIsFull), typeof(ITruckRemoveAllMinerals), typeof(IMineralView));
            containerBuilder.AddSingleton(_playerMeleeAttack, typeof(IPlayerNearEnemys));
            containerBuilder.AddSingleton(_templeProgressView, typeof(ITempleProgressView));

            containerBuilder.AddSingleton(_enemyPool);
            containerBuilder.AddSingleton(_itemPool);
            containerBuilder.AddSingleton(_mineralCubeViewPool);
            containerBuilder.AddSingleton(_enemyRisingParticlePool);
            containerBuilder.AddSingleton(_miningParticlePool);

            containerBuilder.AddSingleton(_skillStorage);

            containerBuilder.AddSingleton(new LocalizationTranslate());
            containerBuilder.AddSingleton(_messageBox);
        }

        private void InstallInput(ContainerBuilder containerBuilder)
        {
            if (Device.IsMobile)
                containerBuilder.AddSingleton(_mobileInput, typeof(IPlayerInput));
            else
                containerBuilder.AddSingleton(typeof(KeyboardInput), typeof(IPlayerInput));
        }

        private void InstallWorkerView(ContainerBuilder containerBuilder)
        {
            if (Device.IsMobile)
                containerBuilder.AddSingleton(_workerViewMobale, typeof(IWorkerView));
            else
                containerBuilder.AddSingleton(_workerViewDesktop, typeof(IWorkerView));
        }
    }
}
