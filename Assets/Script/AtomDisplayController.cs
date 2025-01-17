using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomDisplayController : MonoBehaviour
{
    public GameObject atomPrefab; // Drag and drop your Atom 3D model prefab
    public GameObject resultPrefab; // Drag and drop your final result model prefab
    public Button combineButton; // Button to combine selected elements

    private GameObject firstAtomInstance;
    private GameObject secondAtomInstance;
    private GameObject resultInstance;

    private string firstElementSymbol;
    private string secondElementSymbol;
    private bool isFirstElementSelected = false;

    void Start()
    {
        combineButton.gameObject.SetActive(false);
        combineButton.onClick.AddListener(OnCombineReaction);
    }

    public void OnElementSelected(string elementSymbol)
    {
        if (!isFirstElementSelected)
        {
            if (firstAtomInstance != null)
            {
                Destroy(firstAtomInstance);
            }

            firstElementSymbol = elementSymbol;
            firstAtomInstance = Instantiate(atomPrefab, transform.position + Vector3.left * 2, Quaternion.identity);
            AtomVisualizer atomVisualizer1 = firstAtomInstance.GetComponent<AtomVisualizer>();
            if (atomVisualizer1 != null)
            {
                atomVisualizer1.UpdateAtom(elementSymbol);
            }

            isFirstElementSelected = true;
        }
        else
        {
            if (secondAtomInstance != null)
            {
                Destroy(secondAtomInstance);
            }

            secondElementSymbol = elementSymbol;
            secondAtomInstance = Instantiate(atomPrefab, transform.position + Vector3.right * 2, Quaternion.identity);
            AtomVisualizer atomVisualizer2 = secondAtomInstance.GetComponent<AtomVisualizer>();
            if (atomVisualizer2 != null)
            {
                atomVisualizer2.UpdateAtom(elementSymbol);
            }

            combineButton.gameObject.SetActive(true);
        }
    }

    private void OnCombineReaction()
    {
        if (resultInstance != null)
        {
            Destroy(resultInstance);
        }

        resultInstance = Instantiate(resultPrefab, transform.position + Vector3.forward * 2, Quaternion.identity);
        combineButton.gameObject.SetActive(false);
        ResetSelection();
    }

    private void ResetSelection()
    {
        isFirstElementSelected = false;

        if (firstAtomInstance != null)
        {
            Destroy(firstAtomInstance);
        }
        if (secondAtomInstance != null)
        {
            Destroy(secondAtomInstance);
        }
        if (resultInstance != null)
        {
            Destroy(resultInstance);
        }
    }
}

// AtomVisualizer class for updating atom appearance
public class AtomVisualizer : MonoBehaviour
{
    public Transform nucleus;

    public void UpdateAtom(string elementSymbol)
    {
        // Assuming each atom has a specific number of electrons,
        // you might want to dynamically update the number of electrons based on the elementSymbol
        int atomicNumber = GetAtomicNumber(elementSymbol);

        foreach (Transform child in nucleus)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < atomicNumber; i++)
        {
            Vector3 electronPosition = GetElectronPosition(i, atomicNumber);
            GameObject electron = Instantiate(Resources.Load<GameObject>("ElectronPrefab"), nucleus);
            electron.transform.localPosition = electronPosition;
        }
    }

    private int GetAtomicNumber(string elementSymbol)
    {
        // Return the atomic number based on the element symbol
        // For example, H -> 1, Na -> 11
        Dictionary<string, int> atomicNumbers = new Dictionary<string, int>
        {
            { "H", 1 },
            { "Na", 11 }
            // Add more elements as needed
        };

        if (atomicNumbers.TryGetValue(elementSymbol, out int atomicNumber))
        {
            return atomicNumber;
        }
        return 0; // Default to 0 if not found
    }

    private Vector3 GetElectronPosition(int index, int atomicNumber)
    {
        float angle = index * Mathf.PI * 2 / atomicNumber;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }
}
