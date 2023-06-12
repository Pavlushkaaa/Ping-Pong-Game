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

            gameLoop.OnStartLoop += StartCheck;
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

            StopTimer();
            StartCoroutine(StartHideTimer());
            _coroutine = StartCoroutine(StartTimer());
        }

        private void StopTimer()
        {
            Hide();
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private void ResetTimer()
        {
            Hide();
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(StartTimer());
        }

        private IEnumerator StartTimer()
        {
            while (GameLoop.IsLooping)
            {
                yield return new WaitForSecondsRealtime(10);
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
