using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ReactionManager : MonoBehaviour
{
    public Button startReactionButton; // Reference to the Start Reaction button

    void Start()
    {
        // Initially hide the start reaction button
        startReactionButton.gameObject.SetActive(false);
    }

    public static void CheckSelection()
    {
        // Check if two elements are selected
        if (SelectElement.selectedCount == 2)
        {
            Instance.startReactionButton.gameObject.SetActive(true); // Show start reaction button
        }
        else
        {
            Instance.startReactionButton.gameObject.SetActive(false); // Hide start reaction button
        }
    }

    private static ReactionManager instance;
    public static ReactionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ReactionManager>();
            }
            return instance;
        }
    }
}
