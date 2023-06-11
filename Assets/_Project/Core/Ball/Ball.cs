using UnityEngine;
using System;
using NaughtyAttributes;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Core
{
    public class Ball : MonoBehaviour
    {
        public event Action<Ball> Died;
        public Vector2 MoveDirection { get => _moveDirection; }
        public Vector2 Position { get => _ball.position; }
        [field: SerializeField] public float ColliderRadius { get; private set; }

        [SerializeField] private Animator _collisionEffect;
        [SerializeField] private BallSpeedSO _settings;
        [SerializeField] private Transform _ball;

        [Space]
        [SerializeField] private SoundPlayer _soundPlayer;
        [SerializeField] private List<AudioClip> _reflectionSounds;

        [Space]
        [SerializeField] private LayerMask _checkLayersForStick;

        [Space]
        [SerializeField] [ReadOnly] private Vector2 _moveDirection;
        [SerializeField] [ReadOnly] private float _currentDegree;
        [SerializeField] [ReadOnly] private Vector2 _currentNormal;
        [SerializeField] [ReadOnly] private Vector2 _lastNormal;
        [SerializeField] [ReadOnly] private float _sameNormals;

        private float _currentSpeed;

        private Rigidbody2D _ballRigidbody;
        private DestructibleSprite _destructor;

        private int _stickNumber = 0;

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            float num = ClampReflectAngle(-2F * Vector2.Dot(inNormal, inDirection));

            return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
        }

        public void SetMoveDirection(Vector2 direction) => _moveDirection = direction;
        public void IncreaseSpeed() => ChangeSpeed(_settings.SpeedChangeStep);
        public void DecreaseSpeed() => ChangeSpeed(-_settings.SpeedChangeStep);

        public void Die() { if (GameLoop.IsLooping) ForceDie(); }

        public void ForceDie()
        {
            _ballRigidbody.velocity *= 0.15f; // we stop the ball so that the fragments scatter correctly
            _destructor.Destruct();

            Died?.Invoke(this);
            Destroy(gameObject);
        }

        private void ChangeSpeed(float value)
        {
            var target = _currentSpeed + value;
            _currentSpeed = Mathf.Clamp(target, _settings.MinSpeed, _settings.MaxSpeed);
        }

        private static float ClampReflectAngle(float angle)
        {
            float minReflectAngle = 20;
            var degAngale = angle * Mathf.Rad2Deg;

            if (degAngale >= 0)
                return Mathf.Clamp(degAngale, minReflectAngle, 180 - minReflectAngle) * Mathf.Deg2Rad;
            else
                return Mathf.Clamp(degAngale, -180 + minReflectAngle, -minReflectAngle) * Mathf.Deg2Rad;
        }

        private void Update() => Move();

        private void Start()
        {
            _ballRigidbody = GetComponent<Rigidbody2D>();
            _destructor = GetComponent<DestructibleSprite>();

            _currentSpeed = _settings.NormalSpeed;
        }

        private void Move()
        {
            _ballRigidbody.velocity = _moveDirection.normalized * _currentSpeed;
        }

        private void UpdateStick()
        {
            _stickNumber++;

            if (_stickNumber > 10)
            {
                var direction = Random.insideUnitCircle.normalized;

                var badDirectionNumber = 0;

                while (Physics2D.Raycast(_ball.position, direction, 0.07f, _checkLayersForStick))
                {
                    badDirectionNumber++;
                    direction = Random.insideUnitCircle.normalized;

                    if (badDirectionNumber >= 20)
                        break;
                }

                SetMoveDirection(direction);
                _stickNumber = 0;
            }
        }

        private void FixAxisStick()
        {
            if ((_lastNormal - _currentNormal).magnitude <= 0.05f)
            {
                if (_sameNormals >= 5)
                {
                    _moveDirection.x += 0.01f * Mathf.Sign(_moveDirection.x);
                    _sameNormals = 0;
                }
                else
                    _sameNormals++;
            }
            else
                _sameNormals = 0;

            _lastNormal = _currentNormal;
        }

        private void FixSideStick(Vector2 normal)
        {
            _ball.position += (Vector3)normal * (ColliderRadius + 0.015f);
        }

        private void ChangeDirection(Vector2 normal)
        {
            _currentNormal = normal;

            #if UNITY_EDITOR
            _currentDegree = -2F * Vector2.Dot(normal, _moveDirection) * Mathf.Rad2Deg;
            #endif

            CreateCollisionEffect(normal);
            _moveDirection = Reflect(_moveDirection.normalized, normal);

            FixSideStick(normal);
            FixAxisStick();

            _soundPlayer.Play(_reflectionSounds);
        }

        private void CreateCollisionEffect(Vector2 collisionNormal)
        {
            var position = _ball.position - (Vector3)collisionNormal * ColliderRadius;
            Instantiate(_collisionEffect, position, Quaternion.identity).SetTrigger("Play");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<BasePoint>(out var point))
            {
                if (!point.IsLastTouch)
                    ChangeDirection(collision.contacts[0].normal);

                point.Contact();
                return;
            }
            else
            if (collision.gameObject.TryGetComponent<Platform>(out var t))
            {
                if (collision.contacts[0].normal != Vector2.up)
                    if (_ball.position.y < t.Position.y)
                        return;
            }

            ChangeDirection(collision.contacts[0].normal);
        }

        private void OnCollisionStay2D(Collision2D collision) => UpdateStick();
    }
}
