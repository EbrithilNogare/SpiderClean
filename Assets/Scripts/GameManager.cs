using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Action OnCleaningDone;
    public Action OnTimesUp;

    public Action OnFlyingRatSpawn;
    public Action OnFlowerPotFallingDown;
    public Action OnGreenGoblinAppears;

    public float cleaningProgress = 1.0f;
    public GameObject flyingRat;


}
