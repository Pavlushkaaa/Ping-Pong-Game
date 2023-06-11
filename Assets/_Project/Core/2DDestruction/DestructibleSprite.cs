using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DestructibleSprite : MonoBehaviour
    {
        public event Action OnShowed;
        public event Action OnHidden;

        [SerializeField] [Range(0, 50)] private int _fragmentsNumber = 5;
        [SerializeField] private float _force = 250;
        [SerializeField] private Color _destroyColor;

        [Space]
        [SerializeField] private int _layerId = 7;

        private DestructInfo _destructInfo;
        private Collider2D _collider;

        [ContextMenu("Hide")]
        public void Hide()
        {
            _destructInfo.SpriteRenderer.enabled = false;
            _collider.enabled = false;
            OnHidden?.Invoke();
        }

        [ContextMenu("Show")]
        public void Show()
        {
            _destructInfo.SpriteRenderer.enabled = true;
            _collider.enabled = true;
            OnShowed?.Invoke();
        }

        public List<GameObject> Destruct()
        {
            Hide();
            return SpriteExploder.Explode(_destructInfo);
        }

        private void Awake()
        {
            if (TryGetComponent<SpriteRenderer>(out var sprite))
                _destructInfo.SpriteRenderer = sprite;
            else
                _destructInfo.SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

            var collider = GetComponent<Collider2D>();

            if (collider is PolygonCollider2D)
            {
                _destructInfo.PolygonCollider = (PolygonCollider2D)collider;
                _collider = _destructInfo.PolygonCollider;
            }
            else if (collider is BoxCollider2D)
            {
                _destructInfo.BoxCollider = (BoxCollider2D)collider;
                _collider = _destructInfo.BoxCollider;
            }
            else
            {
                _destructInfo.BoxCollider = gameObject.AddComponent<BoxCollider2D>();
                _destructInfo.BoxCollider.enabled = false;
                _collider = _destructInfo.BoxCollider;
            }
        }

        protected void Start()
        {
            _destructInfo.GameObject = gameObject;
            _destructInfo.Transform = transform;
            _destructInfo.Rigidbody = GetComponent<Rigidbody2D>();

            _destructInfo.LayerId = _layerId;
            _destructInfo.DestroyCcolor = _destroyColor;
            _destructInfo.Force = _force;
            _destructInfo.FragmentsNumber = _fragmentsNumber;
            _destructInfo.SubshatterSteps = 0;
        }

        [ContextMenu("Destruct")]
        private void InspectorDestroy()
        {
            Start();
            Destruct();
        }
    }

    [Serializable]
    public struct DestructInfo
    {
        public GameObject GameObject;
        public Transform Transform;
        public PolygonCollider2D PolygonCollider;
        public BoxCollider2D BoxCollider;
        public Rigidbody2D Rigidbody;
        public SpriteRenderer SpriteRenderer;

        public Color DestroyCcolor;
        public int LayerId;
        public float Force;
        public int FragmentsNumber;
        public int SubshatterSteps;
    }
}
