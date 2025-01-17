using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Transition : MonoBehaviour
{
    public GameObject initialText; // Reference to the initial text element
    public UnityEngine.UI.Image initialImage; // Reference to the initial image element
    public Text updatedText; // Reference to the updated text element
    public GameObject popup; // Reference to the popup UI element
    public UnityEngine.UI.Image homeButtonImage; // Reference to the Image component of the home button
    public Sprite backButtonSprite; // Reference to the new back button sprite
    private ObserverBehaviour mObserverBehaviour;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Get the ObserverBehaviour component
        mObserverBehaviour = GetComponent<ObserverBehaviour>();
        if (mObserverBehaviour)
        {
            // Register event handlers
            mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
            initialPosition = transform.localPosition;
            initialRotation = transform.localRotation;
        }

        // Initially display the initialText and hide the updatedText and popup
        initialText.SetActive(true);
        if (initialImage != null)
        {
            initialImage.gameObject.SetActive(true);
        }
        updatedText.gameObject.SetActive(false);
        popup.SetActive(false);
    }

    private void OnDestroy()
    {
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    // Event handler for target status changes
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    void OnTrackingFound()
    {
        Debug.Log("Image Target Found");
        // Change the text content and hide the initial image
        initialText.SetActive(false); // Hide initial text
        if (initialImage != null)
        {
            initialImage.gameObject.SetActive(false); // Hide initial image
        }
        updatedText.gameObject.SetActive(true);  // Show updated text

        // Show the popup
        popup.SetActive(true);

        // Update the button to the back button sprite
        homeButtonImage.sprite = backButtonSprite;

        // Keep the 3D model anchored in place
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
    }

    void OnTrackingLost()
    {
        Debug.Log("Image Target Lost");
        // Optionally, handle what happens if the target is lost

        // Hide the popup if needed
        popup.SetActive(false);
    }
}
