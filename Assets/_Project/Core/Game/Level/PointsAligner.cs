using NaughtyAttributes;
using UnityEngine;

namespace Core
{
    public class PointsAligner : MonoBehaviour
    {
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _endPosition;

        [Button]
        private void Align()
        {
            var points = LoadPoints();
            if (points.Length == 0) return;

            for (int i = 0; i < points.Length; i++)
            {
                var time = i / ((float)points.Length - 1);
                points[i].localPosition = Vector2.Lerp(_startPosition, _endPosition, time);
            }
        }

        [Button]
        private void AutoAlign()
        {
            var points = LoadPoints();
            if (points.Length == 0) return;

            _startPosition = points[0].localPosition;
            _endPosition = points[points.Length - 1].localPosition;

            for (int i = 0; i < points.Length; i++)
            {
                var time = i / ((float)points.Length - 1);
                points[i].localPosition = Vector2.Lerp(_startPosition, _endPosition, time);
            }
        }

        private Transform[] LoadPoints()
        {
            var points = GetComponentsInChildren<PointsAligner>();

            if (points.Length <= 1)
            {
                var scorePoint = GetComponentsInChildren<ScorePoint>();

                var result = new Transform[scorePoint.Length];

                for (int i = 0; i < scorePoint.Length; i++)
                    result[i] = scorePoint[i].transform;

                return result;
            }
            else
            {
                var result = new Transform[points.Length -1];

                for (int i = 1; i < points.Length; i++)
                    result[i -1] = points[i].transform;

                return result;
            }
        }
    }
}
