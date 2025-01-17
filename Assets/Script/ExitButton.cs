using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    // Reference to the button
    public Button exitButton;

    void Start()
    {
        // Add a listener to the button to call the ExitGame method when clicked
        exitButton.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
