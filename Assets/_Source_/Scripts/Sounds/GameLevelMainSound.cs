using UnityEngine;

namespace Source.Scripts.Sounds
{
    public class GameLevelMainSound : MonoBehaviour
    {
        private void Start()
        {
            BackgroundSound.Instance.PlayGameLevelSound();
        }
    }
}
