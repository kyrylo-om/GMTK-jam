using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public GameManager gameManager;

    public static event Action OnPlayerDeath;


    public Vector2Int gridPosition = new Vector2Int(0, 0); // start at tile (0, 0)
    public float tileSize = 1.0f;
    public float beatWindow = 0.0f;
    public bool wasMoved = false;
    public static event Action OnPlayerMove; // called when player moves


    void Start()
    {
        wasMoved = true;

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
        else if (Input.GetKeyDown(KeyCode.D)) direction = Vector2Int.up;

        if (!GameManager.isPlaying)
        {
            if (direction == Vector2Int.up)
            {
                // start level
                if (RhythmManager.canStartMoving)
                {
                    gridPosition += direction;
                    OnPlayerMove?.Invoke();
                    gameManager.StartLevel();
                }
                else
                {
                    direction = Vector2Int.zero;
                }
            }
            if (direction != Vector2Int.zero)
            {
                gridPosition += direction;
                UpdatePosition();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A)) direction = Vector2Int.down;

            if (direction != Vector2Int.zero && !wasMoved)
            {
                OnPlayerMove?.Invoke();
                if (RhythmManager.canMove)
                {
                    if (direction == Vector2Int.up && gridPosition.y == GameManager.currentLevel.GetComponent<GridManager>().gridSizeY + 1)
                    {
                        gridPosition += direction;
                    }

                    wasMoved = true;

                    gridPosition += direction;
                    UpdatePosition();

                }
                else
                {
                    Debug.Log("Too early!");
                    Death();
                }
            }
        }

    }

    void UpdatePosition()
    {
        transform.position = GameManager.currentLevel.transform.position + new Vector3(gridPosition.x * tileSize, 0, gridPosition.y * tileSize);
    }
    void Death()
    {
        OnPlayerDeath?.Invoke();
        gridPosition = new Vector2Int(0, 0);
        UpdatePosition();
        wasMoved = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Death();
        }
        if (other.CompareTag("Win"))
        {
            GameManager.currentLevel = other.transform.parent.parent.gameObject;
            gridPosition.y = 0;
            gameManager.Win();
        }
    }
}
