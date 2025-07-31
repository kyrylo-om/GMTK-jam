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
        rhythmManager.OnBeat += () =>
        {
            if (!wasMoved)
            {
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
        Debug.Log(wasMoved);
        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.S)) direction = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.A)) direction = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.D)) direction = Vector2Int.right;

        if (direction != Vector2Int.zero)
        {
            float nextBeat = rhythmManager.nextBeatTime;
            float previousBeat = rhythmManager.nextBeatTime - rhythmManager.beatInterval;
            float currentTime = (float)AudioSettings.dspTime;

            if (currentTime + beatWindow >= nextBeat || currentTime - beatWindow <= previousBeat)
            {
                Vector2Int newPos = gridPosition + direction;

                gridPosition = newPos;
                UpdatePosition();
                wasMoved = true;
            }
            else
            {
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
