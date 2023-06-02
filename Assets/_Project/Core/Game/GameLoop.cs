using System;
using UnityEngine;

namespace Core
{
    public class GameLoop : MonoBehaviour
    {
        public static bool IsLooping { get; private set; }

        public event Action OnStartLoop;
        public event Action OnStopLoop;
        public event Action OnContinueLoop;
        public event Action OnEndLoop;

        [SerializeField] private InputModule _inputModule;
        [SerializeField] private Platform _platform;

        private GameZone _gameZone;
        private TimeManager _timeManager;
        private BallSystem _ballSystem;

        public void Restart()
        {
            _gameZone.DestroyZone();
            OnEndLoop?.Invoke();

            IsLooping = true;

            _platform.FreezeMove();
            _platform.Destroy();
            _platform.ShowPlatform();
            _platform.Reset();

            _inputModule.Reset();
            _ballSystem.Reset();
            _gameZone.CreateZone();
            _timeManager.DoSlowmotion(1.5f);

            OnStartLoop?.Invoke();
        }

        public void StartLoop()
        {
            IsLooping = true;

            _platform.FreezeMove();
            _platform.Reset();
            _inputModule.Reset();
            _ballSystem.Reset();

            _gameZone.CreateZone();

            _platform.ShowPlatform();
            _timeManager.DoSlowmotion(1.5f);

            OnStartLoop?.Invoke();
        }

        public void StopLoop()
        {
            _timeManager.ForceStop();
            OnStopLoop?.Invoke();
        }

        public void ContinueLoop()
        {
            _platform.FreezeMove();
            _ballSystem.Reset();
            _inputModule.Reset();
            _platform.Reset();
            _timeManager.DoSlowmotion(1.5f);
            OnContinueLoop?.Invoke();
        }

        public void EndLoop()
        {
            IsLooping = false;

            _timeManager.ForceNormal();

            _ballSystem.Reset();
            _platform.Reset();
            _inputModule.Reset();

            _platform.HidePlatform();
            _platform.Destroy();

            _gameZone.DestroyZone();

            OnEndLoop?.Invoke();
        }

        private void Start()
        {
            IsLooping = false;

            _gameZone = GetComponent<GameZone>();
            _timeManager = GetComponent<TimeManager>();
            _ballSystem = GetComponent<BallSystem>();

            _platform.HidePlatform();

            _ballSystem.OnTrajectoryChoose += () => { _platform.StartMove(); _inputModule.Reset(); };
        }
    }
}
