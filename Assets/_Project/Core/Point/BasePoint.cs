using System;
using UnityEngine;

namespace Core
{
    public abstract class BasePoint : MonoBehaviour
    {
        public event Action<BasePoint> OnDestroy;

        public Vector2 Position { get => _transform.position; }
        public bool IsLastTouch { get; protected set; }

        private Transform _transform;

        public abstract void Contact();

        protected void Die() => OnDestroy?.Invoke(this);

        protected void Start() => _transform = transform;
    }
}
