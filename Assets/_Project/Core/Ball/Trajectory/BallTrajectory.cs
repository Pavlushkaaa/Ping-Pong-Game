using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BallTrajectory : MonoBehaviour
    {
        [SerializeField] private Ball _baseBallPrefab;

        [Space]
        [SerializeField] private float _offset;
        [SerializeField] private int _bounds;
        [SerializeField] private LayerMask _checkLayer;

        [Space]
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;

        [Space]
        [SerializeField] private TrajectoryPoint _pointPrefab;
        [Space]
        [SerializeField] private List<TrajectoryPoint> _points = new List<TrajectoryPoint>();

        public void CreateTrajectory(Vector2 origin, Vector2 direction)
        {
            Hide();

            var points = CalculateСollisionPoints(origin, direction);
            var pointPositions = new List<Vector2>();
            
            for (int i = 0; i < points.Count - 1; i++)
            {
                var numbers = (int)(Vector2.Distance(points[i], points[i + 1]) / _offset);
                if (numbers == 0) numbers = 1;
                for (int j = 0; j < numbers; j ++)
                {
                    float t = j / (float)numbers;
                    var pointPosition = Vector2.Lerp(points[i], points[i + 1], t);

                    pointPositions.Add(pointPosition);
                }
            }

            if (_points.Count < pointPositions.Count)
            {
                var numberNewPoints = pointPositions.Count - _points.Count;
                for (int i = 0; i < numberNewPoints; i++)
                    _points.Add(Instantiate(_pointPrefab, transform));
            }

            for (int i = 0; i < pointPositions.Count; i++)
            {
                _points[i].Position = pointPositions[i];
                _points[i].Show();
            }

            ColorPoints(pointPositions.Count);
        }
        public void Hide()
        {
            foreach (var point in _points)
                point.Hide();
        }

        private void ColorPoints(int pointsNumber)
        {
            var offset = -0.15f;
            for (int i = 0; i < pointsNumber; i++)
            {
                var t = i / (float)pointsNumber;

                offset = Mathf.Lerp(offset,0, Mathf.Clamp01(t * 2));

                _points[i].Color = Color.Lerp(_startColor, _endColor, Mathf.Clamp01(t + offset));
            }
        }

        private List<Vector2> CalculateСollisionPoints(Vector2 startPoint, Vector2 direction)
        {
            List<Vector2> points = new List<Vector2>(_bounds) { startPoint };

            var origin = startPoint;
            var rayDirection = direction;

            for (int i = 0; i < _bounds; i++)
            {
                var hit = Physics2D.CircleCast(origin, _baseBallPrefab.ColliderRadius, rayDirection, Mathf.Infinity, _checkLayer);

                rayDirection = Ball.Reflect(rayDirection.normalized, hit.normal);
                origin = hit.point + (_baseBallPrefab.ColliderRadius + 0.015f) * hit.normal;

                points.Add(origin);
            }

            return points;
        }
    }
}
