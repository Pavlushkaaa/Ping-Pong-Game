using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(SpriteDestructor))]
    public class SimplePoint : BasePoint
    {
        private SpriteDestructor _spriteDestructor;
        public override void Contact()
        {
            Die();
            _spriteDestructor.Destruct(gameObject);
            Destroy(gameObject);
        }

        protected void Start()
        {
            _spriteDestructor = GetComponent<SpriteDestructor>();
        }

        [ContextMenu("Destroy Point")]
        private void DestroyPoint() => Contact();
    }
}
