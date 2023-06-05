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

        [SerializeField] [ReadOnly] private List<BasePoint> _points = new List<BasePoint>();

        private void Start()
        {
            foreach (var point in _points)
                point.OnDestroy += UpdateSystem;
        }

        private void UpdateSystem(BasePoint handler)
        {
            PointDestroyed?.Invoke(handler.Position);
            _points.Remove(handler);
            
            if(_points.Count == 0) PointsEnded?.Invoke();
        }

        #if UNITY_EDITOR
        [Button]
        private void GetAllPoints() => _points = FindObjectsOfType<BasePoint>().ToList();

        [ContextMenu("Destroy 90% Effect")]
        private void TryDestroyAllEffects()
        {
            for (int i = (int)(_points.Count * 0.1f); i < _points.Count; i++)
                _points[i].Contact();
        }
        #endif
    }
}
