using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip playTrack;
    public AudioClip waitTrack;
    public AudioSource audioSource;
    public RhythmManager rhythmManager;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            audioSource.clip = playTrack;
            audioSource.Play();
            rhythmManager.StartLevel();
        }
    }

}
