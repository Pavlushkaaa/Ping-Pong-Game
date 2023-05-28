using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(DestructibleSprite))]
    public class SimplePoint : BasePoint
    {
        private DestructibleSprite _spriteDestructor;
        public override void Contact()
        {
            Die();
            _spriteDestructor.Destruct();
            Destroy(gameObject);
        }

        protected void Start()
        {
            _spriteDestructor = GetComponent<DestructibleSprite>();
        }

        [ContextMenu("Destroy Point")]
        private void DestroyPoint() => Contact();
    }
}
