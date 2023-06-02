using System;
using UnityEngine;

namespace Core
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action StartedGame;

        private Panel _mainPanel;

        public void ShowPanel() => _mainPanel.Show();
        public void HidePanel() => _mainPanel.Destroy();

        public void OnStartedGame() => StartedGame?.Invoke();
        public void OnQuited() => QuitGame.Quit();

        private void Start() => _mainPanel = GetComponent<Panel>();
    }
}
