using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace Core
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelTime;
        [SerializeField] private TextMeshProUGUI _gameTime;

        private string _prefix = "TIME: ";
        private int _currentTimeInSecond;
        private Coroutine _coroutine;

        private void Start ()
        {
            var ballSystem = GetComponent<BallSystem>();
            var gameLoop = GetComponent<GameLoop>();
            var mainMenu = GetComponent<MainMenu>();
            var pauseGame = GetComponent<PauseGame>();

            mainMenu.Showed += HideAll;
            pauseGame.Showed += HideAll;
            pauseGame.Hidden += ShowGameText;

            ballSystem.OnTrajectoryChoose += StartTimer;

            gameLoop.OnEndLoop += StopTimer;
            gameLoop.OnContinueLoop += ContinueTimer;
            gameLoop.OnStopLoop += StopTimer;
            gameLoop.OnStartLoop += ResetText;
        }

        private void HideAll()
        {
            HideText(_gameTime);
            HideText(_levelTime);
        }

        private void ResetText()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            HideText(_levelTime);
            ShowText(_gameTime);

            _currentTimeInSecond = 0;
            SetText(_gameTime);
        }

        private void ShowGameText() => ShowText(_gameTime);
            
        private void StartTimer()
        {
            ShowText(_gameTime);

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            if (_currentTimeInSecond <= 1)
                _coroutine = StartCoroutine(StartTimerCoroutine());
            else
                _coroutine = StartCoroutine(StartTimerCoroutine(_currentTimeInSecond));
        }

        private void ContinueTimer()
        {
            HideText(_levelTime);
            ShowText(_gameTime);
        }

        private void StopTimer()
        {
            HideText(_gameTime);
            ShowText(_levelTime);
            SetText(_levelTime);

            if(_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private void ShowText(TextMeshProUGUI text) => text.enabled = true;
        private void HideText(TextMeshProUGUI text) => text.enabled = false;

        private void SetText(TextMeshProUGUI text)
        {
            var stringBuilder = new StringBuilder(_prefix);
            var remainder = _currentTimeInSecond % 60;
            var minute = (_currentTimeInSecond - remainder) / 60;

            stringBuilder.Append(minute.ToString("00"));
            stringBuilder.Append(":");
            stringBuilder.Append(remainder.ToString("00"));

            text.text = stringBuilder.ToString();
        }

        private IEnumerator StartTimerCoroutine(int startTime = 0)
        {
            _currentTimeInSecond = startTime;
            SetText(_gameTime);

            while (true)
            {
                yield return new WaitForSeconds(1);
                _currentTimeInSecond++;
                SetText(_gameTime);
            }
        }
    }
}
