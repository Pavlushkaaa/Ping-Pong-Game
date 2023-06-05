using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class PointSystem : MonoBehaviour
    {
        public event Action PointsEnded;
        public event Action<Vector2> PointDestroyed;

        [SerializeField][ReadOnly] private List<ScorePoint> _scorePoints = new List<ScorePoint>();
        
        private void Start()
        {
            foreach (var point in _scorePoints)
                point.OnDestroy += UpdateScorePoints;
        }

        private void UpdateScorePoints(BasePoint handler)
        {
            var scorePoint = (ScorePoint)handler;

            if (scorePoint.CheckEffectDrop())
                PointDestroyed?.Invoke(handler.Position);

            _scorePoints.Remove(scorePoint);

            if (_scorePoints.Count == 0) PointsEnded?.Invoke();
        }

        #if UNITY_EDITOR
        [Button]
        private void GetAllPoints() => _scorePoints = FindObjectsOfType<ScorePoint>().ToList();

        [ContextMenu("Destroy 90% Effect")]
        private void TryDestroyAllEffects()
        {
            for (int i = (int)(_scorePoints.Count * 0.1f); i < _scorePoints.Count; i++)
                _scorePoints[i].Contact();
        }
        #endif
    }
}
