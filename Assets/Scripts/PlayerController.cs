using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RhythmManager rhythmManager;

    public static event Action OnPlayerDeath;


    public Vector2Int gridPosition = new Vector2Int(0, 0); // start at tile (0, 0)
    public float tileSize = 1.0f;
    private int gridSize = 4;
    public float beatWindow = 0.0f;
    public bool wasMoved = false;


    void Start()
    {
        UpdatePosition();

        RhythmManager.OnBeat += () =>
        {

        };

        RhythmManager.OnBeatWindowEnd += () =>
        {
            if (!wasMoved)
            {
                Debug.Log("Too late!");
                Death();
            }
            else
            {
                wasMoved = false;
            }
        };

    }

    void Update()
    {
        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.S)) direction = Vector2Int.right;
        else if (Input.GetKeyDown(KeyCode.A)) direction = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.D)) direction = Vector2Int.up;

        if (direction != Vector2Int.zero && !wasMoved)
        {
            if (rhythmManager.canMove)
            {
                wasMoved = true;

                Vector2Int newPos = gridPosition + direction;

                gridPosition = newPos;
                UpdatePosition();
                
            }
            else
            {
                Debug.Log("Too early!");
                Death();
            }
        }
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(gridPosition.x * tileSize, 0, gridPosition.y * tileSize);
    }
    void Death()
    {
        OnPlayerDeath?.Invoke();
        gridPosition = Vector2Int.zero;
        UpdatePosition();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Death();
        }
    }
}
