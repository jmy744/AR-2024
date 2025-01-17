using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResult : MonoBehaviour
{
    public GameObject resultText;
    public Button closeButton;

    private void OnMouseDown()
    {
        resultText.SetActive(true);
        closeButton.gameObject.SetActive(true);
    }

    public void CloseResult()
    {
        resultText.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }
}
