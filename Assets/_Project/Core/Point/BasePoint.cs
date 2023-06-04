using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class BasePoint : MonoBehaviour
    {
        public event Action<BasePoint> OnDestroy;

        public Vector2 Scale { get => _transform.localScale; set => _transform.localScale = value; }
        public Vector2 Position { get => _transform.position; }
        public bool IsLastTouch { get; protected set; }

        [SerializeField] private Transform _transform;

        [Space]
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private List<AudioClip> _destroyClips = new List<AudioClip>();

        public abstract void Contact();

        protected void Die()
        {
            if(_destroyClips.Count != 0)
                _soundPlayer.Play(_destroyClips);

            OnDestroy?.Invoke(this);
        }
    }
}
