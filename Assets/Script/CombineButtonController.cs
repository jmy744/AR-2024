using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineButtonController : MonoBehaviour
{
    private static bool hydrogenDetected = false;
    private static bool lithiumDetected = false;
    public Button combineButton; // Reference to the Combine button
    public Text instructionText; // Reference to the instruction text UI element

    void Start()
    {
        combineButton.gameObject.SetActive(false); // Initially hide the Combine button
        combineButton.onClick.AddListener(OnCombineButtonClick);
        instructionText.gameObject.SetActive(false); // Initially hide the instruction text
    }

    public void OnHydrogenFound()
    {
        hydrogenDetected = true;
        CheckCombineButton();
    }

    public void OnHydrogenLost()
    {
        hydrogenDetected = false;
        CheckCombineButton();
    }

    public void OnLithiumFound()
    {
        lithiumDetected = true;
        CheckCombineButton();
    }

    public void OnLithiumLost()
    {
        lithiumDetected = false;
        CheckCombineButton();
    }

    private void CheckCombineButton()
    {
        if (hydrogenDetected && lithiumDetected)
        {
            combineButton.gameObject.SetActive(true); // Show the Combine button
        }
        else
        {
            combineButton.gameObject.SetActive(false); // Hide the Combine button
        }
    }

    private void OnCombineButtonClick()
    {
        instructionText.gameObject.SetActive(true); // Show the instruction text
        combineButton.gameObject.SetActive(false); // Hide the Combine button after clicking
    }
}
