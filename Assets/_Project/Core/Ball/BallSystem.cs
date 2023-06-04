using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class BallSystem : MonoBehaviour
    {
        public event Action OnTrajectoryChoose;

        [SerializeField] private InputModule _input;
        [SerializeField] private Platform _platform;
        [SerializeField] private TimeManager _timeManager;
        [SerializeField] private BallTrajectory _trajectory;
        [SerializeField] private Ball _ballPrefab;

        [Space]
        [SerializeField] private Transform _ballsParent;

        private List<Ball> _balls = new List<Ball>();

        private EndGame _endGame;

        private Vector2 _ballSpawnPosition;

        public void Reset()
        {
            if (_balls.Count > 0)
            {
                foreach (var ball in _balls)
                {
                    ball.Died -= UpdateSystem;
                    ball.ForceDie();
                }

                _balls.Clear();
            }

            if (GameLoop.IsLooping)
                StartCoroutine(SpawnBall());
            else
                StartCoroutine(StartSpawnRandomBall());
        }

        public void MultiplyBalls(int multiplier)
        {
            if (!GameLoop.IsLooping) return;

            var ballsNumber = _balls.Count;
            if (ballsNumber * multiplier >= 40) return;

            for (int i = 0; i < ballsNumber; i++)
                for (int j = 0; j < multiplier; j++)
                    CreateNewBall(_balls[i].Position).SetMoveDirection(Random.insideUnitCircle.normalized);
        }
        public void IncreaseBallsSpeed()
        {
            foreach(var ball in _balls)
                ball.IncreaseSpeed();
        }
        public void DecreaseBallsSpeed()
        {
            foreach (var ball in _balls)
                ball.DecreaseSpeed();
        }

        private void Start()
        {
            _endGame = GetComponent<EndGame>();

            _ballSpawnPosition = _platform.transform.position;
            _ballSpawnPosition.y += 1;

            StartCoroutine(StartSpawnRandomBall());
        }

        private void UpdateSystem(Ball ball)
        {
            _balls.Remove(ball);

            if (_balls.Count == 0) _endGame.EndFail();
        }

        private Ball CreateNewBall(Vector2 position)
        {
            Ball newBall = Instantiate(_ballPrefab, position, Quaternion.identity);

            _balls.Add(newBall);
            newBall.Died += UpdateSystem;

            newBall.gameObject.transform.SetParent(_ballsParent);

            return newBall;
        }

        private IEnumerator SpawnBall()
        {
            var ball = CreateNewBall(_ballSpawnPosition);
            bool canCreateTrajectory = false;

            yield return new WaitForSecondsRealtime(0.4f); // pause to avoid an accidental click

            while (GameLoop.IsLooping)
            {
                ball.SetMoveDirection(Vector2.zero);

                if(PauseGame.IsPause)
                {
                    _trajectory.Hide();
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                if(!canCreateTrajectory)
                {
                    canCreateTrajectory = _input.IsTouchDown;

                    if (!canCreateTrajectory)
                    {
                        yield return new WaitForEndOfFrame();
                        continue;
                    }
                }

                if(_input.IsTouchMove && _input.TouchDirection.magnitude > 50)
                {
                    _trajectory.CreateTrajectory(_ballSpawnPosition, _input.TouchDirection.normalized);
                }
                else if(_input.IsTouchUp && _input.TouchDirection.magnitude > 50)
                {
                    ball.SetMoveDirection(_input.TouchDirection.normalized);
                    OnTrajectoryChoose?.Invoke();
                    break;
                }
                else
                    _trajectory.Hide();

                yield return new WaitForEndOfFrame();
            }

            _trajectory.Hide();
            yield break;
        }

        private IEnumerator StartSpawnRandomBall()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            SpawnRandomBall();
        }

        private void SpawnRandomBall()
        {
            var position = InputModule.CreateRandomPosition(0.1f);

            RaycastHit2D[] temp = new RaycastHit2D[0];
            while (Physics2D.CircleCastNonAlloc(position, 0.1f, Vector2.zero, temp) > 0)
                position = InputModule.CreateRandomPosition(0.1f);

            var x = Random.Range(-0.95f, 0.95f);
            var y = Random.Range(0.15f, 0.95f);
            Vector2 randomDirection = new Vector2(x, y);

            CreateNewBall(position).SetMoveDirection(randomDirection);
        }
    }
}
