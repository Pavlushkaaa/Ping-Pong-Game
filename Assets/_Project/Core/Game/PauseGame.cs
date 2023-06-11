using System;
using UnityEngine;

namespace Core
{
    public class PauseGame : MonoBehaviour
    {
        public event Action Showed;
        public event Action Hidden;
        public static bool IsPause { get; private set; }

        [SerializeField] private PauseGameView _view;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private EndGame _endGame;

        private TimeManager _time;

        private void Start()
        {
            IsPause = false;

            _time = GetComponentInParent<TimeManager>();

            _view.AppliedPause += ApplyPause;
            _view.RestartedGame += RestartGame;
            _view.PlayedAgain += CanselPause;
            _view.PlayedNext += NextLevel;
            _view.ReturnedToMainMenu += ReturnToMainMenu;

            _gameLoop.OnStartLoop += _view.ShowPauseButton;
            _gameLoop.OnEndLoop += _view.DestructPauseButton;
            _gameLoop.OnStopLoop += _view.DestructPauseButton;
            _gameLoop.OnContinueLoop += _view.ShowPauseButton;

            _endGame.OnEndFail += Reset;
            _endGame.OnEndSuccess += Reset;
        }

        private void ApplyPause()
        {
            _time.DoSlowmotion();
            IsPause = true;
            Showed?.Invoke();
        }
        private void CanselPause()
        {
            _time.DoNormal();
            IsPause = false;
            Hidden?.Invoke();
        }
        private void ReturnToMainMenu()
        {
            CanselPause();
            _endGame.ForceEndFail();
        }
        private void RestartGame()
        {
            CanselPause(); 
            _gameLoop.Restart();
        }

        private void NextLevel()
        {
            CanselPause();
            _gameLoop.StartLoop();
        }

        private void Reset()
        {
            if (!IsPause) return;

            CanselPause();
            _view.HidePanel();
        }
    }
}
