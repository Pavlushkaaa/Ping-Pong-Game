using Core.UI;
using System;
using System.Collections;
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
            _view.OnWatchAd += WatchAd;

            _view.OnPlayAgain += _gameLoop.Restart; 
            _view.OnBackToMainMenu += ForceEndFail;
        }
    }
}
