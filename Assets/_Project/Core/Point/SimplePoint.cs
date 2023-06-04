using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(DestructibleSprite))]
    public class SimplePoint : BasePoint
    {
        private DestructibleSprite _spriteDestructor;

        public void ForceDestroy() => Destroy(gameObject);
        public void Destruct() => _spriteDestructor.Destruct();

        public void Hide() => _spriteDestructor.Hide();
        public void Show() => _spriteDestructor.Show();

        public override void Contact()
        {
            Die();
            _spriteDestructor.Destruct();
            Destroy(gameObject, 0.35f);
        }

        protected void Start() => _spriteDestructor = GetComponent<DestructibleSprite>();

        [ContextMenu("Destroy Point")]
        private void DestroyPoint() => Contact();
    }
}
