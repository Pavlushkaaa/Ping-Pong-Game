using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public class SpriteClickHandler : MonoBehaviour, IClickHandler
    {
        public UnityEvent OnClickEvent;

        public virtual void OnClick() => OnClickEvent.Invoke();
    }
}
