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
        RhythmManager.OnBeat += () =>
        {
            if (!audioSource.isPlaying && audioSource.clip == waitTrack)
            {
                audioSource.Play();
            }
        };

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
        audioSource.clip = playTrack;
        audioSource.Play();
        rhythmManager.StartLevel();
    }

    void Death()
    {
        isPlaying = false;
        audioSource.clip = waitTrack;
    }

    public void Win()
    {
        audioSource.loop = false;
        isPlaying = false;
    }
}
