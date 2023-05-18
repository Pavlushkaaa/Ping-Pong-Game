using Core.Game;
using Core.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BallSystem : MonoBehaviour
    {
        [SerializeField] private Ball _ballPrefab;

        [Space]
        [SerializeField] private Transform _ballsParent;

        private List<Ball> _balls = new List<Ball>();

        private EndGame _endGame;

        private Vector2 _ballSpawnPosition;

        private void Start()
        {
            _endGame = GetComponent<EndGame>();

            _ballSpawnPosition = FindObjectOfType<Platform>().gameObject.transform.position;
            _ballSpawnPosition.y += 1;

            CreateNewBall(_ballSpawnPosition);
        }

        public void Reset()
        {
            if (_balls.Count > 0)
            {
                foreach (var ball in _balls)
                {
                    ball.OnDie -= UpdateSystem;
                    ball.ForceDie();
                }

                _balls.Clear();
            }

            CreateNewBall(_ballSpawnPosition);
        }

        private void UpdateSystem(Ball ball)
        {
            _balls.Remove(ball);

            if (_balls.Count == 0) _endGame.EndFail();
        }

        private void CreateNewBall(Vector2 position)
        {
            Ball newBall = Instantiate(_ballPrefab, position, Quaternion.identity);

            _balls.Add(newBall);
            newBall.OnDie += UpdateSystem;

            newBall.gameObject.transform.SetParent(_ballsParent);
        }

    }
}
