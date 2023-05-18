using System;
using UnityEngine;

namespace Core
{
    public class BallTrigger : MonoBehaviour
    {
        public event Action OnEnter;

        [SerializeField] private bool _isOnceTrigger = false;

        private bool _isEnter = false;

        public void OnTrigger()
        {
            if (_isOnceTrigger && _isEnter) return;

            OnEnter?.Invoke();

            _isEnter = true;
        }
    }
}
