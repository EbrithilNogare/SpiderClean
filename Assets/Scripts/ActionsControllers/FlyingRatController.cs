using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRatController : MonoBehaviour
{
    GameManager gameManager;
    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log("FlyingRat Spawn.");
        
    }
}
