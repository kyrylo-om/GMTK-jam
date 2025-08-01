using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject EndBlock;
    public int gridSizeX = 5;
    public int gridSizeY = 8;
    public float tileSize = 1.0f;

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        Vector3 position = new Vector3((0) * tileSize, -1, (gridSizeY) * tileSize);
        Instantiate(EndBlock, position, Quaternion.identity, transform);


        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                position = new Vector3((x - 2) * tileSize, 0, y * tileSize);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
