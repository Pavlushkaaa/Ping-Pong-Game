using UnityEngine;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MainMenuView _view;
        [SerializeField] private MenuBackgroundDecorator _decorator;

        private GameLoop _gameLoop;

        public void Show()
        {
            _view.ShowPanel();
            _decorator.Show();
        }
        private void Hide() => _view.HidePanel();

        private void StartGame()
        {
            _decorator.Hide();
            Hide(); 
            _gameLoop.StartLoop();
        }

        private void Start()
        {
            _gameLoop= GetComponentInParent<GameLoop>();

            _decorator.Show();
            _view.StartedGame += StartGame;
        }
    }
}
