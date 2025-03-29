using System;
using UnityEngine;

public class FlyingFlowerController : MonoBehaviour
{
    [NonSerialized] public CleanerController player;
    [NonSerialized] public FlowerPotManager flowerPotManager;


    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1f)
        {
            flowerPotManager.DespawnFlowerPot();
            TriggerEnter();
        }
    }

    private void TriggerEnter()
    {
        player.GotHit();
    }
}
