using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RhythmManager rhythmManager;
    public GameManager gameManager;
    public MusicManager musicManager;
    public GameObject deathTextPrefab;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerMove; // called when player moves
    public static event Action OnPlayerBeat; // same as OnBeat but tied to player


    public Vector2Int gridPosition = new Vector2Int(0, 0); // start at tile (0, 0)
    public float tileSize = 1.0f;
    public float beatWindow = 0.0f;
    public bool wasMoved = false;
    public float jumpHeight = 1f;
    public float jumpDuration = 0.2f;
    public bool doubleJump = false;


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
                Instantiate(deathTextPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<DeathTextPopup>().SetText("Too late!");
                OnPlayerBeat?.Invoke();
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

        if (Input.GetKeyDown(KeyCode.W)) { 
            direction = Vector2Int.left;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S)) { 
            direction = Vector2Int.right;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D)) { 
            direction = Vector2Int.up;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (!GameManager.isPlaying)
        {
            if (direction == Vector2Int.up)
            {
                // start level
                if (RhythmManager.canStartMoving)
                {
                    wasMoved = true;
                    gridPosition += direction;
                    gameManager.StartLevel();
                    OnPlayerMove?.Invoke();
                    if (RhythmManager.beforeBeatWindow)
                    {
                        OnPlayerBeat?.Invoke();
                    }
                }
                else
                {
                    Instantiate(deathTextPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<DeathTextPopup>().SetText("Too early!");
                    direction = Vector2Int.zero;
                }
            }
            if (direction != Vector2Int.zero)
            {
                gridPosition += direction;
                if (doubleJump)
                {
                    gridPosition += direction;
                    PrepareDoubleJump();
                }
                UpdatePosition();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = Vector2Int.down;
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }

            if (direction != Vector2Int.zero && !wasMoved)
                {
                    OnPlayerMove?.Invoke();
                    if (RhythmManager.canMove)
                    {
                        OnPlayerBeat?.Invoke();
                        if (direction == Vector2Int.up && gridPosition.y == GameManager.currentLevel.GetComponent<GridManager>().gridSizeY + 1)
                        {
                            gridPosition += direction;
                        }

                        wasMoved = true;

                        gridPosition += direction;
                        if (doubleJump)
                        {
                            gridPosition += direction;
                            PrepareDoubleJump();
                        }
                        UpdatePosition();

                    }
                    else
                    {
                        Instantiate(deathTextPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<DeathTextPopup>().SetText("Too early!");
                        Death();
                    }
                }
        }

    }

    void UpdatePosition()
    {
        Vector3 destination = GameManager.currentLevel.transform.position + new Vector3(gridPosition.x * tileSize, 0, gridPosition.y * tileSize);
        StartCoroutine(Jump(destination));

    }
    void PrepareDoubleJump()
    {
        jumpDuration *= 2;
        jumpHeight *= 2;
    }
    void Death()
    {
        doubleJump = false;
        gridPosition = new Vector2Int(0, 0);
        UpdatePosition();
        OnPlayerDeath?.Invoke();
        wasMoved = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NoBlock"))
        {
            musicManager.Splash();
            Death();
        }
        if (other.CompareTag("ForbiddenEndTile"))
        {
            other.transform.parent.gameObject.GetComponent<EndTile>().animator.Play("End Block Forbidden");
            Death();
        }
        if (other.CompareTag("SpringTile"))
        {
            Vector3 rotation = other.transform.parent.rotation.eulerAngles;
            Vector2Int direction = Vector2Int.zero;
            if (rotation.y == 0) direction = Vector2Int.down;
            else if (rotation.y == 90) direction = Vector2Int.left;
            else if (rotation.y == 180) direction = Vector2Int.up;
            else if (rotation.y == 270) direction = Vector2Int.right;

            gridPosition += direction;
            UpdatePosition();
        }
        if (other.CompareTag("LaunchTile"))
        {
            doubleJump = true;
        }
        if (other.CompareTag("Win"))
            {
                GameManager.currentLevel = other.transform.parent.parent.gameObject;
                gridPosition.y = 0;
                gameManager.Win();
            }
    }

    public IEnumerator Jump(Vector3 destination)
    {
        float jumpProgress = 0f;
        bool startedWithDouble = doubleJump;
        doubleJump = false;
        Vector3 jumpStartPosition = transform.position;

        while (jumpProgress < 1f)
        {
            jumpProgress += Time.deltaTime / jumpDuration;
            float height = Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;
            transform.position = Vector3.Lerp(jumpStartPosition, destination, jumpProgress) + Vector3.up * height;
            yield return null;
        }
        transform.position = destination;
        if (startedWithDouble)
        {
            jumpDuration /= 2;
            jumpHeight /= 2;
        }
    }
}
