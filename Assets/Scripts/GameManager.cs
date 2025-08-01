using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip playTrack;
    public AudioClip waitTrack;
    public RhythmManager rhythmManager;
    private MusicManager musicManager;
    public CameraManager cameraManager;
    
    public static bool isPlaying = false;

    void Start()
    {
        musicManager = GetComponent<MusicManager>();

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
        isPlaying = false;
        musicManager.StopMusic();
        StartCoroutine(cameraManager.MoveCamera(11, 2));
    }
}
