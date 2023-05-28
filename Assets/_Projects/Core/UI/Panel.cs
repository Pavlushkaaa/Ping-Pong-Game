using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private List<DestructibleSprite> _elements;
        public void Show()
        {
            foreach (var button in _elements) 
                button.Show();
        }
        public void Hide() 
        {
            foreach (var button in _elements)
                button.Hide();
        }
        public void Destroy()
        {
            foreach (var button in _elements)
                button.Destruct();
        }

        #if UNITY_EDITOR
        [Button]
        private void SetButtons()
        {
            _elements = gameObject.GetComponentsInChildren<DestructibleSprite>().ToList();
        }
        #endif
    }
}
