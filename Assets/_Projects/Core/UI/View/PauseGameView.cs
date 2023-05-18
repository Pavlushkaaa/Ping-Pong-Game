using Core.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class PauseGameView : MonoBehaviour
    {
        public event Action OnDoPause;
        public event Action OnContinueGame;
        public event Action OnBackToMainMenu;
        public event Action OnQiutGame;

        [SerializeField] private DestroyButton _pauseButton;
        [SerializeField] private Image _back;

        private Panel _pausePanel;

        public void ShowButton() => _pauseButton.ShowButton();
        public void HideButton() => _pauseButton.Destroy();
        public void ShowPanel()
        {
            _pausePanel.Show();
            _back.enabled = true;
        }
        public void HidePanel()
        {
            _back.enabled = false;
            _pausePanel.Destroy();
        }

        public void HideAll()
        {
            _pauseButton.Destroy();
            HidePanel();
        }

        #region Button
        public void OnDoPauseClick()
        {
            if (TimeManager.IsSlowmotionPlaying) return;

            ShowPanel(); 
            OnDoPause?.Invoke();
        }
        public void OnContinueGameClick()
        {
            HidePanel();
            OnContinueGame?.Invoke();
        }

        public void OnBackToMainMenuClick()
        {
            HideAll();
            OnBackToMainMenu?.Invoke();
        }
        public void OnQiutGameClick() => OnQiutGame?.Invoke();  
        #endregion

        private void Start() => _pausePanel = GetComponent<Panel>();
    }
}
