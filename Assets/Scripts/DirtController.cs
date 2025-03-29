using UnityEngine;

public class DirtController : MonoBehaviour
{
    public Texture2D texture;
    public Transform cleaner;
    private Vector2 size = new Vector2(3, 5);

    void Start()
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
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

    public void Clean(Vector2 coor)
    {
        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = (int)(coor.y - size.y); y < coor.y + size.y; y++)
        {
            for (int x = (int)(coor.x - size.x); x < coor.x + size.x; x++)
            {
                texture.SetPixel(x, y, Color.red);
            }
        }
        texture.Apply();
    }
}