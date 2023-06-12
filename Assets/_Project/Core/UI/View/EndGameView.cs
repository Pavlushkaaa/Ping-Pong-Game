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

        [Space]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Color _backDoneColor;
        [SerializeField] private Color _backFailColor;

        [Space]
        [SerializeField] private SpriteRenderer _continueGameButton;
        [SerializeField] private Sprite _wathAd;
        [SerializeField] private Sprite _freeContinue;

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
            _backgroundImage.color = _backDoneColor;
        }
        public void ShowFailPanel()
        {
            _backgroundImage.enabled = true;
            _failPanel.Show();
            _currentShowed = _failPanel;
            _backgroundImage.color = _backFailColor;
        }

        public void SetWatchAdSprite() => _continueGameButton.sprite = _wathAd;
        public void SetFreeContinueSprite() => _continueGameButton.sprite = _freeContinue;

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
