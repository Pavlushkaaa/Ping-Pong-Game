using System.Collections;
using System.IO;
using UnityEngine;

namespace Core
{
    public class GameTeacher : MonoBehaviour
    {
        [SerializeField] private GameObject _learnPanel;
        [SerializeField] private InputModule _inputModule;

        private bool _firstPlay = false;

        private Coroutine _coroutine;

        private string _infoPath;

        private class EmptyFile { }

        private void Start()
        {
            _infoPath = Application.persistentDataPath + @"\TeachData.json";

            #if UNITY_EDITOR
            _infoPath = Application.dataPath + @"\TeachData.json";
            #endif

            if (!File.Exists(_infoPath))
            {
                _firstPlay = true;
                JsonSaver<EmptyFile>.Save(new EmptyFile(), _infoPath);
            }

            var gameLoop = GetComponent<GameLoop>();
            var ballSystem = GetComponent<BallSystem>();

            gameLoop.OnStartLoop += StartCheck;
            gameLoop.OnEndLoop += Hide;
            gameLoop.OnEndLoop += StopTimer;
            ballSystem.OnTrajectoryChoose += StopTimer;
            _inputModule.Touched += Hide;
        }

        private void Show() => _learnPanel.SetActive(true);
        private void Hide() => _learnPanel.SetActive(false);

        private void StartCheck()
        {
            if(_firstPlay)
            {
                Show();
                _firstPlay = false;
            }

            StopTimer();
            _coroutine = StartCoroutine(StartTimer());
        }

        private void StopTimer()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator StartTimer()
        {
            while (GameLoop.IsLooping)
            {
                yield return new WaitForSecondsRealtime(12);
                Show();
            }
        }
    }
}
