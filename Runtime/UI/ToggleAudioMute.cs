using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace K12.UI
{
    public class ToggleAudioMute : MonoBehaviour
    {
        [Header("Visual Change")]
        public Image ToggleImg;
        public Sprite OnSprite, OffSprite;

        [Header("UI Components")]
        public TMP_Text statusText;                 // Text to display "Muted" or "Unmuted"
        public RectTransform toggleSprite;          // Sprite that moves on toggle
        public AudioSource backgroundAudioSource;   // Background audio source
        public AudioSource soundEffectAudioSource;  // Sound effects audio source

        [Header("Target Positions")]
        public RectTransform onTarget;              // RectTransform for "On" (Unmuted) position
        public RectTransform offTarget;             // RectTransform for "Off" (Muted) position

        [Header("Settings")]
        public string muteText = "Muted";
        public string unmuteText = "Unmuted";
        public float moveDuration = 0.3f;           // Duration of the smooth movement
        public bool isBackgroundAudio;              // Flag to determine if this is for background audio

        public bool isMuted = true;
        private Coroutine moveCoroutine;
        private string MutePrefKey => isBackgroundAudio ? "BackgroundAudioMuteState" : "SoundEffectMuteState";

        void Start()
        {
            // Retrieve the saved mute state, defaulting to true if not set
            isMuted = PlayerPrefs.GetInt(MutePrefKey, 1) == 1;

            // Set mute state based on saved value
            if (isBackgroundAudio)
            {
                backgroundAudioSource.mute = !isMuted;
            }
            else
            {
                soundEffectAudioSource.mute = !isMuted;
            }

            // Initialize the UI based on the saved mute state
            UpdateToggleUI();

            // Set the toggle sprite position based on the saved mute state
            RectTransform target = isMuted ? offTarget : onTarget;
            toggleSprite.anchoredPosition = target.anchoredPosition;
        }

        public void ToggleMute()
        {
            isMuted = !isMuted;

            // Mute/unmute the correct audio source
            if (isBackgroundAudio)
            {
                backgroundAudioSource.mute = !isMuted;
            }
            else
            {
                soundEffectAudioSource.mute = !isMuted;
            }

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
    }
}
