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

        public AnimationType animationType;
        public RectTransform rectTransform;
        public Image imageComponent; // For fade animations
        public float animationDuration = 0.5f;
        public float turnOffDelay = 0.5f; // Delay before turning off the object
        public Vector2 slideOffset = new Vector2(500, 0);

        private Vector2 initialPosition;

        private void Awake()
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            if (imageComponent == null)
                imageComponent = GetComponent<Image>();

            initialPosition = rectTransform.anchoredPosition;
            PlayAnimation();
        }

        public void PlayAnimation()
        {
            StopAllCoroutines();
            switch (animationType)
            {
                case AnimationType.PopUp:
                    StartCoroutine(PopUpAnimation());
                    break;
                case AnimationType.SlideIn:
                    StartCoroutine(SlideInAnimation());
                    break;
                case AnimationType.SlideOut:
                    StartCoroutine(SlideOutAnimation());
                    break;
                case AnimationType.FadeIn:
                    StartCoroutine(FadeInAnimation());
                    break;
                case AnimationType.FadeOut:
                    StartCoroutine(FadeOutAndDisable());
                    break;
            }
            StartCoroutine(FadeOutAndDisable());
        }

        private IEnumerator PopUpAnimation()
        {
            float timeElapsed = 0f;
            rectTransform.localScale = Vector3.zero;

            while (timeElapsed < animationDuration)
            {
                float scale = Mathf.Lerp(0, 1, timeElapsed / animationDuration);
                rectTransform.localScale = new Vector3(scale, scale, scale);
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
            float timeElapsed = 0f;
            if (imageComponent != null)
            {
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
        }

        private IEnumerator FadeOutAndDisable()
        {

            // Wait for the additional delay
            yield return new WaitForSeconds(turnOffDelay);
            float timeElapsed = 0f;
            if (imageComponent != null)
            {
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


            // Turn off the GameObject
            gameObject.SetActive(false);
        }
    }
}
