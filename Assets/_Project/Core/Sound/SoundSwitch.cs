using Core.UI;
using System;
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

        private string _settingsPath;

        public void Switch()
        {
            if (AudioListener.volume == 1)
                TurnMiddleSound();
            else if(AudioListener.volume == 0.5f)
                TurnOffSound();
            else
                TurnOnSound();

            SaveSettings();
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
            _settingsPath = Application.persistentDataPath + @"\SoundSettings.json";

            #if UNITY_EDITOR
            _settingsPath = Application.dataPath + @"\SoundSettings.json";
            #endif

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _button = GetComponent<DestructibleButton>();

            _button.OnShowed += CheckAudioState;

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (JsonSaver<SoundSettings>.Load(_settingsPath, out var settings))
            {
                AudioListener.volume = settings.Volume;
                CheckAudioState();
            }
            else
            {
                AudioListener.volume = 1;
                SaveSettings();
            }
        }

        private void SaveSettings()
        {
            var settings = new SoundSettings { Volume = AudioListener.volume };
            JsonSaver<SoundSettings>.Save(settings, _settingsPath);
        }
    }

    [Serializable]
    public class SoundSettings
    {
        public float Volume;
    }
}
