using UnityEngine;
using System;
using NaughtyAttributes;

namespace Core
{
    public class Ball : MonoBehaviour
    {
        public event Action<Ball> OnDie;
        public Vector2 MoveDirection { get => _moveDirection; }

        [SerializeField] private float _speed;
        [SerializeField] private float _minReflectAngle = 20;
        [SerializeField][ReadOnly] private Vector2 _moveDirection;

        [Space]
        [SerializeField][ReadOnly] private float _currentDegre;
        [SerializeField][ReadOnly] private Vector2 _currentNormal;
        [SerializeField][ReadOnly] private Vector2 _lastNormal;
        [SerializeField][ReadOnly] private float _sameNormals;

        private Transform _ball;
        private Rigidbody2D _ballRigidbody;
        private SpriteDestructor _destructor;
        private BallSoundPlayer _soundPlayer;

        public void Die() { if (GameLoop.IsLooping) ForceDie(); }

        public void ForceDie()
        {
            gameObject.AddComponent<BoxCollider2D>();

            _ballRigidbody.velocity *= 0.15f; // we stop the ball so that the fragments scatter correctly
            _destructor.Destruct(gameObject);

            OnDie?.Invoke(this);
            Destroy(gameObject);
        }

        private void Update() => Move();

        private void Start()
        {
            _ball = transform;
            _ballRigidbody = GetComponent<Rigidbody2D>();
            _destructor = GetComponent<SpriteDestructor>();
            _soundPlayer = GetComponent<BallSoundPlayer>();

            _moveDirection = Vector2.up;
        }

        private void Move()
        {
            _ballRigidbody.velocity = _moveDirection.normalized * _speed;
        }

        private Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            float factor = -2F * Vector2.Dot(inNormal, inDirection);

            factor = ClampReflectAngle(factor);
            _currentDegre = factor * Mathf.Rad2Deg;
            
            return new Vector2(factor * inNormal.x + inDirection.x,
                               factor * inNormal.y + inDirection.y);
        }

        private float ClampReflectAngle(float angle)
        {
            var degAngale = angle * Mathf.Rad2Deg;

            if (degAngale < _minReflectAngle && degAngale >= 0)
                return _minReflectAngle * Mathf.Deg2Rad;
            else if(degAngale > -_minReflectAngle && degAngale < 0)
                return -_minReflectAngle * Mathf.Deg2Rad;

            return angle;
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

        private void FixSideStick()
        {
            float fixOffset = 0.05f;
            _ball.position += (Vector3)_moveDirection * fixOffset;
        }

        private void ChangeDirection(Vector2 normal)
        {
            _currentNormal = normal;

            _moveDirection = Reflect(_moveDirection.normalized, normal);

            FixSideStick();
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
