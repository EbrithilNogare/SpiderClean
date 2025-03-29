using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FlowerPotManager : MonoBehaviour
{
    [Header("Per scene")]
    public Transform cleaner;

    [Header("Global")]
    public FlyingFlowerController flyingFlowerController;
    public GameObject flowerPot;
    public Transform startPoint;
    public Transform endPoint;

    private float duration = 3f;

    void Start()
    {
        StartCoroutine(FlowerPotCoroutine());
        flyingFlowerController.player = cleaner.GetComponent<CleanerController>();
        flyingFlowerController.flowerPotManager = this;
    }

    IEnumerator FlowerPotCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(16f);
            SpawnFlowerPot();
        }
    }

    public void SpawnFlowerPot()
    {
        flowerPot.SetActive(true);
        Vector3 randomPosition = Vector3.Lerp(startPoint.position, endPoint.position, Random.Range(0f, 1f));
        flowerPot.transform.position = randomPosition;
        flowerPot.transform.DOMove(randomPosition - new Vector3(0, 10f, 0), duration).SetEase(Ease.InQuad).OnComplete(DespawnFlowerPot);
    }

    public void DespawnFlowerPot()
    {
        flowerPot.transform.DOKill();
        flowerPot.SetActive(false);
    }
}
