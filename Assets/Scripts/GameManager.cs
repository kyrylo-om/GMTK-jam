using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip playTrack;
    public AudioClip waitTrack;
    public RhythmManager rhythmManager;
    private MusicManager musicManager;
    public CameraManager cameraManager;
    public GameObject levelPrefab;
    public static GameObject currentLevel;
    public static GridManager gridManager;

    public static bool isPlaying = false;

    void Start()
    {
        musicManager = GetComponent<MusicManager>();

        currentLevel = Instantiate(levelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        gridManager = currentLevel.GetComponent<GridManager>();
        gridManager.startTile.GetComponent<EndTile>().Activate(false);
        gridManager.gridSizeY = Random.Range(6, 10);

        NewLevel(new Vector3(0, 0, (gridManager.gridSizeY + 3) * GridManager.tileSize));

        Application.targetFrameRate = 60;
        PlayerController.OnPlayerDeath += () =>
        {
            Death();
        };
    }

    void Update()
    {

    }

    public void StartLevel()
    {
        isPlaying = true;
        musicManager.StartLevel();
        rhythmManager.StartLevel();
    }

    void Death()
    {
        isPlaying = false;
    }

    public void Win()
    {
        // this function is executed after currentLevel was set to new one
        isPlaying = false;
        StartCoroutine(cameraManager.MoveCamera(gridManager.gridSizeY + 3, 2));

        gridManager = currentLevel.GetComponent<GridManager>();
        gridManager.startTile.GetComponent<EndTile>().Activate(true);
        NewLevel(new Vector3(0, 0, currentLevel.transform.position.z + (gridManager.gridSizeY + 3) * GridManager.tileSize));
        musicManager.StopMusic();
    }

    public void NewLevel(Vector3 position)
    {
        GameObject newLevel = Instantiate(levelPrefab, position, Quaternion.identity);
        newLevel.GetComponent<GridManager>().gridSizeY = Random.Range(6, 10);
    }
}
