using System;
using UnityEngine;

namespace Core
{
    public class EndGame : MonoBehaviour
    {
        public event Action OnEndFail;
        public event Action OnEndSuccess;

        [SerializeField] private EndGameView _view;

        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private LevelManager _levelsManager;

        public void ForceEndFail()
        {
            _gameLoop.EndLoop();
            _mainMenu.Show();
            OnEndFail?.Invoke();
        }

        public void EndFail()
        {
            _gameLoop.StopLoop();
            _view.ShowFailPanel();
            OnEndFail?.Invoke();
        }

        public void EndSuccess()
        {
            _gameLoop.StopLoop();
            _levelsManager.SetLevelComplete();

            _view.ShowDonePanel();
            OnEndSuccess?.Invoke();
        }

        private void WatchAd()
        {
            ///TRUE
            _gameLoop.ContinueLoop();
            _view.Hide();
        }

        private void Start()
        {
            _view.WatchedAd += WatchAd;

            _view.PlayedAgain += _gameLoop.Restart;
            _view.PlayedNext += _gameLoop.StartLoop;
            _view.ReturnedToMainMenu += ForceEndFail;
        }
    }
}
