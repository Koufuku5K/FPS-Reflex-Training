using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoenixFlash : MonoBehaviour
{
    private IEnumerator coroutine;

    public Image flashScreen;
    public Image instantiatedFlashScreen;

    public FlashGenerator flashGenerator;
    public GameObject instantiatedFlash;

    private Camera mainCamera;
    private bool isInsideView = false;

    public bool isFlashed = false;
    public float flashDuration;
    public float flashCharge;
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

        coroutine = flashCharging(flashCharge);
        StartCoroutine(coroutine);
        Debug.Log("Flash Charge: " + flashCharge);
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

    void Flashed()
    {
        instantiatedFlashScreen = Instantiate(flashScreen);
        isFlashed = true;
    }

    private IEnumerator flashCharging(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (isInsideView)
        {
            Flashed();
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
