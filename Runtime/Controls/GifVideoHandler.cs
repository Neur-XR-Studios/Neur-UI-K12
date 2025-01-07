using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace K12.UI
{

    public class GifVideoHandler : MonoBehaviour
    {
        public VideoPlayer videoPlayer;
        public VideoClip[] clipList;
        public TMP_Text videoName;
        public int CurrentVideo = 1;
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
            videoName.text = clipList[CurrentVideo].name;
        }
    }

}
