using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    //public FlashGenerator flashGenerator;

    /*void Start()
    {
        flashGenerator = FindObjectOfType<FlashGenerator>();
    }*/

    public void StartGame()
    {
        /*flashGenerator.currentFlash = flashGenerator.phoenixFlash;
        flashGenerator.spawnInterval = Random.Range(2f, 10.0f);
        flashGenerator.spawnFlashes = WaitAndSpawn(flashGenerator.spawnInterval);
        StartCoroutine(flashGenerator.spawnFlashes);*/
        Cursor.lockState = CursorLockMode.Locked;
    }
}
