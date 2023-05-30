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

        [ContextMenu("Get All Points")]
        private void GetAllPoints() => _points = FindObjectsOfType<BasePoint>().ToList();

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
    }
}
