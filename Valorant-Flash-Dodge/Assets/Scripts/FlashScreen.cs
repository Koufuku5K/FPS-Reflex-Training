using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScreen : MonoBehaviour
{
    public bool isFlashed = false;
    public float flashDuration;
    public float flashTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashed)
        {
            flashTimer += Time.deltaTime;

            Debug.Log(flashTimer);
            
            if (flashTimer >= flashDuration)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
