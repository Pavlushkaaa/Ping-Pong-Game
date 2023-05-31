using UnityEngine;

namespace Core
{
    public class PauseGame : MonoBehaviour
    {
        [SerializeField] private PauseGameView _view;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private EndGame _endGame;

        private TimeManager _time;

        private bool _isPaused;

        private void Start()
        {
            _time = GetComponentInParent<TimeManager>();

            _view.AppliedPause += ApplyPause;
            _view.RestartedGame += RestartGame;
            _view.PlayedAgain += CanselPause;
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
            _isPaused = true;
        }
        private void CanselPause()
        {
            _time.DoNormal();
            _isPaused = false;
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
        private void Reset()
        {
            if (!_isPaused) return;

            CanselPause();
            _view.HidePanel();
        }
    }
}
