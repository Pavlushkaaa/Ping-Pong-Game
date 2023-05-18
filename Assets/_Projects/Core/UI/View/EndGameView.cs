using Core.Game;
using Core.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class EndGameView : MonoBehaviour
    {
        public event Action OnPlayAgain;
        public event Action OnBackToMainMenu;
        public event Action OnWatchAd;

        [SerializeField] private Panel _donePanel;
        [SerializeField] private Panel _failPanel;
        [SerializeField] private Image _backImage;

        private Panel _currentShowed;

        public void Hide()
        {
            _currentShowed.Destroy();
            _backImage.enabled = false;
        }
        public void ShowDonePanel()
        {
            _donePanel.Show();
            _currentShowed = _donePanel;
            _backImage.enabled = true;
        }
        public void ShowFailPanel()
        {
            _backImage.enabled = true;
            _failPanel.Show();
            _currentShowed = _failPanel;
        }

        public void OnWatchAdClick() => OnWatchAd?.Invoke();
        public void OnBackToMainMenuClick()
        {
            OnBackToMainMenu?.Invoke();
            Hide();
        }
        public void OnPlayAgainClick()
        {
            OnPlayAgain?.Invoke();
            Hide();
        }

        public void OnQuitGameClick() => QuitGame.Quit();
    }
}
