using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SpriteDestructor : MonoBehaviour
    {
        [SerializeField] [Range(0, 50)] private int _fragmentsNumber = 5;
        [SerializeField] private float _force = 250;
        [SerializeField] private Color _destroyColor;

        [Space]
        [SerializeField] private int _layerId = 7;

        private List<GameObject> _fragments;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private DestructInfo _destructInfo;

        [ContextMenu("Hide Sprite")]
        public void HideSprite()
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
        }

        [ContextMenu("Show Sprite")]
        public void ShowSprite()
        {
            _spriteRenderer.enabled = true;
            _collider.enabled = true;
        }

        public List<GameObject> Destruct() => SpriteExploder.Explode(_destructInfo);

        [ContextMenu("Init")]
        private void Awake()
        {
            if(TryGetComponent<SpriteRenderer>(out var sprite))
                _spriteRenderer = sprite;
            else
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _collider = GetComponent<Collider2D>();

            if (_collider is PolygonCollider2D)
                _destructInfo.PolygonCollider = (PolygonCollider2D)_collider;
            else if (_collider is BoxCollider2D)
                _destructInfo.BoxCollider = (BoxCollider2D)_collider;
            else
            {
                _destructInfo.BoxCollider = gameObject.AddComponent<BoxCollider2D>();
                _destructInfo.BoxCollider.enabled = false;
            }


            _destructInfo.GameObject = gameObject;
            _destructInfo.Transform = transform;
            _destructInfo.Rigidbody = GetComponent<Rigidbody2D>();
            _destructInfo.SpriteRenderer = _spriteRenderer;

            _destructInfo.LayerId = _layerId;
            _destructInfo.DestroyCcolor = _destroyColor;
            _destructInfo.Force = _force;
            _destructInfo.FragmentsNumber = _fragmentsNumber;
            _destructInfo.SubshatterSteps = 0;
        }

        [ContextMenu("Destruct")]
        private void InspectorDestroy()
        {
            Awake();
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
