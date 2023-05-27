using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashGenerator : MonoBehaviour
{
    float spawnInterval;
    float randomX;
    float randomY;

    public GameObject flash;
    public GameObject instantiatedFlash;
    enum flashDirection
    {
        Left,
        Right
    }
    public KeyCode stopKey = KeyCode.Space;
    private bool isGenerating = true;

    private IEnumerator spawnFlashes;

    private Rigidbody rb;
    public float curveForce = 600f;
    public float initialSpeed = 300f;

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = Random.Range(1.5f, 10.0f);
        spawnFlashes = WaitAndSpawn(spawnInterval);
        StartCoroutine(spawnFlashes);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(stopKey))
        {
            Debug.Log("Stop Spawning!");
            isGenerating = false;
        }
    }

    public void SpawnFlash()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(2f, 18f);
        float randomZ = Random.Range(20f, 28f);

        instantiatedFlash = Instantiate(flash, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
        Curve(instantiatedFlash);
    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (isGenerating)
        {
            yield return new WaitForSeconds(waitTime);
            SpawnFlash();
        }
    }

    void Curve(GameObject flashObject)
    {
        Rigidbody rb = flashObject.GetComponent<Rigidbody>();
        int randomNumber = Random.Range(0, 2);
        flashDirection selectedDirection = (flashDirection)randomNumber;

        // Randomly selects a direction for the flash to go.
        switch (selectedDirection)
        {
            case flashDirection.Left:
                rb.velocity = -transform.right * initialSpeed;
                rb.AddForce(-transform.forward * curveForce, ForceMode.Force);
                break;
            case flashDirection.Right:
                rb.velocity = transform.right * initialSpeed;
                rb.AddForce(-transform.forward * curveForce, ForceMode.Force);
                break;
        }
    }
}
