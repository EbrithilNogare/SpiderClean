using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Action OnCleaningDone;
    public Action OnTimesUp;

    public Action OnFlowerPotFallingDown;
    public Action OnGreenGoblinAppears;

    public float cleaningProgress = 1.0f;
    public GameObject flyingRat;
    [FormerlySerializedAs("spawnInterval")] public float flyingRat_spawnInterval = 10f; // Time between spawns
    [FormerlySerializedAs("spawnChance")] public float FlyingRatspawnChance = 0.6f; 
    
    public UnityEvent OnFlyingRatHit;
    
    [Header("DEBUG")]
    public bool spawnFlyingRat = false;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flyingRat_spawnInterval); // Wait 10 seconds

            if (Random.value < FlyingRatspawnChance) // 60% probability (Random.value returns 0-1)
            {
                SpawnFlyingRat();
                Debug.Log("Spawned an object!");
            }
            else
            {
                Debug.Log("No spawn this time.");
            }
        }
    }

    private void Update()
    {
        if (spawnFlyingRat)
        {
            spawnFlyingRat = false;
            SpawnFlyingRat();
        }
    }

    public void SpawnFlyingRat()
    {
        var newflyingRat = Instantiate(flyingRat);
        newflyingRat.GetComponent<FlyingRatController>().Init();
        spawnFlyingRat = false;
    }
    
    


}
