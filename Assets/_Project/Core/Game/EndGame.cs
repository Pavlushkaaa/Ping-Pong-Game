using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(SoundPlayer))]
    public class EndGame : MonoBehaviour
    {
        public event Action OnEndFail;
        public event Action OnEndSuccess;

        [SerializeField] private EndGameView _view;

        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private LevelManager _levelsManager;

        [Space]
        [SerializeField] private AudioClip _doneClip;
        [SerializeField] private AudioClip _failClip;
        private SoundPlayer _soundPlayer;

        public void ForceEndFail()
        {
            _gameLoop.EndLoop();
            _mainMenu.Show();
            OnEndFail?.Invoke();
        }

        public void EndFail()
        {
            _soundPlayer.Play(_failClip);

            _gameLoop.StopLoop();
            _view.ShowFailPanel();
            OnEndFail?.Invoke();
        }

        public void EndSuccess()
        {
            _levelsManager.SetLevelComplete();
            _soundPlayer.Play(_doneClip);

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
            _view.WatchedAd += WatchAd;

            _view.PlayedAgain += _gameLoop.Restart;
            _view.PlayedNext += _gameLoop.StartLoop;
            _view.ReturnedToMainMenu += ForceEndFail;

            _soundPlayer = GetComponent<SoundPlayer>();
        }
    }
}
