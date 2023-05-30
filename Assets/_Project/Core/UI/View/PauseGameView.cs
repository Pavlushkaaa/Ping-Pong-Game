using Core.Game;
using Core.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class PauseGameView : MonoBehaviour
    {
        public event Action AppliedPause;
        public event Action RestartedGame;
        public event Action PlayedAgain;
        public event Action ReturnedToMainMenu;

        [SerializeField] private DestructibleButton _pauseButton;
        [SerializeField] private Image _background;

        private Panel _pausePanel;

        public void ShowPauseButton() => _pauseButton.Show();
        public void DestructPauseButton() => _pauseButton.Destruct();
        public void HidePauseButton() => _pauseButton.Hide();

        public void ShowPanel()
        {
            _pausePanel.Show();
            _background.enabled = true;
        }
        public void HidePanel()
        {
            _background.enabled = false;
            _pausePanel.Destroy();
        }
        public void HideAll()
        {
            _pauseButton.Destruct();
            HidePanel();
        }

        #region Button
        public void OnAppliedPause()
        {
            if (TimeManager.IsSlowmotionPlaying) return;

            HidePauseButton();
            ShowPanel(); 
            AppliedPause?.Invoke();
        }
        public void OnRestartedGame()
        {
            HidePanel();
            ShowPauseButton();
            RestartedGame?.Invoke();
        }
        public void OnPlayedAgain()
        {
            HidePanel();
            ShowPauseButton();
            PlayedAgain?.Invoke();
        }
        public void OnReturnedToMainMenu()
        {
            HideAll();
            ReturnedToMainMenu?.Invoke();
        }
        public void OnQuitedGame() => QuitGame.Quit();
        #endregion

        private void Start() => _pausePanel = GetComponent<Panel>();
    }
}
