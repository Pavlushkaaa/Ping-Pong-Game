using UnityEngine;
using System;
using NaughtyAttributes;

namespace Core
{
    public class Ball : MonoBehaviour
    {
        public event Action<Ball> Died;
        public Vector2 MoveDirection { get => _moveDirection; }
        [field:SerializeField] public float ColliderRadius { get; private set; }

        [SerializeField] private float _speed;
        [SerializeField] [ReadOnly] private Vector2 _moveDirection;

        [Space]
        [SerializeField] [ReadOnly] private float _currentDegree;
        [SerializeField] [ReadOnly] private Vector2 _currentNormal;
        [SerializeField] [ReadOnly] private Vector2 _lastNormal;
        [SerializeField] [ReadOnly] private float _sameNormals;

        private Transform _ball;
        private Rigidbody2D _ballRigidbody;
        private DestructibleSprite _destructor;
        private BallSoundPlayer _soundPlayer;

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            float num = ClampReflectAngle(- 2F * Vector2.Dot(inNormal, inDirection));

            return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
        }

        public void SetMoveDirection(Vector2 direction) => _moveDirection = direction;

        public void Die() { if (GameLoop.IsLooping) ForceDie(); }

        public void ForceDie()
        {
            _ballRigidbody.velocity *= 0.15f; // we stop the ball so that the fragments scatter correctly
            _destructor.Destruct();

            Died?.Invoke(this);
            Destroy(gameObject);
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
            _ball = transform;

            _ballRigidbody = GetComponent<Rigidbody2D>();
            _destructor = GetComponent<DestructibleSprite>();
            _soundPlayer = GetComponent<BallSoundPlayer>();
        }

        private void Move()
        {
            _ballRigidbody.velocity = _moveDirection.normalized * _speed;
        }

        private void FixAxisStick()
        {
            if ((_lastNormal + _currentNormal).magnitude <= 0.05f)
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
            
            _moveDirection = Reflect(_moveDirection.normalized, normal);
            
            FixSideStick(normal);
            FixAxisStick();

            _soundPlayer.PlayReflection();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<BasePoint>(out var point))
            {
                if(!point.IsLastTouch)
                    ChangeDirection(collision.contacts[0].normal);

                point.Contact();
                return;
            }
            else
            if (collision.gameObject.TryGetComponent<BallTrigger>(out var t)) t.OnTrigger();

            ChangeDirection(collision.contacts[0].normal);
        }
    }
}
