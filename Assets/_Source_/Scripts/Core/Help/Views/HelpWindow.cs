using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Core.Help.Views
{
    public class HelpWindow : MonoBehaviour
    {
        [SerializeField] private Transform _contents;
        [SerializeField] private Button _continueBtn;
        [SerializeField] private Button _nextBtn;
        [SerializeField] private Button _prevBtn;

        private int _currentIndexHelp = 0;

        private void OnEnable()
        {
            _nextBtn.onClick.AddListener(OnClickNext);
            _prevBtn.onClick.AddListener(OnClickPrev);
            _continueBtn.onClick.AddListener(OnClickContinue);

            Open();
        }

        private void OnDisable()
        {
            _nextBtn.onClick.RemoveListener(OnClickNext);
            _prevBtn.onClick.RemoveListener(OnClickPrev);
            _continueBtn.onClick.RemoveListener(OnClickContinue);
        }

        private void Update()
        {
            Time.timeScale = 0f;
        }

        public void Open()
        {
            Time.timeScale = 0;
            Initialize();
            UpdateButtons();
            ShowHelpWindow();
        }

        public void Close()
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }

        private void ShowHelpWindow()
        {
            for (int i = 0; i < _contents.childCount; i++)
            {
                _contents.GetChild(i).gameObject.SetActive(_currentIndexHelp == i);
            }
        }

        private void Initialize()
        {
            _currentIndexHelp = 0;
        }

        private void UpdateButtons()
        {
            if (_currentIndexHelp == 0)
            {
                _prevBtn.gameObject.SetActive(false);
            }
            else if (_currentIndexHelp > 0)
            {
                _prevBtn.gameObject.SetActive(true);
            }

            if (_currentIndexHelp != _contents.childCount - 1)
            {
                _nextBtn.gameObject.SetActive(true);
                _continueBtn.gameObject.SetActive(false);
            }

            if (_currentIndexHelp == _contents.childCount - 1)
            {
                _nextBtn.gameObject.SetActive(false);
                _continueBtn.gameObject.SetActive(true);
            }
        }

        private void OnClickNext()
        {
            _currentIndexHelp++;
            ShowHelpWindow();
            UpdateButtons();
        }

        private void OnClickPrev()
        {
            _currentIndexHelp--;
            ShowHelpWindow();
            UpdateButtons();
        }

        private void OnClickContinue()
        {
            Close();
        }
    }
}