using NaughtyAttributes;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(DestructibleSprite))]
    public class Effect : MonoBehaviour
    {
        public event Action<Effect> Caught;
        public event Action<Effect> Destroyed;
        public EffectSettings Settings { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private DestructibleSprite _spriteDestructor;
        public void Destroy()
        {
            Destroyed?.Invoke(this);
            _spriteDestructor.Destruct();
            Destroy(gameObject);
        }

        public void Initialize(EffectSettings settings)
        {
            _spriteRenderer.sprite = settings.Sprite;
            Settings = settings;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // The effects are on a layer that interacts only with the platform,
            // so there is no need to check what the contact was with
            Caught?.Invoke(this);
            Destroy();
        }
    }
}
