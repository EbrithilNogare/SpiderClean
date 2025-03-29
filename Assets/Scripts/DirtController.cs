using UnityEngine;

public class DirtController : MonoBehaviour
{
    [Header("Per scene")]
    public Transform cleaner;

    [Header("Global")]
    public Texture2D texture;
    private float size;
    private float smallSize = 5;
    private float bigSize = 15;

    public int dirtCount;
    public int startingDirtCount;

    void Start()
    {
        size = smallSize;
        dirtCount = startingDirtCount = texture.height * texture.width;
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, Color.black);
            }
        }
    }

    public void IncreaseSize()
    {
        size = bigSize;
    }

    /// <summary></summary>
    /// <param name="coor">Range 0 to 255</param>
    /// <param name="radius">in scaled pixels</param>
    public void PutDirt(Vector2 coor, float radius)
    {

        for (int y = (int)(coor.y - radius); y <= coor.y + radius; y++)
        {
            for (int x = (int)(coor.x - radius); x <= coor.x + radius; x++)
            {
                if (Vector2.Distance(coor, new Vector2(x, y)) < radius)
                    texture.SetPixel(x, y, Color.black);
            }
        }
    }

    void Update()
    {
        Vector2 cleanerPosition = new Vector2(cleaner.position.x, cleaner.position.y);
        Vector2 buildingPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 coor = new Vector2(
            (cleanerPosition.x - buildingPosition.x) * texture.width / transform.localScale.x + texture.width / 2,
            (cleanerPosition.y - buildingPosition.y) * texture.height / transform.localScale.y + texture.height / 2
        );
        Clean(coor);
    }

    private Vector2 lastCoor = new Vector2(-1000000, 0);
    public void Clean(Vector2 coor)
    {
        if (lastCoor.x == -1000000)
        {
            lastCoor = coor;
            return;
        }

        GetComponent<Renderer>().material.mainTexture = texture;
        int countOfXSize = (int)Mathf.Abs(lastCoor.x - coor.x) + 2;

        for (int y = (int)(coor.y - size); y < coor.y + size; y++)
        {
            for (int x = (int)(coor.x - countOfXSize); x < coor.x + countOfXSize; x++)
            {
                Color pixelColor = texture.GetPixel(x, y);
                if (pixelColor == Color.black)
                {
                    dirtCount--;
                    texture.SetPixel(x, y, Color.white);
                }
            }
        }
        texture.Apply();
        lastCoor = coor;
    }
}