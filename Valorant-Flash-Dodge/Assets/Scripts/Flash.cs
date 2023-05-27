using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    private IEnumerator coroutine;

    public Image flashScreen;
    public Image instantiatedFlashScreen;

    public FlashGenerator flashGenerator;
    public GameObject instantiatedFlash;

    private Camera mainCamera;
    private bool isInsideView = false;

    public bool isFlashed = false;
    public float flashDuration = 1.5f;
    public float flashTimer = 0f;

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();

        if (mainCamera == null)
        {
            Debug.LogError("No camera found in the scene!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        flashGenerator = FindObjectOfType<FlashGenerator>();
        instantiatedFlash = flashGenerator.instantiatedFlash;

        // Charging time before the flash flashes.
        float flashCharge = 0.5f;

        coroutine = flashCharging(flashCharge);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        CheckFlashInView();

        if (isFlashed)
        {
            flashTimer += Time.deltaTime;

            Debug.Log(flashTimer);

            if (flashTimer >= flashDuration)
            {
                isFlashed = false;
                Destroy(instantiatedFlashScreen.gameObject);
                Destroy(instantiatedFlash);
            }
        }
    }

    void Flashing()
    {
        instantiatedFlashScreen = Instantiate(flashScreen);
        isFlashed = true;
    }

    private IEnumerator flashCharging(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (isInsideView)
        {
            Flashing();
            Debug.Log("Flashed!");
        }
        else
        {
            Destroy(instantiatedFlash);
            Debug.Log("Not Flashed!");
        }
    }

    void CheckFlashInView()
    {
        // Calculate the camera frustum planes
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // Check if the object is inside the camera view
        isInsideView = GeometryUtility.TestPlanesAABB(frustumPlanes, instantiatedFlash.GetComponent<Renderer>().bounds);
    }
}
