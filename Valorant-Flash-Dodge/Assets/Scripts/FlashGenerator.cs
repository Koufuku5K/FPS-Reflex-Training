using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashGenerator : MonoBehaviour
{
    float spawnInterval;
    float randomX;
    float randomY;

    // Current Type of Flash Being Generated
    public GameObject currentFlash;
    public GameObject phoenixFlash;
    public GameObject yoruFlash;
    public GameObject instantiatedFlash;
    enum flashDirection
    {
        Left,
        Right
    }
    private bool isGenerating = true;

    private IEnumerator spawnFlashes;

    private Rigidbody rb;
    public float curveForce = 600f;
    public float initialSpeed = 300f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        currentFlash = phoenixFlash;
        spawnInterval = Random.Range(2f, 10.0f);
        spawnFlashes = WaitAndSpawn(spawnInterval);
        StartCoroutine(spawnFlashes);
    }

    // Update is called once per frame
    void Update()
    {
        // Press Spacebar to stop spawning flash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Stop Spawning!");
            isGenerating = false;
        }

        // Press 1 on top of alphanumeric keyboard to spawn Phoenix flash
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Spawn Phoenix Flash
            Debug.Log("Phoenix Flash Spawn!");
            currentFlash = phoenixFlash;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Spawn Yoru Flash
            Debug.Log("Yoru Flash Spawn!");
            currentFlash = yoruFlash;
        }

        // Press Esc to pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }

    public void SpawnFlash(GameObject flash)
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(2f, 18f);
        float randomZ = Random.Range(20f, 28f);

        instantiatedFlash = Instantiate(flash, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
        
        if (flash == phoenixFlash)
        {
            Curve(instantiatedFlash);
        }
        else if (flash == yoruFlash)
        {
            Bounce(instantiatedFlash);
        }
    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (isGenerating)
        {
            yield return new WaitForSeconds(waitTime);
            SpawnFlash(currentFlash);
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
                rb.AddForce(-transform.forward * curveForce, ForceMode.Impulse);
                break;
            case flashDirection.Right:
                rb.velocity = transform.right * initialSpeed;
                rb.AddForce(-transform.forward * curveForce, ForceMode.Impulse);
                break;
        }
    }

    void Bounce(GameObject flashObject)
    {
        Rigidbody rb = flashObject.GetComponent<Rigidbody>();

        Vector3 randomDirection = Random.onUnitSphere;

        rb.velocity = randomDirection * initialSpeed;
    }
}
