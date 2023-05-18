using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SpriteDestructor : MonoBehaviour
    {
        [SerializeField] [Range(0, 50)] private int _fragmentsNumber = 10;
        [SerializeField] private float _force = 250;
        [SerializeField] private Color _destroyColor;

        [Space]
        [SerializeField] private int _layerId;

        private List<GameObject> _fragments;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        [ContextMenu("Clear")]
        public void Clear()
        {
            if (_fragments != null)
                foreach (var fragment in _fragments)
                    if (fragment != null) Destroy(fragment);

            _fragments.Clear();
        }

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

        public List<GameObject> Destruct(GameObject source)
        {
            _fragments = new List<GameObject>();

            Color orig;
            var sp = source.GetComponent<SpriteRenderer>();
            orig = sp.color;
            sp.color = _destroyColor;

            _fragments = SpriteExploder.GenerateVoronoiPieces(source, _force, _fragmentsNumber, 0);

            sp.color = orig;

            foreach (var fragment in _fragments)
            {
                fragment.AddComponent<AutoDestroy>();
                fragment.layer =  _layerId;
            }

            return _fragments;
        }

        [ContextMenu("Init")]
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        [ContextMenu("Destruct")]
        private void InspectorDestroy()
        {
            Awake();
            Destruct(gameObject);
        }
    }
}
