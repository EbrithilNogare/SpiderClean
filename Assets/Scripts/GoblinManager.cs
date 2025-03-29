using DG.Tweening;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject goblin;
    public Transform spawnPoint;
    public Transform despawnPoint;
    public float duration;

    void Start()
    {

    }

    public void SpwanGoblin()
    {
        goblin.SetActive(true);
        goblin.transform.position = spawnPoint.position;
        goblin.transform.DOMove(despawnPoint.position, duration);
    }
}
