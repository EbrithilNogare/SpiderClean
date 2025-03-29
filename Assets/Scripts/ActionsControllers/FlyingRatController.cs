using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
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
    
    private Tween fallTween;
    private bool falling = false;
    private CleanerController player;

    public void Init()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<CleanerController>();
        
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
        if (Vector3.Distance(transform.position, player.transform.position) < 1f && !falling)
        {
            falling = true;
            TriggerEnter();
        }
        
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

        if (transform.position.y < -8f )
        {
            KillFall();
        }
        
    }

    private void TriggerEnter()
    {
        Debug.Log("FlyingRat OnCollisionEnter2D");
        gameManager.OnFlyingRatHit.Invoke();
        StartFall();
    }

    public void StartFall()
    {
        // Kill any existing tween before starting a new one
        if (fallTween != null) fallTween.Kill();

        // Move down (-5 in Y) over 2 seconds and rotate during the fall
        fallTween = transform.DOMoveY(transform.position.y - 5, 2f)
            .SetEase(Ease.InQuad)
            .OnComplete(() => Debug.Log("Fall Completed"));

        // Rotate while falling
        transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);
    }

    public void KillFall()
    {
        if (fallTween != null) fallTween.Kill();
        Debug.Log("Tween Killed");
        Destroy(gameObject);
    }
}
