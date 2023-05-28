using Core.Game;
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
                SpawnRandomBall();
        }

        private void Start()
        {
            _endGame = GetComponent<EndGame>();

            _ballSpawnPosition = _platform.transform.position;
            _ballSpawnPosition.y += 1;

            SpawnRandomBall();
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

            while (GameLoop.IsLooping)
            {
                ball.SetMoveDirection(Vector2.zero);

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

        private void SpawnRandomBall()
        {
            var x = Random.Range(-0.95f, 0.95f);
            var y = Random.Range(0.15f, 0.95f);
            Vector2 randomDirection = new Vector2(x, y);
            CreateNewBall(_ballSpawnPosition).SetMoveDirection(randomDirection);
        }
    }
}
