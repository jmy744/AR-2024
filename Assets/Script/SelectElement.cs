using UnityEngine;
using UnityEngine.UI;

public class SelectElement : MonoBehaviour
{
    public Image borderImage; // Reference to the border image component
    public Button startReactionButton; // Reference to the Start Reaction button assigned in the Inspector
    public int elementNumber; // Number associated with the element
    public string elementSymbol; // Symbol associated with the element (e.g., H, Fe)
    private static Button staticStartReactionButton; // Static reference to the Start Reaction button
    private bool isSelected = false;
    public static int selectedCount = 0; // Public for tracking the number of selected elements
    private static SelectElement firstSelected;
    private static SelectElement secondSelected;

    void Start()
    {
        // Ensure the border is initially hidden
        if (borderImage != null)
        {
            borderImage.enabled = false;
            Debug.Log($"[Start] {elementSymbol} borderImage.enabled = {borderImage.enabled}");
        }

        // Ensure the start reaction button is initially hidden
        if (startReactionButton != null)
        {
            staticStartReactionButton = startReactionButton;
            staticStartReactionButton.gameObject.SetActive(false);
            Debug.Log("[Start] startReactionButton.gameObject.SetActive(false)");
        }
    }

    public void OnButtonClick()
    {
        if (isSelected)
        {
            Deselect();
        }
        else if (selectedCount < 2)
        {
            Select();
        }

        CheckSelection();
    }

    private void Select()
    {
        isSelected = true;
        if (borderImage != null)
        {
            borderImage.enabled = true; // Show the border image
            Debug.Log($"[Select] {elementSymbol} borderImage.enabled = {borderImage.enabled}");
        }
        selectedCount++;

        if (firstSelected == null)
        {
            firstSelected = this;
        }
        else if (secondSelected == null)
        {
            secondSelected = this;
        }
    }

    private void Deselect()
    {
        isSelected = false;
        if (borderImage != null)
        {
            borderImage.enabled = false; // Hide the border image
            Debug.Log($"[Deselect] {elementSymbol} borderImage.enabled = {borderImage.enabled}");
        }
        selectedCount--;

        if (firstSelected == this)
        {
            firstSelected = null;
        }
        else if (secondSelected == this)
        {
            secondSelected = null;
        }
    }

    private void CheckSelection()
    {
        if (selectedCount == 2 && staticStartReactionButton != null)
        {
            staticStartReactionButton.gameObject.SetActive(true); // Show start reaction button
            Debug.Log("[CheckSelection] staticStartReactionButton.gameObject.SetActive(true)");
        }
        else if (staticStartReactionButton != null)
        {
            staticStartReactionButton.gameObject.SetActive(false); // Hide start reaction button
            Debug.Log("[CheckSelection] staticStartReactionButton.gameObject.SetActive(false)");
        }
    }

    public static int GetFirstElementNumber()
    {
        return firstSelected != null ? firstSelected.elementNumber : 0;
    }

    public static int GetSecondElementNumber()
    {
        return secondSelected != null ? secondSelected.elementNumber : 0;
    }

    public static string GetFirstElementSymbol()
    {
        return firstSelected != null ? firstSelected.elementSymbol : "";
    }

    public static string GetSecondElementSymbol()
    {
        return secondSelected != null ? secondSelected.elementSymbol : "";
    }

    public static void SetStartReactionButton(Button button)
    {
        staticStartReactionButton = button;
        if (staticStartReactionButton != null)
        {
            staticStartReactionButton.gameObject.SetActive(false); // Hide button initially
            Debug.Log("[SetStartReactionButton] staticStartReactionButton.gameObject.SetActive(false)");
        }
    }
}
