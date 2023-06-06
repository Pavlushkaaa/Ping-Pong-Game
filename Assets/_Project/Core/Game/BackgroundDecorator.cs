using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BackgroundDecorator : MonoBehaviour
    {
        [SerializeField] private int _pointsNumber;
        [SerializeField] [Range(0f, 1f)] private float _minPointsToDecore;
        [SerializeField] private float _offsetFromBorder = 0.15f;
        [SerializeField] private float _minDistanceBettwenPoints = 0.1f;
        [SerializeField] private float _maxScale = 1;
        [SerializeField] private float _minScale = 0.1f;

        [Space]
        [SerializeField] private DecorativePoint _pointPrefab;
        [SerializeField] private Transform _pointsParent;
        private List<DecorativePoint> _points = new List<DecorativePoint>();

        public void Show()
        {
            if(_points.Count / _pointsNumber < _minPointsToDecore)
            {
                foreach (var point in _points)
                    point.ForceDestroy();

                _points.Clear();
                Decore();

                return;
            }

            foreach (var point in _points)
            {
                point.Show();
                point.IsPlayColorAnimation = true;
            }
        }
        public void Hide()
        {
            foreach (var point in _points)
            {
                point.Hide();
                point.IsPlayColorAnimation = false;
            }
        }

        private void Decore()
        {
            for (int i = 0; i < _pointsNumber; i++)
            {
                var position = InputModule.CreateRandomPosition(_offsetFromBorder);

                while(!CheckPosition(position))
                    position = InputModule.CreateRandomPosition(_offsetFromBorder);

                var point = Instantiate(_pointPrefab, position, Quaternion.identity, _pointsParent);
                point.Scale = CreateRandomScale();
                point.OnDestroy += UpdateList;
                point.IsPlayColorAnimation = true;

                _points.Add(point);
            }
        }

        private void UpdateList(BasePoint handler) => _points.Remove((DecorativePoint)handler);

        private Vector2 CreateRandomScale()
        {
            var number = Random.Range(_minScale, _maxScale);
            return new Vector2(number, number);
        }

        private bool CheckPosition(Vector2 position)
        {
            for (int i = 0; i < _points.Count; i++)
                if((position - _points[i].Position).sqrMagnitude < _minDistanceBettwenPoints)
                    return false; 

            return true;
        }
    }
}
