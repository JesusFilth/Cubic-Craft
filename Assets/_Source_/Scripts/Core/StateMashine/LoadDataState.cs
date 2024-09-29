using System;
using Agava.YandexGames;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.Models;
using Source.Scripts.Core.Storage.User;
using UnityEngine;

namespace Source.Scripts.Core.StateMashine
{
    public class LoadDataState : IGameState
    {
        private const string UserKey = "User";

        private readonly GameStateMashine _stateMashine;
        private readonly IUserStorage _userStorage;
        private readonly IDefaultUser _defaultUser;

        public LoadDataState(GameStateMashine stateMashine, IUserStorage userStorage, IDefaultUser defaultUser)
        {
            if (stateMashine == null)
                throw new ArgumentNullException(nameof(stateMashine));

            if (userStorage == null)
                throw new ArgumentNullException(nameof(userStorage));

            if (defaultUser == null)
                throw new ArgumentNullException(nameof(defaultUser));

            _stateMashine = stateMashine;
            _userStorage = userStorage;
            _defaultUser = defaultUser;
        }

        public void Execute()
        {
            Load();
        }

        private void Load()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
                LoadCloud();
            else
                LoadPlayerPrefs();
#else
            LoadPlayerPrefs();
#endif
        }

        private void LoadPlayerPrefs()
        {
            string json = PlayerPrefs.GetString(UserKey);
            SetLoadUserData(GetDeserialize(json));
        }

        private void LoadCloud()
        {
            PlayerAccount.GetCloudSaveData(onSuccessCallback: (json) =>
            {
                SetLoadUserData(GetDeserialize(json));
            });
        }

        private void SetLoadUserData(UserModel user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userStorage.SetUser(user);
            _stateMashine.EnterIn<LoadMainMenuSceneState>();
        }

        private UserModel GetDeserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
                return _defaultUser.GetUser();

            UserModel userModel = JsonUtility.FromJson<UserModel>(json);

            if (userModel == null)
                return _defaultUser.GetUser();

            if (userModel.Levels == null || userModel.Levels.Length == 0)
                return _defaultUser.GetUser();

            return userModel;
        }
    }
}
