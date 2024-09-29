using System;
using System.Linq;
using Agava.YandexGames;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.Models;
using Source.Scripts.Core.Storage.User;
using UnityEngine;

namespace Source.Scripts.Core.Storage
{
    public class UserStorage : IStateStorage, IGoldStorage, ILevelStorage, IUserStorage
    {
        private const string UserKey = "User";
        private const string LeaderboardName = "Leaderboard";
        private const int MaxStars = 3;
        private const int GoldRise = 50;

        private UserModel _user;

        public event Action<UserStatsModel> StatsChanged;
        public event Action<int> GoldChanged;

        public int GetGold()
        {
            return _user.Gold;
        }

        public void AddGold(int gold)
        {
            _user.Gold = Mathf.Clamp(_user.Gold += gold, 0, int.MaxValue);
            GoldChanged?.Invoke(_user.Gold);
            Save();
        }

        public int GetPurchaseGold(float value)
        {
            return (int)value * GoldRise;
        }

        public bool AddStat(ref float value)
        {
            if (HasGold(value))
            {
                AddGold(-GetPurchaseGold((int)value));
                value += 1;
                StatsChanged?.Invoke(GetStats());
                Save();

                return true;
            }

            return false;
        }

        public UserStatsModel GetStats()
        {
            return _user.PlayerStats;
        }

        public int GetAllStars()
        {
            return _user.Stars;
        }

        public LevelModel GetLastOpenLevel()
        {
            LevelModel lastLevel = _user.Levels.Last(level => level.IsOpen);

            if (lastLevel == null)
                throw new ArgumentNullException(nameof(lastLevel));

            return lastLevel;
        }

        public LevelModel GetLevel(int index)
        {
            if (index < 0 || index > _user.Levels.Length)
                throw new IndexOutOfRangeException(nameof(index));

            return _user.Levels[index];
        }

        public LevelModel[] GetLevels()
        {
            return _user.Levels;
        }

        public int GetLevelCount() => _user.Levels.Length;

        public void AddStar(int indexLevel, LevelTypeMode mode)
        {
            LevelModel level = _user.Levels[indexLevel];

            if ((int)mode < (int)level.OpenMode)
                return;

            if (mode == level.OpenMode)
                if (level.Stars == MaxStars)
                    return;

            level.Stars++;
            _user.Stars++;

            if (level.OpenMode != LevelTypeMode.III)
                level.OpenMode = (LevelTypeMode)((int)level.OpenMode + 1);

            if (level.IsEndGame)
            {
                if (level.Stars == MaxStars)
                {
                    level.OpenMode = LevelTypeMode.I;
                    level.Stars = 0;
                }
            }

            OpenNewLevels();
            UpdatePlayerScore();
            Save();
        }

        public bool TryGetUpValueProperty(int index, out int upValue)
        {
            upValue = 0;

            if (_user.Levels[index].IsEndGame == false)
                return false;

            upValue = _user.Stars - _user.Levels[index].NeedStarForOpen;

            return true;
        }

        public void SetUser(UserModel user)
        {
            _user = user;

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            CheckPurchansedProduct();
#endif
        }

        private void OpenNewLevels()
        {
            for (int i = 0; i < _user.Levels.Length; i++)
            {
                _user.Levels[i].IsOpen = _user.Levels[i].NeedStarForOpen <= GetAllStars();
            }
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_user);
            PlayerPrefs.SetString(UserKey, json);
            PlayerPrefs.Save();

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetCloudSaveData(json);
#endif
        }

        private bool HasGold(float value)
        {
            if (GetPurchaseGold((int)value) <= _user.Gold)
                return true;

            return false;
        }

        private void UpdatePlayerScore()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
         if (PlayerAccount.IsAuthorized == false)
            return;

        int score = GetAllStars();

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < score)
                Leaderboard.SetScore(LeaderboardName, score);
        });
#endif
        }

        private void CheckPurchansedProduct()
        {
            Billing.GetPurchasedProducts((purchaseProduct) =>
            {
                foreach (var purchase in purchaseProduct.purchasedProducts)
                {
                    ConsumeProduct(purchase);
                }
            });
        }

        private void ConsumeProduct(PurchasedProduct purchase)
        {
            AddGold(GetCoinCount(purchase.productID));
            Billing.ConsumeProduct(purchase.purchaseToken);
        }

        private int GetCoinCount(string id)
        {
            const int CoinUpFromId = 10;

            string[] paths = id.Split('_');
            int coins = int.Parse(paths[1]);

            return coins * CoinUpFromId;
        }
    }
}