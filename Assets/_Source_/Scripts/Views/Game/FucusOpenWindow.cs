using UnityEngine;

namespace Source.Scripts.Views.Game
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FucusOpenWindow : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (_canvasGroup.alpha == 1)
                Time.timeScale = 0;
        }
    }
}
