using Agava.WebUtility;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class FocusGame : MonoBehaviour
    {
        private bool _isLock;

        public static FocusGame Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        public void Lock()
        {
            MuteAudio(true);
            PauseGame(true);

            _isLock = true;
        }

        public void Unlock()
        {
            _isLock = false;

            MuteAudio(false);
            PauseGame(false);
        }

        private void MuteAudio(bool value)
        {
            if (_isLock)
                return;

            AudioListener.pause = value;
        }

        private void PauseGame(bool value)
        {
            if (_isLock)
                return;

            Time.timeScale = value ? 0 : 1;
        }

        private void OnInBackgroundChangeApp(bool value)
        {
            MuteAudio(!value);
            PauseGame(!value);
        }

        private void OnInBackgroundChangeWeb(bool value)
        {
            MuteAudio(value);
            PauseGame(value);
        }
    }
}
