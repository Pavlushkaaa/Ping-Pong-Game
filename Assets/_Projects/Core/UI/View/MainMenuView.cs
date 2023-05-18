using Core.Game;
using System;
using UnityEngine;

namespace Core
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnStartGame;

        private Panel _mainPanel;

        public void ShowPanel() => _mainPanel.Show();
        public void HidePanel() => _mainPanel.Destroy();

        public void OnStartGameClick() => OnStartGame?.Invoke();
        public void OnExitClick() => QuitGame.Quit();

        private void Start() => _mainPanel = GetComponent<Panel>();
    }
}
