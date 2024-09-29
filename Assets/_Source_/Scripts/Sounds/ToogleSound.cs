using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Sounds
{
    public class ToogleSound : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _on;
        [SerializeField] private Image _off;

        private void Awake()
        {
            if (BackgroundSound.Instance.IsPlaying)
                On();
            else
                Off();
        }

        private void OnEnable()
        {
            _button?.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (BackgroundSound.Instance.IsPlaying)
            {
                BackgroundSound.Instance.OffVolume();
                Off();
            }
            else
            {
                BackgroundSound.Instance.OnVolume();
                On();
            }
        }

        private void On()
        {
            _on.enabled = true;
            _off.enabled = false;
        }

        private void Off()
        {
            _on.enabled = false;
            _off.enabled = true;
        }
    }
}
