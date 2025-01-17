using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ModelInstantiator : MonoBehaviour
{
    public GameObject modelPrefab; // Prefab for the atom (Hydrogen or Lithium)
    private GameObject modelInstance;

    public void OnTargetFound()
    {
        if (modelInstance == null)
        {
            modelInstance = Instantiate(modelPrefab, transform.position, transform.rotation);
            modelInstance.transform.SetParent(transform);
            modelInstance.SetActive(true);
        }
    }

    public void OnTargetLost()
    {
        if (modelInstance != null)
        {
            Destroy(modelInstance);
        }
    }
}
