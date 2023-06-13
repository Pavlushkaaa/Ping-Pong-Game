using NaughtyAttributes;
using UnityEngine;

namespace Core
{
    public class PlatformScaler : MonoBehaviour
    {
        [SerializeField] private PlatformScalerSO _settings;

        [Space]
        [SerializeField] private float _scaleSpeed;

        private Transform _transform;
        private Vector3 _currentScale = Vector3.forward;
        private Vector3 _targetScale = Vector3.forward;

        public void Reset()
        {
            _currentScale.y = _settings.Height;
            _currentScale.x = _settings.NormalLength;

            _targetScale = _currentScale;

            _transform.localScale = _currentScale;
        }
        [Button]
        public void Increase() => ChangeTargetLenght(1);
        [Button]
        public void Decrease() => ChangeTargetLenght(-1);

        private void Start()
        {
            _transform = transform;

            Reset();
        }

        private void Update()
        {
            if(_targetScale.x != _currentScale.x)
                _currentScale.x = Mathf.Lerp(_currentScale.x, _targetScale.x, _scaleSpeed * Time.deltaTime);

            _transform.localScale = _currentScale;
        }

        private void ChangeTargetLenght(float value)
        {
            _targetScale.x = Mathf.Clamp(_targetScale.x + _settings.Step * value, _settings.MinLength, _settings.MaxLength);
        }
    }
}
