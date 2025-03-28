using DG.Tweening;
using UnityEngine;

public class CleanerController : MonoBehaviour
{
    public Transform centerPoint;
    public float radius;
    public float duration;

    void Start()
    {
        CircleTween();
    }

    void CircleTween()
    {
        transform.DOLocalPath(
            GenerateCirclePath(),
            duration,
            PathType.CatmullRom
        ).SetEase(Ease.InOutCubic);
    }

    Vector3[] GenerateCirclePath()
    {
        int points = 20;
        Vector3[] path = new Vector3[points];
        for (int i = 0; i < points; i++)
        {
            float angle = (i / (float)points) * Mathf.PI;
            path[i] = centerPoint.position + new Vector3(-Mathf.Cos(angle), -Mathf.Sin(angle), 0) * radius;
        }
        return path;
    }
}
