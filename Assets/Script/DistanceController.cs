using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this for UI elements

public class DistanceController : MonoBehaviour
{
    [System.Serializable]
    public class Combination
    {
        public Transform marker1;         // Marker for the first element (e.g., Hydrogen)
        public GameObject model1;         // Model for the first element
        public Transform marker2;         // Marker for the second element (e.g., Oxygen)
        public GameObject model2;         // Model for the second element
        public GameObject combinedModel;  // Combined model for the resulting compound
        public string resultText;         // Result text for the combination
    }

    public List<Combination> combinations; // List of combinations
    public float distanceThreshold = 290f; // Distance to trigger combined model
    public Button exploreButton;           // Button to show result
    public GameObject resultCard;          // Card to display the result
    public Text resultDescription;         // Text to display the result description
    public Button closeButton;             // Button to close the result card

    private Combination activeCombination = null; // Currently active combination
    private Vector3 smoothedPosition;             // Smoothed position for the active combination

    void Start()
    {
        // Ensure all combined models are hidden initially
        foreach (var combo in combinations)
        {
            if (combo.combinedModel != null)
                combo.combinedModel.SetActive(false);

            if (combo.model1 != null)
                combo.model1.SetActive(true);

            if (combo.model2 != null)
                combo.model2.SetActive(true);
        }

        // Ensure the explore button and result card are hidden initially
        if (exploreButton != null)
            exploreButton.gameObject.SetActive(false);

        if (resultCard != null)
            resultCard.SetActive(false);

        // Add listeners to buttons
        if (exploreButton != null)
            exploreButton.onClick.AddListener(OnExploreButtonClick);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    void Update()
    {
        Combination detectedCombination = null;

        // Check each combination to see if markers are within range
        foreach (var combo in combinations)
        {
            if (combo.marker1 != null && combo.marker2 != null &&
                combo.model1 != null && combo.model2 != null && combo.combinedModel != null)
            {
                float distance = Vector3.Distance(combo.marker1.position, combo.marker2.position);

                // Debug log for distance
                Debug.Log($"Distance between {combo.marker1.name} and {combo.marker2.name}: {distance}");

                // Check if markers are within the distance threshold and visible
                if (distance <= distanceThreshold && AreMarkersVisible(combo.marker1, combo.marker2))
                {
                    detectedCombination = combo;
                    break; // Exit loop as we only need the first detected combination
                }
            }
        }

        // Handle the active combination
        if (detectedCombination != null)
        {
            if (detectedCombination != activeCombination)
            {
                // A new combination is detected
                DeactivateCurrentCombination(); // Deactivate the previous combination
                activeCombination = detectedCombination;
            }

            // Smooth the position of the combined model
            Vector3 midpoint = (activeCombination.marker1.position + activeCombination.marker2.position) / 2;
            smoothedPosition = Vector3.Lerp(smoothedPosition, midpoint, 0.1f);

            // Show the combined model
            activeCombination.combinedModel.SetActive(true);
            activeCombination.combinedModel.transform.position = smoothedPosition;

            // Hide individual models
            activeCombination.model1.SetActive(false);
            activeCombination.model2.SetActive(false);

            // Show the explore button
            if (exploreButton != null)
                exploreButton.gameObject.SetActive(true);

            Debug.Log($"Showing combined model for {activeCombination.marker1.name} and {activeCombination.marker2.name}.");
        }
        else
        {
            // No combination is detected; reset everything
            DeactivateCurrentCombination();
        }
    }

    // Handle the explore button click
    private void OnExploreButtonClick()
    {
        if (activeCombination != null)
        {
            // Set the result description text for the active combination
            if (resultDescription != null)
                resultDescription.text = activeCombination.resultText;

            // Show the result card with a pop-up animation transition
            if (resultCard != null)
                StartCoroutine(ShowResultCardWithAnimation());
        }
    }

    // Coroutine to show the result card with pop-up animation
    private IEnumerator ShowResultCardWithAnimation()
    {
        if (resultCard != null)
        {
            resultCard.SetActive(true);
            CanvasGroup canvasGroup = resultCard.GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                float duration = 0.5f; // Animation duration
                float elapsedTime = 0f;
                Vector3 initialScale = resultCard.transform.localScale;
                Vector3 targetScale = Vector3.one; // Assuming the result card's final scale is (1, 1, 1)

                while (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    canvasGroup.alpha = Mathf.Lerp(0, 1, t);
                    resultCard.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                canvasGroup.alpha = 1;
                resultCard.transform.localScale = targetScale;
            }
        }
    }

    // Handle the close button click
    private void OnCloseButtonClick()
    {
        if (resultCard != null)
            resultCard.SetActive(false);

        // Hide the explore button
        if (exploreButton != null)
            exploreButton.gameObject.SetActive(false);
    }

    // Deactivate the current combination and reset models
    private void DeactivateCurrentCombination()
    {
        if (activeCombination != null)
        {
            activeCombination.combinedModel.SetActive(false); // Hide the combined model
            activeCombination.model1.SetActive(true);         // Show the first model
            activeCombination.model2.SetActive(true);         // Show the second model
        }

        // Hide the explore button
        if (exploreButton != null)
            exploreButton.gameObject.SetActive(false);

        // Hide the result card
        if (resultCard != null)
            resultCard.SetActive(false);

        // Reset active combination
        activeCombination = null;
    }

    // Helper method to check if both markers are visible
    private bool AreMarkersVisible(Transform marker1, Transform marker2)
    {
        return marker1.gameObject.activeInHierarchy && marker2.gameObject.activeInHierarchy;
    }
}
