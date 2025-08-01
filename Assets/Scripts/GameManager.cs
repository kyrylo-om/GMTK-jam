using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip playTrack;
    public AudioClip waitTrack;
    public AudioSource audioSource;
    public RhythmManager rhythmManager;
    public static bool isPlaying = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        PlayerController.OnPlayerDeath += () =>
        {
            Death();
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !isPlaying)
        {
            StartLevel();
            isPlaying = true;
        }
    }

    void StartLevel()
    {
        audioSource.clip = playTrack;
        audioSource.Play();
        rhythmManager.StartLevel();
    }

    void Death()
    {
        isPlaying = false;
        audioSource.clip = waitTrack;
        audioSource.Play();
    }

}
