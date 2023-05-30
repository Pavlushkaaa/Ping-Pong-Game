using UnityEngine;

namespace Core
{
    public class TrajectoryPoint : MonoBehaviour
    {
        public Color Color { set => _sprite.color = value; }
        public Vector2 Position { set => _transform.position = value; }

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Transform _transform;

        public void Show() => _sprite.enabled = true;
        public void Hide() => _sprite.enabled = false;
    }
}
