using UnityEngine;

namespace Core.Game
{
    [RequireComponent(typeof(SpriteDestructor))]
    public class Platform : MonoBehaviour
    {
        [SerializeField] private InputModule _input;

        [Space]
        [SerializeField] private float _speed;

        private float _xAxisLimit;

        private Transform _platform;
        private Camera _camera;
        private SpriteDestructor _spriteDestructor;

        public void Destroy() => _spriteDestructor.Destruct(gameObject);
        public void HidePlatform() => _spriteDestructor.HideSprite();
        public void ShowPlatform() => _spriteDestructor.ShowSprite();

        private void Update() => Move();

        private void Awake()
        {
            _camera = Camera.main;
            _platform = GetComponent<Transform>();
            _spriteDestructor = GetComponent<SpriteDestructor>();

            _xAxisLimit = InputModule.WorlsScreenSize.x - transform.localScale.x / 2;
        }

        private void Move()
        {
            Vector2 target = _camera.ScreenToWorldPoint(new(_input.PointerXAxisPosition, 0));
            target.y = _platform.position.y;

            target = Vector2.Lerp(_platform.position, target, Time.deltaTime * _speed);
            target.x = Mathf.Clamp(target.x, -_xAxisLimit, _xAxisLimit);

            _platform.position = target;
        }
    }
}
