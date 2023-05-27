using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject instantiatedTargetObject;
    public float totalTarget = 0;

    float randomX;
    float randomY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTargets(targetObject);
    }

    void SpawnTargets(GameObject target)
    {
        while(totalTarget < 10)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomY = Random.Range(2f, 18f);

            instantiatedTargetObject = Instantiate(target, new Vector3(randomX, randomY, 28f), Quaternion.identity);
            totalTarget += 1;
        }
    }
}
