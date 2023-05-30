using System.Collections;
using UnityEngine;

namespace Core
{
    public class TimeManager : MonoBehaviour
    {
        public static bool IsSlowmotionPlaying { get; private set; }

        [SerializeField] private float _slowdownFactor = 0.01f;
        [SerializeField] private float _slowdownLength = 1f;

        private Coroutine _coroutine;

        public void DoNormal()
        {
            if (IsSlowmotionPlaying && _coroutine != null) StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(BackToNormalTime());
        }

        public void DoSlowmotion(float time)
        {
            if (IsSlowmotionPlaying && _coroutine != null) StopCoroutine(_coroutine);

            Time.timeScale = _slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            IsSlowmotionPlaying = true;

            _coroutine = StartCoroutine(BackToNormalTime(time));
        }

        public void DoSlowmotion()
        {
            Time.timeScale = _slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            IsSlowmotionPlaying = true;
        }

        private void Start()
        {
            IsSlowmotionPlaying = false;
        }

        public void ForceNormal()
        {
            Time.fixedDeltaTime = 0.02f;
            Time.timeScale = 1;
        }

        public void ForceStop()
        {
            Time.fixedDeltaTime = 0.0001f;
            Time.timeScale = 0.0001f;
        }

        private IEnumerator BackToNormalTime()
        {
            while(Time.timeScale != 1)
            {
                Time.timeScale += (1f / _slowdownLength) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

                Time.fixedDeltaTime = Time.timeScale * .02f;

                yield return new WaitForEndOfFrame();
            }

            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            IsSlowmotionPlaying = false;
            yield break;
        }

        private IEnumerator BackToNormalTime(float time)
        {
            while (Time.timeScale != 1)
            {
                Time.timeScale += (1f / time) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

                Time.fixedDeltaTime = Time.timeScale * .02f;

                yield return new WaitForEndOfFrame();
            }

            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            IsSlowmotionPlaying = false;

            yield break;
        }
    }
}
