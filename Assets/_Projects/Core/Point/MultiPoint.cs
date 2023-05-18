using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MultiPoint : SimplePoint
    {
        [SerializeField] private int _maxContact = 3;
        [SerializeField] private List<Color> _color;

        private int _currentContact = 0;
        private SpriteRenderer _spriteRenderer;
        public override void Contact()
        {
            _currentContact++;

            if(_maxContact - 1 == _currentContact)
                IsLastTouch = true;

            if (_maxContact <= _currentContact)
                base.Contact();
            else
                _spriteRenderer.color = _color[_currentContact];

        }

        private new void Start()
        { 
            _spriteRenderer = GetComponent<SpriteRenderer>();
            IsLastTouch = false;

            base.Start();
        }
    }
}
