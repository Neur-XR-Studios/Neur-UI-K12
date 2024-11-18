using System.Collections;
using UnityEngine;
namespace K12.UI
{
    public class PopUpPanelEffect : MonoBehaviour
    {
        // Animation duration
        public float animDuration = 0.5f;

        // Scale values for pop-up and pop-down
        private Vector3 targetPopUpSize = new Vector3(1f, 1f, 1f); // Full size
        private Vector3 targetPopDownSize = new Vector3(0f, 0f, 0f); // Hidden size

        // Reference to the object’s transform for animation
        private Transform objTransform;

        // Awake: Initialize the Transform reference
        private void Awake()
        {
            objTransform = GetComponent<Transform>();
        }

        // Public method to trigger the pop-up effect
        public void PopUpEff()
        {
            StopAllCoroutines(); // Stop ongoing animations, if any
            StartCoroutine(PopUp());
        }

        // Public method to trigger the pop-down effect
        public void PopDownEff()
        {
            StopAllCoroutines(); // Stop ongoing animations, if any
            StartCoroutine(PopDown());
        }

        // Coroutine for animating the pop-up effect (growing from current size to full size)
        private IEnumerator PopUp()
        {
            Vector3 startScale = objTransform.localScale; // Start from the current scale
            float elapsedTime = 0f;

            // Animate from current size to target pop-up size
            while (elapsedTime < animDuration)
            {
                float t = elapsedTime / animDuration;
                objTransform.localScale = Vector3.Lerp(startScale, targetPopUpSize, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the scale is exactly at targetPopUpSize after animation
            objTransform.localScale = targetPopUpSize;
        }

        // Coroutine for animating the pop-down effect (shrinking from current size to hidden size)
        private IEnumerator PopDown()
        {
            Vector3 startScale = objTransform.localScale; // Start from the current scale
            float elapsedTime = 0f;

            // Animate from current size to target pop-down size
            while (elapsedTime < animDuration)
            {
                float t = elapsedTime / animDuration;
                objTransform.localScale = Vector3.Lerp(startScale, targetPopDownSize, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the scale is exactly at targetPopDownSize after animation
            objTransform.localScale = targetPopDownSize;
        }
    }

}
