using System.Collections;
using System.IO;
using UnityEngine;

namespace Core
{
    public class GameTeacher : MonoBehaviour
    {
        [SerializeField] private GameObject _learnPanel;
        [SerializeField] private InputModule _inputModule;

        public bool IsFirstPlay { get; private set; }

        private Coroutine _coroutine;

        private string _infoPath;
        private bool _canHide = false;
        private bool _stopShow = false;

        private class EmptyFile { }

        private void Start()
        {
            _infoPath = Application.persistentDataPath + @"\TeachData.json";

            #if UNITY_EDITOR
            _infoPath = Application.dataPath + @"\TeachData.json";
            #endif

            if (!File.Exists(_infoPath))
            {
                IsFirstPlay = true;
                JsonSaver<EmptyFile>.Save(new EmptyFile(), _infoPath);
            }

            var gameLoop = GetComponent<GameLoop>();
            var ballSystem = GetComponent<BallSystem>();
            var pause = GetComponent<PauseGame>();

            pause.Showed += ResetTimer;
            gameLoop.OnStartLoop += StartCheck;
            gameLoop.OnStartLoop += () => _stopShow = false;
            gameLoop.OnEndLoop += Hide;
            gameLoop.OnStopLoop += Hide;
            gameLoop.OnEndLoop += StopTimer;
            ballSystem.OnTrajectoryChose += StopTimer;
            ballSystem.OnTrajectoryChoosing += ResetTimer;
            _inputModule.Touched += Hide;
        }

        private void Show() => _learnPanel.SetActive(true);
        private void Hide()
        {
            if (_canHide) 
                _learnPanel.SetActive(false);
        }

        private void StartCheck()
        {
            if(IsFirstPlay)
            {
                IsFirstPlay = false;
                Show();
            }

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            StartCoroutine(StartHideTimer());
            _coroutine = StartCoroutine(StartTimer());
        }

        private void StopTimer()
        {
            Hide();

            _stopShow = true;

            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private void ResetTimer()
        {
            Hide();

            if (_stopShow) return;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            while (GameLoop.IsLooping)
            {
                if (_stopShow) yield break;

                yield return new WaitForSecondsRealtime(10);
                if (_stopShow) yield break;

                if (!PauseGame.IsPause)
                    Show();
            }
        }

        private IEnumerator StartHideTimer()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            _canHide = true;
        }
    }
}
