using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRatController : MonoBehaviour
{
    GameManager gameManager;

    public SpriteRenderer ratSprite;

    private float minYForSpawn = -4;
    private float maxYForSpawn = 2;

    private int xSpawnPosition = 10;

    private bool flyingFromLeft = false;
    private bool canBeDestroyed = false;
    
    private float flyingSpeed = 1.5f;

    public void Init()
    {
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log("FlyingRat Spawn.");
        
        //GENERATE SIDE
        int RandomNumber = UnityEngine.Random.Range(0, 2);
        float ySpawnPosition = UnityEngine.Random.Range(minYForSpawn, maxYForSpawn);
        if (RandomNumber == 0)
        {
            transform.position = new Vector3(xSpawnPosition, ySpawnPosition, 0);
            flyingFromLeft = false;
        }
        else
        {
            transform.position = new Vector3(-xSpawnPosition, ySpawnPosition, 0);
            ratSprite.flipX = true;
            flyingFromLeft = true;
        }
        
    }

    private void Update()
    {
        if (flyingFromLeft) 
        {
            float newXPosition = transform.position.x + flyingSpeed * Time.deltaTime;
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        }
        else
        {
            float newXPosition = transform.position.x - flyingSpeed * Time.deltaTime;
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
        
        if (transform.position.x < xSpawnPosition && transform.position.x > -xSpawnPosition)
        {
            canBeDestroyed = true;
        }
        else
        {
            if(canBeDestroyed)
                Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("FlyingRat OnCollisionEnter2D");
        gameManager.OnFlyingRatHit.Invoke();
        //Destroy(gameObject);
    }
}
