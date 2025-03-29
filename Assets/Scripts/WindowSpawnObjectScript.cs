using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSpawnObjectScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"{collision.gameObject.name} interacts with {gameObject.name}!");

        transform.parent.GetComponent<WindowScript>().OnObjectCollected(gameObject);
    }
}
