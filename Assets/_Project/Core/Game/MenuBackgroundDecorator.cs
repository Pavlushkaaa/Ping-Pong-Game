using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MenuBackgroundDecorator : MonoBehaviour
    {
        [SerializeField] private int _pointsNumber;
        [SerializeField] private float _offsetFromBorder;
        [SerializeField] private float _minDistanceBettwenPoints = 0.5f;
        [SerializeField] private float _minScale = 0.15f;

        [Space]
        [SerializeField] private DecorativePoint _pointPrefab;
        [SerializeField] private Transform _pointsParent;
        private List<DecorativePoint> _points = new List<DecorativePoint>();

        public void Show()
        {
            if(_points.Count / _pointsNumber < 0.7f)
            {
                foreach (var point in _points)
                    point.ForceDestroy();

                _points.Clear();
                Decore();

                return;
            }

            foreach (var point in _points)
                point.Show();
        }

        public void Hide()
        {
            foreach (var point in _points)
                point.Hide();
        }

        [Button]
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

                _points.Add(point);
            }
        }

        private void UpdateList(BasePoint handler) => _points.Remove((DecorativePoint)handler);

        private Vector2 CreateRandomScale()
        {
            var number = Random.Range(_minScale, 1);
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
