using System;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.Models;
using Source.Scripts.Core.Storage.User;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Core.Storage
{
    public class LevelStorage : MonoBehaviour, IFindLevel, ILevelInfo, IDefaultUser
    {
        [SerializeField] private DefaultUserSettings _userSettings;
        [SerializeField] private LevelSetting[] _levels;

        public LevelSetting[] Levels => _levels;

        public bool TryGetLevel(int index, LevelTypeMode mode, out LevelMode level)
        {
            if (_levels[index].IsEndGame == false)
            {
                level = _levels[index].GetLevelMode(mode);

                if (level == null)
                    throw new ArgumentNullException(nameof(level));

                level.SetCurrentLevelIndex(index);
                level.SetMap(_levels[index].LevelMap);

                return true;
            }

            level = GetRandomLevelMode(mode);

            return true;
        }

        public LevelMode GetEndGameLevelMode(LevelTypeMode mode)
        {
            LevelMode endGameLevel = _levels[_levels.Length - 1].GetLevelMode(mode);

            return endGameLevel;
        }

        public UserModel GetUser()
        {
            UserModel user;
            _userSettings.Init(out user);

            user.Levels = GetLevels();

            return user;
        }
    
        public int GetNeedStars(int index)
        {
            if (index > _levels.Length || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return _levels[index].NeedStars;
        }

        public Sprite GetIcon(int index)
        {
            if (index > _levels.Length || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return _levels[index].Icon;
        }

        private LevelModel[] GetLevels()
        {
            if (_levels == null || _levels.Length == 0)
                throw new ArgumentNullException(nameof(_levels));

            LevelModel[] levels = new LevelModel[_levels.Length];

            for (int i = 0; i < levels.Length; i++)
            {
                levels[i] = new LevelModel()
                {
                    NeedStarForOpen = _levels[i].NeedStars,
                    OpenMode = LevelTypeMode.I,
                    Stars = 0,
                    IsEndGame = _levels[i].IsEndGame,
                    Id = i,
                };
            }

            levels[0].IsOpen = true;
            levels[0].OpenMode = LevelTypeMode.I;

            return levels;
        }

        private LevelMode GetRandomLevelMode(LevelTypeMode mode)
        {
            int randomLevelIndex = Random.Range(1, _levels.Length - 1);
            LevelSetting level = _levels[randomLevelIndex];

            if (level == null)
                throw new ArgumentNullException(nameof(level));

            LevelMode levelMode = level.GetLevelMode(mode);
            levelMode.SetCurrentLevelIndex(_levels.Length - 1);
            levelMode.SetMap(_levels[randomLevelIndex].LevelMap);

            return levelMode;
        }
    }
}