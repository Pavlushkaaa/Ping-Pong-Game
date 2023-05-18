using Core.Game;
using Core.UI;
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

        private void Reset()
        {
            CanselPause();
            _endGame.ForceEndFail();
        }

        private void Start()
        {
            _time = GetComponentInParent<TimeManager>();

            _view.OnDoPause += ApplyPause;
            _view.OnContinueGame += CanselPause;
            _view.OnBackToMainMenu += Reset;
            _view.OnQiutGame += QuitGame.Quit;

            _gameLoop.OnStartLoop += _view.ShowButton;
            _gameLoop.OnEndLoop += _view.HideButton;
            _gameLoop.OnStopLoop += _view.HideButton;
            _gameLoop.OnContinueLoop += _view.ShowButton;

            _endGame.OnEndFail += () => { if (!_isPaused) return;  _time.ForceNormal(); _view.HidePanel(); };
            _endGame.OnEndSuccess += () => { if (!_isPaused) return; _time.ForceNormal(); _view.HidePanel(); };
        }
    }
}
