using System;
using UnityEngine;

namespace Source.Scripts.Sounds
{
    public class BackgroundSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _mainMenu;
        [SerializeField] private AudioClip _gameLevel;

        public static BackgroundSound Instance { get; private set; }

        public bool IsPlaying => AudioListener.volume == 1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlayClip(AudioClip clip)
        {
            if (clip == null)
                throw new ArgumentNullException(nameof(clip));

            if (IsPlaying == false)
                return;

            _audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void PlayMainMenuSound()
        {
            _audioSource.clip = _mainMenu;
            _audioSource.Play();
        }

        public void PlayGameLevelSound()
        {
            _audioSource.clip = _gameLevel;
            _audioSource.Play();
        }

        public void Stop()
        {
            _audioSource.Stop();
        }

        public void OnVolume()
        {
            AudioListener.volume = 1;
            _audioSource.Play();
        }

        public void OffVolume()
        {
            AudioListener.volume = 0;
            _audioSource.Stop();
        }
    }
}