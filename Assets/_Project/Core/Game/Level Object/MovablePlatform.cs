using NaughtyAttributes;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class MovablePlatform : MonoBehaviour
    {
        [SerializeField] private Vector2 _start;
        [SerializeField] private Vector2 _end;

        [Space]
        [SerializeField] private float _speed = 1;
        [SerializeField] private PlatformDirection _startDirection = PlatformDirection.Right;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            StartCoroutine(StartMove());
        }

        private IEnumerator StartMove()
        {
            int direction = (int)_startDirection;
            float time = 0;
            _transform.position = _start;

            if (direction == -1)
            {
                time = 1;
                _transform.position = _end;
            }

            while(true) 
            {
                time += Time.deltaTime * direction * _speed;

                if (time >= 1)
                {
                    direction = -1;
                    time = 1;
                }
                else if (time <= 0)
                {
                    direction = 1;
                    time = 0;
                }

                _transform.localPosition = Vector2.Lerp(_start, _end, time);

                yield return new WaitForEndOfFrame();
            }
        }

        [Button]
        private void SetStart() => _start = transform.position;
        [Button]
        private void SetEnd() => _end = transform.position;
    }

    public enum PlatformDirection
    {
        Left = -1,
        Right = 1
    }
}
