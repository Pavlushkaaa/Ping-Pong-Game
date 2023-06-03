using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class EndGameView : MonoBehaviour
    {
        public event Action PlayedAgain;
        public event Action PlayedNext;
        public event Action ReturnedToMainMenu;
        public event Action WatchedAd;

        [SerializeField] private Panel _donePanel;
        [SerializeField] private Panel _failPanel;
        [SerializeField] private Image _backgroundImage;

        private Panel _currentShowed;

        public void Hide()
        {
            _currentShowed.Destroy();
            _backgroundImage.enabled = false;
        }

        public void ShowDonePanel()
        {
            _donePanel.Show();
            _currentShowed = _donePanel;
            _backgroundImage.enabled = true;
        }
        public void ShowFailPanel()
        {
            _backgroundImage.enabled = true;
            _failPanel.Show();
            _currentShowed = _failPanel;
        }

        public void OnWatchedAd() => WatchedAd?.Invoke();
        public void OnReturnedToMainMenu()
        {
            ReturnedToMainMenu?.Invoke();
            Hide();
        }
        public void OnPlayedAgain()
        {
            PlayedAgain?.Invoke();
            Hide();
        }

        public void OnPlayedNext()
        {
            PlayedNext?.Invoke();
            Hide();
        }
        public void OnQuitedGame() => QuitGame.Quit();
    }
}
