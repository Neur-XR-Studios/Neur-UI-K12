using UnityEngine.UI;
using UnityEngine;
using TMPro;
namespace K12.UI
{
    public class VoiceoverControllerScript : MonoBehaviour
    {
        [Space(10)]
        [Header("----------------Voiceover UI REFERENCES--------------------")]
        [Space(20)]
        public Button Mute_Button;
        public static bool isActivity = true;
        public bool IsMuted = false;
        public Sprite mute, unmute;
        [HideInInspector]
        public static AudioSource VoiceOverAudioSource;
        public AudioSource[] BG_AudioSource;


        #region Initialization
        private void Start()
        {
            Initialisation();
        }
        void Initialisation()
        {
            Mute_Button.onClick.AddListener(MuteButtonClickHandler);
            VoiceOverAudioSource = gameObject.GetComponent<AudioSource>();
        }
        #endregion

        void PlaySubStepVoiceOver()
        {
            string audioClipName = DataManager.StaticVariables.COLUMN_05;
            AudioClip clip = Resources.Load<AudioClip>($"Audios/{audioClipName}");
            VoiceOverAudioSource.clip = clip;
            VoiceOverAudioSource.Play();
        }
        void PlayPromtVoiceOver()
        {
            string audioClipName = DataManager.StaticVariables.COLUMN_06;
            AudioClip clip = Resources.Load<AudioClip>($"Audios/{audioClipName}");
            if (clip != null)
            {
                VoiceOverAudioSource.clip = clip;
                VoiceOverAudioSource.Play();
            }
            else
            {
                Debug.Log("Audioclip not found!");
            }
        }

        public void MuteButtonClickHandler()
        {

            if (!IsMuted)
            {
                Debug.Log(PlayerPrefs.GetInt("BackgroundAudioMuteState"));
                Mute_Button.image.sprite = mute;
                if (PlayerPrefs.GetInt("BackgroundAudioMuteState") == 1)
                {
                    BG_AudioSource[0].mute = true;
                }
                if (PlayerPrefs.GetInt("SoundEffectMuteState") == 1)
                {
                    BG_AudioSource[1].mute = true;
                }
                VoiceOverAudioSource.mute = true;
            }
            else
            {
                Mute_Button.image.sprite = unmute;
                if (PlayerPrefs.GetInt("BackgroundAudioMuteState") == 1)
                {
                    BG_AudioSource[0].mute = false;
                }
                if (PlayerPrefs.GetInt("SoundEffectMuteState") == 1)
                {
                    BG_AudioSource[1].mute = false;
                }
                VoiceOverAudioSource.mute = false;

            }
            IsMuted = !IsMuted;
          
        }

        #region Event subscription
        //subribed on VoiceOver event
        private void OnEnable()
        {
            EventManager.AddHandler(EVENTS.STEP, PlaySubStepVoiceOver);
            EventManager.AddHandler(EVENTS.PROMPT, PlayPromtVoiceOver);
        }
        //unsubribed from SPAW event
        private void OnDisable()
        {
            EventManager.RemoveHandler(EVENTS.STEP, PlaySubStepVoiceOver);
            EventManager.RemoveHandler(EVENTS.PROMPT, PlayPromtVoiceOver);
        }
        #endregion
    }

}
