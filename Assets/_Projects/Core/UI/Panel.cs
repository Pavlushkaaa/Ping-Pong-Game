using Core.UI;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private List<DestroyButton> _buttons;
        public void Show()
        {
            foreach (var button in _buttons) 
                button.ShowButton();
        }
        public void Hide() 
        {
            foreach (var button in _buttons)
                button.HideButton();
        }
        public void Destroy()
        {
            foreach (var button in _buttons)
                button.Destroy();
        }

        [Button]
        private void SetButtons()
        {
            _buttons = gameObject.GetComponentsInChildren<DestroyButton>().ToList();
        }
    }
}
