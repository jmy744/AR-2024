using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionController : MonoBehaviour
{
    public GameObject hydrogenModel; // Reference to the Hydrogen 3D model
    public GameObject uraniumModel; // Reference to the Uranium 3D model
    public GameObject lithiumModel; // Reference to the Lithium 3D model
    public GameObject sodiumModel; // Reference to the Sodium 3D model
    public GameObject potassiumModel; // Reference to the Potassium 3D model
    public GameObject chromiumModel; // Reference to the Chromium 3D model

    public GameObject hydrogenLithiumModel; // Combined model for Hydrogen + Lithium
    public GameObject hydrogenSodiumModel; // Combined model for Hydrogen + Sodium
    // Add other combined models as needed

    public Button nextButton; // Reference to the Next button
    public Button combineButton; // Reference to the Combine button
    public Button mixAgainButton; // Reference to the Mix Again button
    public GameObject scrollView; // Reference to the Scroll View to hide
    public GameObject beaker; // Reference to the beaker GameObject

    private string selectedElement1; // Store the first selected element
    private string selectedElement2; // Store the second selected element

    void Start()
    {
        // Ensure buttons and models are initially set correctly
        hydrogenModel.SetActive(false);
        uraniumModel.SetActive(false);
        lithiumModel.SetActive(false);
        sodiumModel.SetActive(false);
        potassiumModel.SetActive(false);
        chromiumModel.SetActive(false);

        hydrogenLithiumModel.SetActive(false);
        hydrogenSodiumModel.SetActive(false);

        combineButton.gameObject.SetActive(false);
        mixAgainButton.gameObject.SetActive(false);

        // Assign the Next button to trigger the element selection
        nextButton.onClick.AddListener(OnNextButtonClick);

        // Assign the Combine button to trigger the reaction
        combineButton.onClick.AddListener(OnCombineReaction);

        // Assign the Mix Again button to reset the scene
        mixAgainButton.onClick.AddListener(OnMixAgainClick);

        // Check and log assignment of scrollView
        if (scrollView == null)
        {
            Debug.LogError("ScrollView object is not assigned.");
        }

        // Check and log assignment of beaker
        if (beaker == null)
        {
            Debug.Log("Beaker object is not assigned or has been removed.");
        }
    }

    private void OnNextButtonClick()
    {
        // Get selected elements from the SelectElement script
        selectedElement1 = SelectElement.GetFirstElementSymbol();
        selectedElement2 = SelectElement.GetSecondElementSymbol();

        // Show the appropriate 3D model based on the user's selection
        ShowSelectedModels(selectedElement1, selectedElement2);

        // Hide the grid view
        if (scrollView != null)
        {
            scrollView.SetActive(false);
        }

        // Hide the beaker if it exists
        if (beaker != null)
        {
            beaker.SetActive(false);
            Debug.Log("Beaker has been hidden.");
        }

        // Hide the Next button and show the Combine button
        nextButton.gameObject.SetActive(false);
        combineButton.gameObject.SetActive(true);
    }

    private void ShowSelectedModels(string element1, string element2)
    {
        // Deactivate all models first
        hydrogenModel.SetActive(false);
        uraniumModel.SetActive(false);
        lithiumModel.SetActive(false);
        sodiumModel.SetActive(false);
        potassiumModel.SetActive(false);
        chromiumModel.SetActive(false);

        // Activate the selected models
        if (element1 == "H" || element2 == "H")
        {
            hydrogenModel.SetActive(true);
            AdjustModel(hydrogenModel);
        }
        if (element1 == "U" || element2 == "U")
        {
            uraniumModel.SetActive(true);
            AdjustModel(uraniumModel);
        }
        if (element1 == "Li" || element2 == "Li")
        {
            lithiumModel.SetActive(true);
            AdjustModel(lithiumModel);
        }
        if (element1 == "Na" || element2 == "Na")
        {
            sodiumModel.SetActive(true);
            AdjustModel(sodiumModel);
        }
        if (element1 == "K" || element2 == "K")
        {
            potassiumModel.SetActive(true);
            AdjustModel(potassiumModel);
        }
        if (element1 == "Cr" || element2 == "Cr")
        {
            chromiumModel.SetActive(true);
            AdjustModel(chromiumModel);
        }
    }

    private void AdjustModel(GameObject model)
    {
        // Uniformly adjust scale to ensure model enlarges evenly
        float uniformScale = 2.0f; // Example scale factor
        model.transform.localScale = new Vector3(uniformScale, uniformScale, uniformScale);

        // Adjust position as needed to keep it within the AR camera view
        model.transform.position = new Vector3(0.0f, 0.0f, 0.5f); // Example position adjustment
    }

    private void OnCombineReaction()
    {
        // Hide individual models
        hydrogenModel.SetActive(false);
        uraniumModel.SetActive(false);
        lithiumModel.SetActive(false);
        sodiumModel.SetActive(false);
        potassiumModel.SetActive(false);
        chromiumModel.SetActive(false);

        // Show the combined model based on the selected elements
        ShowCombinedModel(selectedElement1, selectedElement2);

        // Hide the Combine button and show the Mix Again button
        combineButton.gameObject.SetActive(false);
        mixAgainButton.gameObject.SetActive(true);

        // Hide the grid view when the Combine button is clicked
        if (scrollView != null)
        {
            scrollView.SetActive(false);
        }
    }

    private void ShowCombinedModel(string element1, string element2)
    {
        // Deactivate all combined models first
        hydrogenLithiumModel.SetActive(false);
        hydrogenSodiumModel.SetActive(false);
        // Deactivate other combined models as needed

        // Activate the appropriate combined model based on the selection
        if ((element1 == "H" && element2 == "Li") || (element1 == "Li" && element2 == "H"))
        {
            hydrogenLithiumModel.SetActive(true);
            AdjustModel(hydrogenLithiumModel);
        }
        else if ((element1 == "H" && element2 == "Na") || (element1 == "Na" && element2 == "H"))
        {
            hydrogenSodiumModel.SetActive(true);
            AdjustModel(hydrogenSodiumModel);
        }
        // Add other combinations as needed
    }

    private void OnMixAgainClick()
    {
        // Reset the UI and return to the initial state
        hydrogenModel.SetActive(false);
        uraniumModel.SetActive(false);
        lithiumModel.SetActive(false);
        sodiumModel.SetActive(false);
        potassiumModel.SetActive(false);
        chromiumModel.SetActive(false);
        hydrogenLithiumModel.SetActive(false);
        hydrogenSodiumModel.SetActive(false);
        // Deactivate other combined models as needed

        combineButton.gameObject.SetActive(false);
        mixAgainButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);

        // Reset selections
        selectedElement1 = string.Empty;
        selectedElement2 = string.Empty;

        // Show the Scroll View for selecting elements
        if (scrollView != null)
        {
            scrollView.SetActive(true);
        }
    }
}
