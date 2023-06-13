using System;
using UnityEngine;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        public event Action Showed;

        [SerializeField] private MainMenuView _view;
        [SerializeField] private BackgroundDecorator _menuBackgroundDecorator;

        private GameLoop _gameLoop;

        public void Show()
        {
            Resources.UnloadUnusedAssets();

            _view.ShowPanel();
            _menuBackgroundDecorator.Show();

            Showed?.Invoke();
        }
        private void Hide() => _view.HidePanel();

        private void StartGame()
        {
            _menuBackgroundDecorator.Hide();
            Hide(); 
            _gameLoop.StartLoop();
        }

        private void Start()
        {
            _gameLoop= GetComponentInParent<GameLoop>();

            _menuBackgroundDecorator.Show();
            _view.StartedGame += StartGame;
        }
    }
}
