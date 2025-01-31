using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace Simulanis.ContentSDK.K12.UI
{

    public class GifVideoHandler : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public VideoClip[] clipList;
        public TMP_Text videoName;
        public int CurrentVideo = 1;

        public string[] videoNameEnglish;
        public string[] videoNameHindi;
        public void NextButtonHandler()
        {
            if (CurrentVideo < clipList.Length - 1)
            {

                CurrentVideo++;
                Preparevideo();
            }
        }
        public void PreviousButtonHandler()
        {
            if (CurrentVideo > 0)
            {
                CurrentVideo--;
                Preparevideo();
            }
        }

        void Preparevideo()
        {
            string lan = LanguageSelectionManager.CurrentLanguage.ToLower();
           // lan = "hindi";
            videoPlayer.clip = clipList[CurrentVideo];
            videoPlayer.Play();
            if (lan == "english")
            {
                videoName.text = videoNameEnglish[CurrentVideo];

            }
            else if (lan == "hindi")
            {
                videoName.text = videoNameHindi[CurrentVideo];

                EventManager.Broadcast(EVENTS.CORRECT_HINDI);

            }

        }
    }

}
