using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Adjust speed to control movement rate
    public float resetPosition = -10f; // Position where the object resets
    public float startPosition = 10f; // Initial start position

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime; // Move left continuously

        // Reset position to create an infinite effect
        if (transform.position.x <= resetPosition)
        {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
    }
}
