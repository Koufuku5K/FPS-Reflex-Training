using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YoruFlash : MonoBehaviour
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
    Vector3 lastVelocity;
    private int bounceNum = 0;

    private Renderer rendererComponent;
    private Rigidbody rb;
    private bool isVisible = false;

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

        rendererComponent = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        SetVisibility(false);
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

        lastVelocity = rb.velocity;
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

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the desired object
        if (collision.gameObject.CompareTag("Wall") && bounceNum == 0)
        {
            // Make the GameObject visible
            SetVisibility(true);
            bounceNum += 1;

            // Calculate the Reflection of the Flash
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);

            coroutine = flashCharging(flashCharge);
            StartCoroutine(coroutine);
            Debug.Log("Flash Charge: " + flashCharge);
        }
    }

    private void SetVisibility(bool visible)
    {
        rendererComponent.enabled = visible;
        isVisible = visible;
    }
}
