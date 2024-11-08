using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class VoiceoverControllerScript : MonoBehaviour
{
    [Space(10)]
    [Header("----------------Voiceover UI REFERENCES--------------------")]
    [Space(20)]
    public Button Mute_Button;
    public static bool isActivity = true;
    public Sprite mute, unmute;
    [HideInInspector]
    public static AudioSource VoiceOverAudioSource;
    //public AudioSource BG_AudioSource;


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
/*
    public void BGMuteHandler()
    {
        bool IsMute = DataManager.StaticVariables.Is_BG_MUTED;
        if(IsMute)
        {
            BG_AudioSource.Pause();
        }
        else
        {
            BG_AudioSource.Play();
        }
    }*/

    void PlaySubStepVoiceOver()
    {
        Mute_Button.image.sprite = unmute;
        string audioClipName = DataManager.StaticVariables.COLUMN_05;
        AudioClip clip = Resources.Load<AudioClip>($"Audios/{audioClipName}");
        VoiceOverAudioSource.clip = clip;
        VoiceOverAudioSource.Play();
    }
    void PlayPromtVoiceOver()
    {
        Mute_Button.image.sprite = unmute;
        string audioClipName = DataManager.StaticVariables.COLUMN_06;
        AudioClip clip = Resources.Load<AudioClip>($"Audios/{audioClipName}");
        if(clip != null)
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
        if (isActivity)
        {
            if (VoiceOverAudioSource.isPlaying)
            {
                VoiceOverAudioSource.Stop();
                Mute_Button.image.sprite = mute;
            }
            else
            {
                VoiceOverAudioSource.Play();
                Mute_Button.image.sprite = unmute; 
            }
        }
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
