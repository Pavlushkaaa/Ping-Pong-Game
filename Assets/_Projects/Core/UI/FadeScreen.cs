using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Core.UI
{
    public class FadeScreen : MonoBehaviour
    {
        public FadeScreenState State { get => _state; }

        [SerializeField] private Image _screen;
        [SerializeField] private Color _screenColor;
        [SerializeField] private FadeScreenState _startState = FadeScreenState.Showed;

        [SerializeField] private FadeScreenState _state = FadeScreenState.Showed;

        private Color _color;

        public void ApplyFade()
        {
            SetAlfa(1);
            _state = FadeScreenState.Faded;
            _screen.raycastTarget = false;
        }

        public void ApplyShow()
        {
            SetAlfa(0);
            _state = FadeScreenState.Showed;
            _screen.raycastTarget = true;
        }

        public void Cycle(float time, float delayFade = 0, float delayShow = 0, Action endFade = null)
        {
            Fade(time, delayFade, () => { endFade?.Invoke(); Show(time, delayShow); });
        }

        #region Show
        public void Show(float time, float delay = 0, Action endShow = null)
        {
            if (_state == FadeScreenState.Showed) return;

            _screen.raycastTarget = false;
            StartCoroutine(DoShow(time, delay, endShow));
        }

        private IEnumerator DoShow(float time, float delay, Action endShow = null)
        {
            float currentTime = time;

            yield return new WaitForSecondsRealtime(delay);

            while (currentTime > 0)
            {
                currentTime -= Time.unscaledDeltaTime;

                SetAlfa(currentTime / time);

                yield return null;
            }

            _state = FadeScreenState.Showed;

            endShow?.Invoke();

            yield break;
        }
        #endregion

        #region Fade
        public void Fade(float time, float delay = 0, Action endFade = null)
        {
            if (_state == FadeScreenState.Faded) return;

            _screen.raycastTarget = true;
            StartCoroutine(DoFade(time, delay, endFade));
        }

        private IEnumerator DoFade(float time, float delay, Action endFade = null)
        {
            float currentTime = 0;

            yield return new WaitForSecondsRealtime(delay);

            while (currentTime <= time)
            {
                currentTime += Time.unscaledDeltaTime;

                SetAlfa(currentTime / time);

                yield return null;
            }

            _state = FadeScreenState.Faded;

            endFade?.Invoke();

            yield break;
        }
        #endregion

        private void SetAlfa(float target)
        {
            _color.a = target;

            _screen.color = _color;
        }

        private void Start()
        {
            _screen.raycastTarget = false;

            _color = _screenColor;
            _color.a = _screen.color.a;
            _screen.color = _color;

            if(_screen.color.a == 1) _state = FadeScreenState.Faded;

            if (_startState == FadeScreenState.Faded) ApplyFade();
            else ApplyShow();

        }
    }

    public enum FadeScreenState
    {
        Faded, Showed
    }
}
