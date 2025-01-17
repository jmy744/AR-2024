using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public RawImage videoRawImage;  // The RawImage to display the video
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public Button playButton;       // Reference to the Play Button
    public Button closeButton;      // Reference to the Close Button

    void Start()
    {
        // Initially hide the video RawImage
        videoRawImage.gameObject.SetActive(false);

        // Add listener to Play button
        playButton.onClick.AddListener(PlayVideo);

        // Add listener to Close button
        closeButton.onClick.AddListener(CloseVideo);
    }

    // Method to play the video
    public void PlayVideo()
    {
        // Show the RawImage and start playing the video
        videoRawImage.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    // Method to close the video
    public void CloseVideo()
    {
        // Stop the video and hide the RawImage
        videoPlayer.Stop();
        videoRawImage.gameObject.SetActive(false);
    }
}
