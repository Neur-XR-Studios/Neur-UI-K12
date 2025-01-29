using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Simulanis.ContentSDK.K12.Assessment
{
    public class UIAnimator : MonoBehaviour
    {
        public enum AnimationType
        {
            PopUp,
            SlideIn,
            SlideOut,
            FadeIn,
            FadeOut
        }

        [SerializeField] private AnimationType animationType;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image imageComponent;
        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private float turnOffDelay = 0.5f;
        [SerializeField] private Vector2 slideOffset = new Vector2(500, 0);
        [SerializeField] private bool isNeedToOff = false;
        [SerializeField] private float shakeDuration = 0.2f;
        [SerializeField] private float shakeMagnitude = 10f;

        private Vector2 initialPosition;

        private void Awake()
        {
            rectTransform ??= GetComponent<RectTransform>();
            imageComponent ??= GetComponent<Image>();
            initialPosition = rectTransform.anchoredPosition;
        }
        private void Start()
        {
            PlayAnimation();
        }
        public void PlayAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            switch (animationType)
            {
                case AnimationType.PopUp:
                    yield return PopUpAnimation();
                    yield return ShakeEffect();
                    break;
                case AnimationType.SlideIn:
                    yield return SlideInAnimation();
                    yield return ShakeEffect();
                    break;
                case AnimationType.SlideOut:
                    yield return SlideOutAnimation();
                    break;
                case AnimationType.FadeIn:
                    yield return FadeInAnimation();
                    yield return ShakeEffect();
                    break;
                case AnimationType.FadeOut:
                    yield return FadeOutAnimation();
                    break;
            }

            if (isNeedToOff)
            {
                yield return new WaitForSeconds(turnOffDelay);
                gameObject.SetActive(false);
            }
        }

        private IEnumerator PopUpAnimation()
        {
            float timeElapsed = 0f;
            rectTransform.localScale = Vector3.zero;

            while (timeElapsed < animationDuration)
            {
                rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            rectTransform.localScale = Vector3.one;
        }

        private IEnumerator SlideInAnimation()
        {
            float timeElapsed = 0f;
            Vector2 startPosition = initialPosition + slideOffset;

            while (timeElapsed < animationDuration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(startPosition, initialPosition, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = initialPosition;
        }

        private IEnumerator SlideOutAnimation()
        {
            float timeElapsed = 0f;
            Vector2 targetPosition = initialPosition + slideOffset;

            while (timeElapsed < animationDuration)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = targetPosition;
        }

        private IEnumerator FadeInAnimation()
        {
            if (imageComponent == null) yield break;

            float timeElapsed = 0f;
            Color color = imageComponent.color;
            color.a = 0;
            imageComponent.color = color;

            while (timeElapsed < animationDuration)
            {
                color.a = Mathf.Lerp(0, 1, timeElapsed / animationDuration);
                imageComponent.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            color.a = 1;
            imageComponent.color = color;
        }

        private IEnumerator FadeOutAnimation()
        {
            if (imageComponent == null) yield break;

            float timeElapsed = 0f;
            Color color = imageComponent.color;
            color.a = 1;
            imageComponent.color = color;

            while (timeElapsed < animationDuration)
            {
                color.a = Mathf.Lerp(1, 0, timeElapsed / animationDuration);
                imageComponent.color = color;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            color.a = 0;
            imageComponent.color = color;
        }

        private IEnumerator ShakeEffect()
        {
            Vector2 originalPosition = rectTransform.anchoredPosition;
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration)
            {
                float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
                float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
                rectTransform.anchoredPosition = originalPosition + new Vector2(offsetX, offsetY);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
