using Core.UI;
using UnityEngine;

namespace Core
{
    public class SoundSwitch : MonoBehaviour
    {
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundMiddle;
        [SerializeField] private Sprite _soundOff;

        private SpriteRenderer _spriteRenderer;
        private DestructibleButton _button;

        public void Switch()
        {
            if (AudioListener.volume == 1)
                TurnMiddleSound();
            else if(AudioListener.volume == 0.5f)
                TurnOffSound();
            else
                TurnOnSound();
        }

        private void TurnOffSound()
        {
            AudioListener.volume = 0;
            _spriteRenderer.sprite = _soundOff;
        }
        private void TurnMiddleSound()
        {
            AudioListener.volume = 0.5f;
            _spriteRenderer.sprite = _soundMiddle;
        }
        private void TurnOnSound()
        {
            AudioListener.volume = 1;
            _spriteRenderer.sprite = _soundOn;
        }

        public void CheckAudioState()
        {
            if (AudioListener.volume == 1)
                _spriteRenderer.sprite = _soundOn;
            else if (AudioListener.volume == 0.5f)
                _spriteRenderer.sprite = _soundMiddle;
            else
                _spriteRenderer.sprite = _soundOff;
        }

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _button = GetComponent<DestructibleButton>();

            _button.OnShowed += CheckAudioState;

            TurnOnSound();
        }
    }
}
