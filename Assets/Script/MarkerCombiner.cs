using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCombiner : MonoBehaviour
{
    public GameObject hydrogenPrefab; // Prefab for the Hydrogen model
    public GameObject lithiumPrefab; // Prefab for the Lithium model
    public GameObject combinedModelPrefab; // Prefab for the combined LiH model

    private GameObject hydrogenInstance;
    private GameObject lithiumInstance;
    private GameObject combinedModelInstance;

    private static bool hydrogenDetected = false;
    private static bool lithiumDetected = false;
    private static Transform hydrogenTransform;
    private static Transform lithiumTransform;

    void Update()
    {
        if (hydrogenDetected && lithiumDetected)
        {
            // Check proximity or alignment condition
            if (Vector3.Distance(hydrogenTransform.position, lithiumTransform.position) < 0.1f) // Adjust the distance threshold as needed
            {
                ShowCombinedModel();
            }
        }
    }

    public void OnHydrogenFound(GameObject hydrogenInstance, Transform hydrogenTrans)
    {
        hydrogenDetected = true;
        this.hydrogenInstance = hydrogenInstance;
        hydrogenTransform = hydrogenTrans;
    }

    public void OnHydrogenLost()
    {
        hydrogenDetected = false;
        hydrogenTransform = null;
        RemoveCombinedModel();
    }

    public void OnLithiumFound(GameObject lithiumInstance, Transform lithiumTrans)
    {
        lithiumDetected = true;
        this.lithiumInstance = lithiumInstance;
        lithiumTransform = lithiumTrans;
    }

    public void OnLithiumLost()
    {
        lithiumDetected = false;
        lithiumTransform = null;
        RemoveCombinedModel();
    }

    private void ShowCombinedModel()
    {
        // Hide individual models
        if (hydrogenInstance != null) hydrogenInstance.SetActive(false);
        if (lithiumInstance != null) lithiumInstance.SetActive(false);

        // Instantiate the combined model at the midpoint of the two markers
        Vector3 combinedPosition = (hydrogenTransform.position + lithiumTransform.position) / 2;
        Quaternion combinedRotation = Quaternion.LookRotation(lithiumTransform.position - hydrogenTransform.position);

        if (combinedModelInstance == null)
        {
            combinedModelInstance = Instantiate(combinedModelPrefab, combinedPosition, combinedRotation);
        }
        else
        {
            combinedModelInstance.transform.position = combinedPosition;
            combinedModelInstance.transform.rotation = combinedRotation;
            combinedModelInstance.SetActive(true);
        }
    }

    private void RemoveCombinedModel()
    {
        if (combinedModelInstance != null)
        {
            combinedModelInstance.SetActive(false);
        }
    }
}
