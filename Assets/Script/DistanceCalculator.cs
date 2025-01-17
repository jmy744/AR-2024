using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public GameObject hydrogenPrefab; // Prefab for the Hydrogen model
    public GameObject lithiumPrefab; // Prefab for the Lithium model
    public GameObject combinedLiHPrefab; // Prefab for the combined LiH model

    private GameObject hydrogenInstance;
    private GameObject lithiumInstance;
    private GameObject combinedModelInstance;

    private static Transform hydrogenTransform;
    private static Transform lithiumTransform;

    public float distanceThreshold = 0.1f; // Distance threshold for showing the combined model

    void Update()
    {
        if (hydrogenTransform != null && lithiumTransform != null)
        {
            // Calculate the distance between the two targets
            float distance = Vector3.Distance(hydrogenTransform.position, lithiumTransform.position);

            if (distance < distanceThreshold)
            {
                ShowCombinedModel();
            }
            else
            {
                ShowIndividualModels();
            }
        }
    }

    public void OnHydrogenFound(GameObject hydrogenInstance, Transform hydrogenTrans)
    {
        this.hydrogenInstance = hydrogenInstance;
        hydrogenTransform = hydrogenTrans;

        // Show the Hydrogen model
        if (hydrogenInstance != null) hydrogenInstance.SetActive(true);
    }

    public void OnHydrogenLost()
    {
        hydrogenTransform = null;
        ShowIndividualModels();
    }

    public void OnLithiumFound(GameObject lithiumInstance, Transform lithiumTrans)
    {
        this.lithiumInstance = lithiumInstance;
        lithiumTransform = lithiumTrans;

        // Show the Lithium model
        if (lithiumInstance != null) lithiumInstance.SetActive(true);
    }

    public void OnLithiumLost()
    {
        lithiumTransform = null;
        ShowIndividualModels();
    }

    private void ShowCombinedModel()
    {
        // Hide individual models
        if (hydrogenInstance != null) hydrogenInstance.SetActive(false);
        if (lithiumInstance != null) lithiumInstance.SetActive(false);

        // Instantiate the combined model at the midpoint of the two targets
        Vector3 combinedPosition = (hydrogenTransform.position + lithiumTransform.position) / 2;
        Quaternion combinedRotation = Quaternion.LookRotation(lithiumTransform.position - hydrogenTransform.position);

        if (combinedModelInstance == null)
        {
            combinedModelInstance = Instantiate(combinedLiHPrefab, combinedPosition, combinedRotation);
        }
        else
        {
            combinedModelInstance.transform.position = combinedPosition;
            combinedModelInstance.transform.rotation = combinedRotation;
            combinedModelInstance.SetActive(true);
        }
    }

    private void ShowIndividualModels()
    {
        // Show individual models
        if (hydrogenInstance != null) hydrogenInstance.SetActive(true);
        if (lithiumInstance != null) lithiumInstance.SetActive(true);

        // Hide the combined model
        if (combinedModelInstance != null) combinedModelInstance.SetActive(false);
    }
}
