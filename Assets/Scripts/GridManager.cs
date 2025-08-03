using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject celesteTilePrefab;
    public GameObject springTilePrefab;
    public GameObject emptyTilePrefab;
    public GameObject startTile;
    public int gridSizeX = 5;
    public int gridSizeY;
    public static float tileSize = 2.5f;

    public Dictionary<GameObject, float> tileProbabilities;

    void Start()
    {
        tileProbabilities = new Dictionary<GameObject, float>()
        {
            { tilePrefab, 0.6f },
            { celesteTilePrefab, 0.2f },
            { springTilePrefab, 0.1f },
            { emptyTilePrefab, 0.1f }
        };
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
                GameObject tileToSpawn = null;
                float randomValue = Random.value;
                float cumulativeProbability = 0f;

                foreach (var kvp in tileProbabilities)
                {
                    cumulativeProbability += kvp.Value;
                    if (randomValue <= cumulativeProbability)
                    {
                        tileToSpawn = kvp.Key;
                        break;
                    }
                }

                if (tileToSpawn == null)
                    tileToSpawn = tilePrefab;

                GameObject newTile = Instantiate(tileToSpawn, transform);
                newTile.transform.localPosition = position;
                newTile.transform.Rotate(0, Random.Range(0, 5) * 90f, 0);

                if (tileToSpawn == celesteTilePrefab)
                {
                    newTile.GetComponent<CelesteTile>().active = Random.value < 0.5f;
                }
            }
        }
    }
}
