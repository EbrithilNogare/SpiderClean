using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    [Header("Per scene")]
    public DirtController dirtController;

    [Header("Global")]
    public float duration;
    public GameObject goblin;
    public GameObject tomato;
    public Sprite tomatoSprite;
    public Sprite tomatoSplashSprite;
    public Transform spawnPoint;
    public Transform despawnPoint;

    void Start()
    {
        StartCoroutine(goblinCoroutine());
    }

    IEnumerator goblinCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            SpwanGoblin();
        }
    }

    public void SpwanGoblin()
    {
        goblin.SetActive(true);
        goblin.transform.position = spawnPoint.position;

        goblin.transform.DOMove(despawnPoint.position, duration).SetEase(Ease.Linear).OnComplete(DespawnGoblin);
        DOVirtual.DelayedCall(duration * 6f / 10f, SpawnTomato);
    }

    private void SpawnTomato()
    {
        tomato.SetActive(true);
        tomato.transform.position = goblin.transform.position;

        SpriteRenderer spriteRenderer = tomato.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tomatoSprite;
        spriteRenderer.color = Color.white;

        Vector3 targetPosition = new Vector3(
            Random.Range(-5.5f, 5.5f),
            Random.Range(-4.5f, 1f),
            0f
        );

        Vector3 controlPoint = (tomato.transform.position + targetPosition) / 2 + Vector3.up * 3f; // Midpoint with height for the curve

        Sequence sequence = DOTween.Sequence();
        sequence.Append(tomato.transform.DOPath(new Vector3[] { tomato.transform.position, controlPoint, targetPosition }, 1f, PathType.CatmullRom)
            .SetEase(Ease.Linear))
            .OnComplete(SmashTomato);
    }

    private void DespawnGoblin()
    {
        goblin.SetActive(false);
    }
    private void SmashTomato()
    {
        SpriteRenderer spriteRenderer = tomato.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tomatoSplashSprite;
        dirtController.PutDirt(tomato.transform.position, 10f);
        spriteRenderer.DOFade(0f, 3f).OnComplete(() => tomato.SetActive(false));
    }
}
