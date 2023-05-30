using UnityEngine.Events;

namespace Core.UI
{
    public class DestructibleButton : DestructibleSprite, IClickHandler
    {
        public UnityEvent Clicked;

        public virtual void OnClicked() => Clicked.Invoke();
    }
}
