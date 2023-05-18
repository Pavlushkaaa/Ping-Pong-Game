using UnityEngine;

namespace Core
{
    public class BallSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _reflectionSound;
        [SerializeField] private AudioClip _destroySound;

        private AudioSource _audioSource;
        public void PlayReflection() => PlaySound(_reflectionSound);

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlaySound(AudioClip clip)
        {
            if (_audioSource.isPlaying) _audioSource.Stop();

            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
