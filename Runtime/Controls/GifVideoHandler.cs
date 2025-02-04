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
        private void Start()
        {
            Preparevideo();
        }
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
            videoPlayer.clip = clipList[CurrentVideo];
            videoPlayer.Play();
            if (DataManager.StaticVariables.IS_ENGLISH)
            {
                videoName.text = videoNameEnglish[CurrentVideo];

            }
            else
            {
                videoName.text = videoNameHindi[CurrentVideo];

                EventManager.Broadcast(EVENTS.CORRECT_HINDI);

            }

        }
    }

}
