using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [RequireComponent (typeof (AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private float _volume = 1;

        private AudioSource _audioSource;

        public void Play(AudioClip sound) => _audioSource.PlayOneShot(sound);

        public void Play(List<AudioClip> sounds) => Play(sounds[Random.Range(0, sounds.Count)]);

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = _volume;
        }
    }
}
