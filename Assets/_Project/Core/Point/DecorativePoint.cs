using System.Collections;
using UnityEngine;

namespace Core
{
    public class DecorativePoint : SimplePoint
    {
        public bool IsPlayColorAnimation { get; set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Space]
        [SerializeField] private bool _reflectBall = false;
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;
        [SerializeField] private Vector2 _animationTimeLimit;

        private new void Start()
        {
            IsLastTouch = !_reflectBall;
            base.Start();
            StartCoroutine(AnimateColor());
        }

        private IEnumerator AnimateColor()
        {
            float maxTime = Random.Range(_animationTimeLimit.x, _animationTimeLimit.y);
            float currentTime = Random.Range(_animationTimeLimit.x, _animationTimeLimit.y);
            var sign = Mathf.Sign(Random.Range(-1, 1));

            while (true)
            {
                if (!IsPlayColorAnimation)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                if (currentTime <= 0)
                {
                    sign = 1;
                    currentTime = 0;
                }    
                else if(currentTime >= maxTime)
                {
                    sign = -1;
                    currentTime = maxTime;
                }

                currentTime += Time.deltaTime * sign;

                _spriteRenderer.color = Color.Lerp(_startColor, _endColor, currentTime / maxTime);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
