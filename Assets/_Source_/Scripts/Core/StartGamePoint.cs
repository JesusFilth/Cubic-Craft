using Reflex.Attributes;
using Source.Scripts.Core.StateMashine;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.User;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class StartGamePoint : MonoBehaviour
    {
        [Inject] private GameStateMashine _stateMashine;
        [Inject] private IUserStorage _userStorage;
        [Inject] private IDefaultUser _defaultUser;

        private void Start()
        {
            _stateMashine.Init(_userStorage, _defaultUser);
            _stateMashine.EnterIn<BootstrapState>();
        }
    }
}