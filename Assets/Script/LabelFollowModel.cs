using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelFollowModel : MonoBehaviour
{
    public Transform model; // The model the label should follow
    public Vector3 offset; // Offset position for the label

    void Update()
    {
        if (model != null)
        {
            // Set label's position relative to the model
            transform.position = model.position + offset;
            transform.LookAt(Camera.main.transform); // Ensure label faces the camera
        }
    }
}
