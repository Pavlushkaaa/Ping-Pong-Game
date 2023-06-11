using UnityEngine.Events;
using UnityEngine;

namespace Core.UI
{
    [RequireComponent(typeof(SoundPlayer))]
    public class DestructibleButton : DestructibleSprite, IClickHandler
    {
        public UnityEvent Clicked;

        [SerializeField] private AudioClip _clickClip;
        private SoundPlayer _soundPlayer;
        public virtual void OnClicked()
        {
            Clicked.Invoke();

            _soundPlayer.Play(_clickClip);
        }

        private void Start() => _soundPlayer = GetComponent<SoundPlayer>();
    }
}
