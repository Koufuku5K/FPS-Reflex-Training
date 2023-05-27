using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFlashInView : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject flashObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flashObject != null)
        {
            // Calculate the camera frustum planes
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

            // Check if the object is inside the camera view
            bool isInsideView = GeometryUtility.TestPlanesAABB(frustumPlanes, flashObject.GetComponent<Renderer>().bounds);

            if (isInsideView)
            {
                Debug.Log("Flashed!");
            }
            else
            {
                Debug.Log("Not Flashed!");
            }
        }
        else
        {
            Debug.Log("No flash object instantiated!");
        }
    }
}
