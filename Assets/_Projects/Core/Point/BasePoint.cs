using System;
using UnityEngine;

namespace Core
{
    public abstract class BasePoint : MonoBehaviour
    {
        public event Action<BasePoint> OnDestroy;
        public bool IsLastTouch { get; protected set; }

        public abstract void Contact();

        protected void Die() => OnDestroy?.Invoke(this);
    }
}
