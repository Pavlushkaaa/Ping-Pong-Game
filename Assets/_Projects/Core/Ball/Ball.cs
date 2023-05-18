using UnityEngine;
using System;
using NaughtyAttributes;

namespace Core
{
    public class Ball : MonoBehaviour
    {
        public event Action<Ball> OnDie;

        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _clampReflection = new Vector2(30, 60);

        [Space]
        [SerializeField][ReadOnly] private float _currentDegre;
        [SerializeField][ReadOnly] private Vector3 _moveDirection;

        private Transform _ball;
        private Rigidbody2D _ballRigidbody;
        private SpriteDestructor _destructor;

        private event Action _onReflect;

        private void Update() => Move();

        private void Start()
        {
            _ball = transform;
            _ballRigidbody = GetComponent<Rigidbody2D>();
            _destructor = GetComponent<SpriteDestructor>();

            _moveDirection = Vector2.up;

            BallSoundPlayer ballSoundPlayer = GetComponent<BallSoundPlayer>();
            _onReflect += ballSoundPlayer.PlayReflection;
        }

        [ContextMenu("Die")]
        public void Die()
        {
            if (GameLoop.IsLooping) ForceDie();
        }

        public void ForceDie()
        {
            gameObject.AddComponent<BoxCollider2D>();

            _ballRigidbody.velocity *= 0.1f;
            _destructor.Destruct(gameObject);

            OnDie?.Invoke(this);
            Destroy(gameObject);
        }

        private void Move()
        {
            _ballRigidbody.velocity = _moveDirection.normalized * _speed;
        }

        private Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            float factor = -2F * Vector3.Dot(inNormal, inDirection);

            factor = Mathf.Clamp(factor, _clampReflection.x * Mathf.Deg2Rad, _clampReflection.y * Mathf.Deg2Rad);
            
            _currentDegre = factor * Mathf.Rad2Deg;

            return new Vector3(factor * inNormal.x + inDirection.x,
                factor * inNormal.y + inDirection.y,
                factor * inNormal.z + inDirection.z);
        }

        private void ChangeDirection(Vector2 normal)
        {
            _moveDirection = Reflect(_moveDirection, normal);

            float fixOffset = 0.05f; //prevents sticking
            _ball.position += _moveDirection.normalized * fixOffset;

            if (Mathf.Abs(_moveDirection.x) <= 0.05) _moveDirection.x += 0.05f * Mathf.Sign(_moveDirection.x);
            else if (Mathf.Abs(_moveDirection.y) <= 0.05) _moveDirection.y += 0.05f * Mathf.Sign(_moveDirection.y);

            _onReflect?.Invoke();
        }

        private void OnCollisionStay2D(Collision2D collision) => ChangeDirection(collision.contacts[0].normal);

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<BasePoint>(out var point))
                point.Contact();
            else
            if (collision.gameObject.TryGetComponent<BallTrigger>(out var t)) t.OnTrigger();
        }
    }
}
