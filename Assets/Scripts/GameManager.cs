using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 4;
    public float tileSize = 1.0f;

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
