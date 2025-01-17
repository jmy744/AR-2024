using UnityEngine;
using UnityEngine.UI;

public class StepByStepAnimation : MonoBehaviour
{
    public Animator animator;          // The Animator for the 3D model
    public Button nextButton;          // Button to trigger the next animation
    public string[] animationClips;    // Array of animation clip names to play in sequence
    private int currentAnimationIndex = 0;  // Tracks the current animation in the sequence

    void Start()
    {
        // Initially, make sure the button is hooked up
        nextButton.onClick.AddListener(OnNextButtonClicked);

        // Start by playing the first animation
        PlayAnimation(currentAnimationIndex);
    }

    void OnNextButtonClicked()
    {
        // If there are more animations, proceed to the next one
        if (currentAnimationIndex < (animationClips.Length - 1))
        {
            currentAnimationIndex++;
            PlayAnimation(currentAnimationIndex);
        }
        else
        {
            Debug.Log("All animations completed.");
            // Optionally, you can disable the button after the last animation
            nextButton.interactable = false;
        }
    }

    void PlayAnimation(int index)
    {
        // Check that the index is within bounds
        if (index >= 0 && index < animationClips.Length)
        {
            // Play the animation from the Animator
            animator.Play(animationClips[index]);
            Debug.Log("Playing animation: " + animationClips[index]);
        }
    }
}
