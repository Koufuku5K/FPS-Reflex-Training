using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float shootingRange = 100f;
    private LayerMask targetLayer;

    public TargetGenerator targetGenerator;

    // Start is called before the first frame update
    void Start()
    {
        targetLayer = LayerMask.GetMask("Target");

        targetGenerator = FindObjectOfType<TargetGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * shootingRange);
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.DrawRay(transform.position, transform.forward * shootingRange);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, shootingRange, targetLayer))
        {
            Object.Destroy(hit.collider.gameObject);
            targetGenerator.totalTarget -= 1;
        }
    }
}
