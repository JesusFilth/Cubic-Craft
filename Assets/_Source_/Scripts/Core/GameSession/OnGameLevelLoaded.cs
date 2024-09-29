using Source.Scripts.Characters.Player;
using Source.Scripts.Core.Spawners;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.User;
using Source.Scripts.Enviroment.Level;
using IJunior.TypedScenes;
using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Core.GameSession
{
    public class OnGameLevelLoaded : MonoBehaviour, ISceneLoadHandler<LevelMode>,
        ILevelSpawnItemSetting,
        ILevelMineralOreSetting,
        ILevelEnemySettings,
        ILevelTempleSetting,
        ICurrentLevelInfo,
        ILevelSkillSetting,
        ILevelBombSettings
    {
        private LevelMode _currentLevelMode;
        private LevelMode _endGameMode;
        private int _endGameUpValue;

        [Inject] private IPlayerPosition _player;

        [Inject] private SpawnEnemyPool _spawnEnemyPool;
        [Inject] private SpawnItemPool _spawnItemPool;

        [Inject] private GameLevelConteinerDI _levelConteinerDI;

        [Inject] private ILevelStorage _levelStorage;
        [Inject] private IFindLevel _findLevel;

        private bool IsEndGame => _endGameMode != null;

        public void OnSceneLoaded(LevelMode levelMode)
        {
            _levelConteinerDI.InitHot();
            _currentLevelMode = levelMode;

            InitEndGameLevel();
            Initialize();
        }

        private void Initialize()
        {
            LevelModeInit level = Instantiate(_currentLevelMode.Map);

            _spawnEnemyPool.Init(_currentLevelMode.Enemys, _currentLevelMode.EnemyCapasityPool);
            _spawnItemPool.Init(_currentLevelMode.Items, _currentLevelMode.ItemCapasityPool);

            level.Init(_currentLevelMode.Type);
            _player.SetPosition(level.StartPoint);
        }

        private void InitEndGameLevel()
        {
            if (_levelStorage.TryGetUpValueProperty(_currentLevelMode.CurrentLevelIndex, out int value))
            {
                _endGameMode = _findLevel.GetEndGameLevelMode(_currentLevelMode.Type);
                _endGameUpValue = value;
            }
        }

        public int GetCount() => _currentLevelMode.ItemSpawnCount;

        public float GetOreMaxProgress()
        {
            if (IsEndGame)
                return _endGameMode.OreMaxProgress + _endGameUpValue;

            return _currentLevelMode.OreMaxProgress;
        }

        public float GetOreForceResistance()
        {
            if (IsEndGame)
                return GetEndGameUpValuePercent(_endGameMode.OreForceResistance);

            return _currentLevelMode.OreForceResistance;
        }

        public float GetBuildMaxProgress()
        {
            if (IsEndGame)
                return _endGameMode.BuildMaxProgress + _endGameUpValue;

            return _currentLevelMode.BuildMaxProgress;
        }

        public float GetBuildForceResistance()
        {
            if (IsEndGame)
                return GetEndGameUpValuePercent(_endGameMode.BuildForceResistance);

            return _currentLevelMode.BuildForceResistance;
        }

        public int GetPrice()
        {
            if (IsEndGame)
                return GetEndGameUpValueGold(_endGameMode.GoldPrise);

            return _currentLevelMode.GoldPrise;
        }

        public float GetImproveStats()
        {
            if (IsEndGame)
                return _endGameMode.ImproveEnemyStats + _endGameUpValue;

            return _currentLevelMode.ImproveEnemyStats;
        }

        public float GetSpawnChance() => _currentLevelMode.EnemyChanceSpawn;

        public float GetSpawnDelay() => _currentLevelMode.EnemyDelaySpawn;

        public int GetLevelNumber() => _currentLevelMode.CurrentLevelIndex + 1;

        public LevelTypeMode GetLevelType() => _currentLevelMode.Type;

        public int GetMaxValue() => _currentLevelMode.MaxValueSkill;

        public float GetDelay() => _currentLevelMode.BombDelay;

        public int GetChance() => _currentLevelMode.BombChance;

        public float GetMaerkerDelay() => _currentLevelMode.BombMarkerDelay;

        private float GetEndGameUpValuePercent(float value)
        {
            const float PercentUp = 10;

            return value + (_endGameUpValue / PercentUp);
        }

        private int GetEndGameUpValueGold(int value)
        {
            const int PercentUp = 10;
            const int MaxPercent = 100;

            int upPercent = _endGameUpValue * PercentUp;
            return value + (value * upPercent / MaxPercent);
        }
    }
}