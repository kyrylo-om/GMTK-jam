using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RhythmManager rhythmManager;


    public Vector2Int gridPosition = new Vector2Int(0, 0); // start at tile (0, 0)
    public float tileSize = 1.0f;
    private int gridSize = 4;
    public float beatWindow = 0.15f;

    void Start()
    {
        UpdatePosition();
    }

    void Update()
    {
        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.S)) direction = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.A)) direction = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.D)) direction = Vector2Int.right;

        float timeOffset = rhythmManager.GetTimeUntilNextBeat();

        if (timeOffset <= beatWindow)
        {
            if (direction != Vector2Int.zero)
            {
                Vector2Int newPos = gridPosition + direction;

                // Check bounds
                if (newPos.x >= 0 && newPos.x < gridSize && newPos.y >= 0 && newPos.y < gridSize)
                {
                    gridPosition = newPos;
                    UpdatePosition();
                }
            }
        }
        else
        {
            Debug.Log("Off beat!");
        }

    }

    void UpdatePosition()
    {
        transform.position = new Vector3(gridPosition.x * tileSize, 0, gridPosition.y * tileSize);
    }
}
