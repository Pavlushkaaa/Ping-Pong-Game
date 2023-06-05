using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(DestructibleSprite))]
    [RequireComponent(typeof(SoundPlayer))]
    public class Effect : MonoBehaviour
    {
        public event Action<Effect> Caught;
        public event Action<Effect> Destroyed;
        public EffectSettings Settings { get; private set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private DestructibleSprite _spriteDestructor;

        [Space]
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private AudioClip _successDestroyClip;
        [SerializeField] private AudioClip _failDestroyClip;

        public void FailDestroy()
        {
            _soundPlayer.Play(_failDestroyClip);
            Destroy();
        }

        public void Destroy()
        {
            Destroyed?.Invoke(this);
            _spriteDestructor.Destruct();
            Destroy(gameObject, 0.25f);
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
            
            _soundPlayer.Play(_successDestroyClip);
            Destroy();
        }
    }
}
