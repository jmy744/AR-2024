using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this for UI elements

public class UserProfileController : MonoBehaviour
{
    public Button userProfileButton;     // Button to open user profile card
    public GameObject userProfileCard;   // Card design for user profile
    public Button femaleButton;          // Button for female student image
    public Button maleButton;            // Button for male student image
    public Button saveButton;            // Button to save the selected image
    public Button closeButton;           // Button to close the card
    public Image profilePicture;         // Image component for the profile picture
    public Sprite femaleSprite;          // Sprite for female student image
    public Sprite maleSprite;            // Sprite for male student image
    public Text successMessage;          // Text component for the success message

    private Sprite selectedSprite;       // Sprite selected by the user

    void Start()
    {
        // Ensure the user profile card is hidden initially
        if (userProfileCard != null)
            userProfileCard.SetActive(false);

        // Ensure the success message is hidden initially
        if (successMessage != null)
            successMessage.gameObject.SetActive(false);

        // Add listeners to buttons
        if (userProfileButton != null)
            userProfileButton.onClick.AddListener(OnUserProfileButtonClick);

        if (femaleButton != null)
            femaleButton.onClick.AddListener(OnFemaleButtonClick);

        if (maleButton != null)
            maleButton.onClick.AddListener(OnMaleButtonClick);

        if (saveButton != null)
            saveButton.onClick.AddListener(OnSaveButtonClick);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    // Handle user profile button click to open the card
    private void OnUserProfileButtonClick()
    {
        if (userProfileCard != null)
            userProfileCard.SetActive(true);
    }

    // Handle female button click to select the female image
    private void OnFemaleButtonClick()
    {
        selectedSprite = femaleSprite; // Set the selected sprite to female
    }

    // Handle male button click to select the male image
    private void OnMaleButtonClick()
    {
        selectedSprite = maleSprite; // Set the selected sprite to male
    }

    // Handle save button click to update the profile picture and show success message
    private void OnSaveButtonClick()
    {
        if (selectedSprite != null)
        {
            // Update the user profile button image
            if (userProfileButton != null)
            {
                userProfileButton.image.sprite = selectedSprite;
                userProfileButton.transform.localScale = new Vector3(3f, 3f, 3f); // Change the scale to 3
            }

            // Show the success message
            if (successMessage != null)
            {
                successMessage.text = "Profile Updated Successfully!"; // Set the success message
                successMessage.gameObject.SetActive(true); // Show the success message
                StartCoroutine(HideSuccessMessageWithDelay(2f)); // Hide after 2 seconds
            }
        }
    }

    // Coroutine to hide the success message after a delay
    private IEnumerator HideSuccessMessageWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (successMessage != null)
            successMessage.gameObject.SetActive(false);
    }

    // Handle close button click to close the card
    private void OnCloseButtonClick()
    {
        if (userProfileCard != null)
            userProfileCard.SetActive(false); // Hide the user profile card
    }
}
