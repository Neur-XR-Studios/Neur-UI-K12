using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace K12.UI
{
    public class ButtonClickSound : MonoBehaviour
    {
        public List<Button> UIButtons = new List<Button>();
        public List<Toggle> toggles = new List<Toggle>();
        public AudioSource EffectAudioSource;
        public AudioClip audioClip;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var button in UIButtons)
            {
                if(button is not null)
                button.onClick.AddListener(() => PlayClickAudio(audioClip));
            }

            foreach (var toggle in toggles)
            {
                if (toggle is not null)
                    toggle.onValueChanged.AddListener(value => PlayClickAudio(audioClip));
            }

        }

        void PlayClickAudio(AudioClip audioClip)
        {
            if (audioClip != null && EffectAudioSource != null)
            {
                if (EffectAudioSource.isPlaying)
                {
                    EffectAudioSource.Stop();
                }
                EffectAudioSource.PlayOneShot(audioClip);
            }
        }
        public void PlayClickAudio()
        {
            PlayClickAudio(audioClip);
        }
    }
}
