using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public GameObject profilePanel; // Reference to the Profile Panel
    public GameObject menuPanel;    // Reference to the Menu Panel

    // Toggle Profile Panel visibility
    public void ToggleProfilePanel()
    {
        // Close Menu Panel if it's open
        if (menuPanel.activeSelf)
            menuPanel.SetActive(false);

        // Toggle Profile Panel
        profilePanel.SetActive(!profilePanel.activeSelf);
    }

    // Toggle Menu Panel visibility
    public void ToggleMenuPanel()
    {
        // Close Profile Panel if it's open
        if (profilePanel.activeSelf)
            profilePanel.SetActive(false);

        // Toggle Menu Panel
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}

