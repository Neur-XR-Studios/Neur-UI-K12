using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Simulanis.ContentSDK.K12.UI
{
    public class ToggleAudioMute : MonoBehaviour
    {
        #region Header
        [Header("Visual Change")]
        public Image ToggleImg;
        public Sprite OnSprite, OffSprite;

        [Header("UI Components")]
        public TMP_Text statusText;                 // Text to display "Muted" or "Unmuted"
        public RectTransform toggleSprite;          // Sprite that moves on toggle
        private AudioSource audioSource;   // Background audio source
        //public AudioSource soundEffectAudioSource;  // Sound effects audio source

        [Header("Target Positions")]
        public RectTransform onTarget;              // RectTransform for "On" (Unmuted) position
        public RectTransform offTarget;             // RectTransform for "Off" (Muted) position

        [Header("Settings")]
        public string muteText = "Muted";
        public string unmuteText = "Unmuted";
        public float moveDuration = 0.3f;           // Duration of the smooth movement
        public bool isBackgroundAudio;              // Flag to determine if this is for background audio

        private bool isMuted = false;
        private Coroutine moveCoroutine;
        private string MutePrefKey => isBackgroundAudio ? "BackgroundAudioMuteState" : "SoundEffectMuteState";
        #endregion

        void Start()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            ToggleMute();
        }

        #region Toggle Manager
        public void ToggleMute()
        {
            isMuted = !isMuted;
            audioSource.mute = !isMuted;
            Debug.Log($"{MutePrefKey} : {isMuted}");
            // Save the mute state
            PlayerPrefs.SetInt(MutePrefKey, isMuted ? 1 : 0);
            PlayerPrefs.Save();

            // Update the UI and start the smooth movement
            UpdateToggleUI();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Set the target position based on the mute state
            RectTransform target = isMuted ? offTarget : onTarget;
            moveCoroutine = StartCoroutine(SmoothMove(target));
        }

        private void UpdateToggleUI()
        {
            // Set the text, font, and sprite based on mute status
            statusText.text = isMuted ? muteText : unmuteText;
            statusText.color = isMuted ? Color.white : Color.black;
            ToggleImg.sprite = isMuted ? OnSprite : OffSprite;
        }

        private IEnumerator SmoothMove(RectTransform target)
        {
            Vector2 initialPosition = toggleSprite.anchoredPosition;
            Vector2 targetPosition = target.anchoredPosition;
            float elapsedTime = 0;

            while (elapsedTime < moveDuration)
            {
                toggleSprite.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            toggleSprite.anchoredPosition = targetPosition;
        }
        #endregion
    }
}
