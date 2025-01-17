using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Button buttonOn; // Reference to the On button
    public Button buttonOff; // Reference to the Off button
    public AudioSource musicSource; // Reference to the Audio Source

    void Start()
    {
        // Initially, show the On button and hide the Off button
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);

        // Play the music automatically
        musicSource.Play();

        // Assign button listeners
        buttonOn.onClick.AddListener(TurnMusicOff);
        buttonOff.onClick.AddListener(TurnMusicOn);
    }

    void TurnMusicOff()
    {
        musicSource.Stop();
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);
    }

    void TurnMusicOn()
    {
        musicSource.Play();
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);
    }
}
