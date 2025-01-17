using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Transition2 : MonoBehaviour
{
    public GameObject initialText; // Reference to the initial text element
    public UnityEngine.UI.Image initialImage; // Reference to the initial image element
    public Text updatedText; // Reference to the updated text element
    public GameObject background; // Reference to the background image
    public GameObject scrollView; // Reference to the Scroll View GameObject
    public UnityEngine.UI.Image homeButtonImage; // Reference to the Image component of the home button
    public Sprite backButtonSprite; // Reference to the new back button sprite
    public GameObject beaker; // Reference to the beaker GameObject
   
    public Button startReactionButton; // Reference to the Start Reaction button
    
    private ObserverBehaviour mObserverBehaviour;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float lastTapTime = 0f;
    private const float doubleTapTime = 0.5f; // Time interval to consider a double tap

    private ScrollRect scrollRect;
    private RectTransform contentTransform;

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

        // Ensure Scroll View and its components are referenced
        scrollRect = scrollView.GetComponent<ScrollRect>();
        contentTransform = scrollView.transform.Find("Viewport/Content").GetComponent<RectTransform>();

        // Assign the content of ScrollRect
        if (scrollRect != null && contentTransform != null)
        {
            scrollRect.content = contentTransform;
        } 
            
        scrollView.SetActive(false); // Hide scroll view initially
        // Initially display the initialText and hide the updatedText, and scroll view
        initialText.SetActive(true);
        if (initialImage != null)
        {
            initialImage.gameObject.SetActive(true);
        }
        updatedText.gameObject.SetActive(false);
        
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

        // Update the button to the back button sprite
        homeButtonImage.sprite = backButtonSprite;

        // Keep the 3D model anchored in place
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;

        // Start listening for double taps
        StartCoroutine(DoubleTapDetection());
    }

    void OnTrackingLost()
    {
        Debug.Log("Image Target Lost");
        // Optionally, handle what happens if the target is lost

        // Stop listening for double taps
        StopCoroutine(DoubleTapDetection());
    }

    IEnumerator DoubleTapDetection()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                float currentTime = Time.time;
                if (currentTime - lastTapTime < doubleTapTime)
                {
                    OnDoubleTap();
                }
                lastTapTime = currentTime;
            }
            yield return null;
        }
    }

    void OnDoubleTap()
    {
        Debug.Log("Beaker Double Tapped");
        // Change content to scroll view
        initialText.SetActive(false); // Hide initial text
        if (initialImage != null)
        {
            initialImage.gameObject.SetActive(false); // Hide initial image
        }
        updatedText.gameObject.SetActive(false); // Hide updated text
        background.SetActive(false); // Hide background image
        scrollView.SetActive(true); // Show scroll view

        // Ensure the stir tool is hidden when editing
       
    }

    public void OnStartReaction()
    {
        // Display the beaker and stir tool
        if (beaker != null)
        {
            beaker.SetActive(true);
        }

       
        // Change the button text to "Combine"
        if (startReactionButton != null)
        {
            Text buttonText = startReactionButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "";
            }
        }

        // Hide the Scroll View
        if (scrollView != null)
        {
            scrollView.SetActive(false);
        }
    }
}
