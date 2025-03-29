using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Action OnCleaningDone;
    public Action OnTimesUp;

    public Action OnFlowerPotFallingDown;
    public Action OnGreenGoblinAppears;

    public float cleaningProgress = 1.0f;
    public GameObject flyingRat;
    
    public UnityEvent OnFlyingRatHit;
    
    [Header("DEBUG")]
    public bool spawnFlyingRat = false;

    private void Start()
    {
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
