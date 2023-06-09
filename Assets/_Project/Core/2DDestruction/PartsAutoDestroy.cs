﻿using System.Collections;
using UnityEngine;

namespace Core
{
    public class PartsAutoDestroy : MonoBehaviour
    {
        private WaitForSeconds _pause = new WaitForSeconds(1);
        private Transform _transform;
        private float _maxYPosition;

        public void Start()
        {
            _maxYPosition = -InputModule.WorlsScreenSize.y - 1;
            _transform = transform;
            StartCoroutine(CheckPosition());
        }

        private IEnumerator CheckPosition()
        {
            while (true)
            {
                if (_transform.position.y < _maxYPosition)
                    Destroy(gameObject);

                yield return _pause;
            }
        }
    }
}
