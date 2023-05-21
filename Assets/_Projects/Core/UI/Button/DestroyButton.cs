using UnityEngine;

namespace Core.UI
{
    [RequireComponent(typeof(SpriteDestructor))]
    public class DestroyButton : SpriteClickHandler
    {
        public bool IsActive { get => _spriteRenderer.enabled; }

        private SpriteDestructor _spriteDestructor;

        private SpriteRenderer _spriteRenderer;

        public void HideButton() => _spriteDestructor.HideSprite();
        public void ShowButton() => _spriteDestructor.ShowSprite();

        public void Destroy()
        {
            _spriteDestructor.HideSprite();

            var buttonTransform = transform;
            var fragments = _spriteDestructor.Destruct();

            foreach (var fragment in fragments)
                fragment.transform.SetParent(buttonTransform);
        }

        private void Awake()
        {
            _spriteDestructor = GetComponent<SpriteDestructor>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
