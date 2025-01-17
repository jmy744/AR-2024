using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI; // For RawImage

public class ThumbnailManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign your VideoPlayer component
    public VideoClip previewClip;
    public VideoClip lesson2Clip;
    public VideoClip lesson3Clip;
    public RawImage rawImage;       // Assign your RawImage component
    public Texture defaultTexture; // Assign the default texture for thumbnails
    public GameObject PlayButton;

    public void PlayPreviewVideo()
    {
        rawImage.texture = videoPlayer.targetTexture;
        videoPlayer.clip = previewClip;
        videoPlayer.Play();
        PlayButton.SetActive(false);
    }

    public void PlayLesson2Video()
    {
        rawImage.texture = videoPlayer.targetTexture;
        videoPlayer.clip = lesson2Clip;
        videoPlayer.Play();
    }

    public void PlayLesson3Video()
    {
        rawImage.texture = videoPlayer.targetTexture;
        videoPlayer.clip = lesson3Clip;
        videoPlayer.Play();
    }
    public void StopPlay()
{
    videoPlayer.Stop(); // Use the correct method to stop the video
    PlayButton.SetActive(true);
    rawImage.texture = defaultTexture;
}
}
