using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject startTile;
    public int gridSizeX = 5;
    public int gridSizeY;
    public static float tileSize = 2.5f;

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        // Vector3 position = new Vector3((0) * tileSize, -1, (gridSizeY + 3) * tileSize);
        // Instantiate(EndBlock, transform).transform.localPosition = position;


        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = new Vector3((x - 2) * tileSize, 0, (y + 2) * tileSize);
                Instantiate(tilePrefab, transform).transform.localPosition = position;
            }
        }
    }
}
