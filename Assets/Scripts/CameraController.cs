using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float targetWidth = 1920f;
    private float targetHeight = 1080f;
    private float targetSize = 108f;

    private Camera camera;

    void Start()
    {
        camera = Camera.main;
        SetOrthographicSize();
    }

    void SetOrthographicSize()
    {
        float targetAspect = targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / (float)Screen.height;

        if (windowAspect >= targetAspect)
            camera.orthographicSize = targetHeight / 2f / targetSize;
        else
            camera.orthographicSize = (targetWidth / windowAspect) / 2f / targetSize;
    }

    void Update()
    {
        SetOrthographicSize();
    }
}
