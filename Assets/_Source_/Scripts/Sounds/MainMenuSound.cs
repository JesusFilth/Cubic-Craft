using UnityEngine;

namespace Source.Scripts.Sounds
{
    public class MainMenuSound : MonoBehaviour
    {
        private void Start()
        {
            BackgroundSound.Instance.PlayMainMenuSound();
        }
    }
}
