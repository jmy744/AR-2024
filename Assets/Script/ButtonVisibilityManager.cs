using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVisibilityManager : MonoBehaviour
{
    public GameObject combineButton; // Reference to the Combine button

    void Start()
    {
        combineButton.SetActive(false); // Initially hide the Combine button
    }

    public void ShowCombineButton()
    {
        combineButton.SetActive(true); // Show the Combine button
    }

    public void HideCombineButton()
    {
        combineButton.SetActive(false); // Hide the Combine button
    }
}
