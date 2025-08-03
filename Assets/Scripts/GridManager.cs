using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject celesteTilePrefab;
    public GameObject springTilePrefab;
    public GameObject emptyTilePrefab;
    public GameObject launchTilePrefab;
    public GameObject startTile;
    public int gridSizeX = 5;
    public int gridSizeY;
    public static float tileSize = 2.5f;

    public Dictionary<GameObject, float> tileProbabilities;

    void Start()
    {
        float baseProbability = Mathf.Max(0.3f, 0.9f - GameManager.levelCount * 0.08f);
        float otherTileProbability = (1f - baseProbability) / 5f;
        tileProbabilities = new Dictionary<GameObject, float>()
        {
            { tilePrefab, baseProbability },
            { celesteTilePrefab, otherTileProbability * 2 },
            { springTilePrefab, otherTileProbability },
            { emptyTilePrefab, otherTileProbability },
            { launchTilePrefab, otherTileProbability }
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
