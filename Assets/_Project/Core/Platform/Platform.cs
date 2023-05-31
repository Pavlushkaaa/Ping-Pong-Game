using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(DestructibleSprite))]
    public class Platform : MonoBehaviour
    {
        [SerializeField] private InputModule _input;

        [Space]
        [SerializeField] private float _speed;

        private float _xAxisLimit;

        private Transform _platform;
        private Camera _camera;
        private DestructibleSprite _destructor;

        private bool _isFreese;

        public void FreezeMove()=> _isFreese = true;
        public void StartMove()=> _isFreese = false;

        public void Destroy() => _destructor.Destruct();
        public void HidePlatform() => _destructor.Hide();
        public void ShowPlatform() => _destructor.Show();

        private void Awake() => _destructor = GetComponent<DestructibleSprite>();
        private void Start()
        {
            _camera = Camera.main;
            _platform = GetComponent<Transform>();
            _destructor = GetComponent<DestructibleSprite>();

            _xAxisLimit = InputModule.WorlsScreenSize.x - transform.localScale.x / 2;
        }

        private void Update() => Move();
        private void Move()
        {
            if (_isFreese) return;

            Vector2 target = _camera.ScreenToWorldPoint(new(_input.PointerXAxisPosition, 0));
            target.y = _platform.position.y;

            target = Vector2.Lerp(_platform.position, target, Time.deltaTime * _speed);
            target.x = Mathf.Clamp(target.x, -_xAxisLimit, _xAxisLimit);

            _platform.position = target;
        }
    }
}
