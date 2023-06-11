using UnityEngine;

namespace Core
{
    public class RotatingPlatform : MonoBehaviour
    {
        [SerializeField] private float _speed = 100;
        [SerializeField] private PlatformDirection _direction = PlatformDirection.Right;

        private Transform _transform;

        private void Update() => _transform.Rotate(0, 0, _speed * (int)_direction * Time.deltaTime);

        private void Start() => _transform = transform;
    }
}
