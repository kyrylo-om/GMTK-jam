using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static AudioSource waitSource;
    private static AudioSource playSource;
    public AudioClip waitMusic;
    public AudioClip playMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waitSource = gameObject.GetComponents<AudioSource>()[0];
        playSource = gameObject.GetComponents<AudioSource>()[1];

        RhythmManager.OnBeat += () =>
        {
            if (RhythmManager.beatNum % 4 == 0)
            {
                waitSource.PlayOneShot(waitMusic);
            }
            // if (playStartBeat != 0 && (RhythmManager.beatNum - playStartBeat) % 16 == 0)
            if (RhythmManager.beatNum % 16 == 0)
            {
                playSource.PlayOneShot(playMusic);
            }
        };

        PlayerController.OnPlayerDeath += () =>
        {
            StopMusic();
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartLevel()
    {
        ResumeMusic();
    }
    public void StopMusic()
    {
        StopCoroutine("ResumeMusicRoutine");
        StartCoroutine("StopMusicRoutine", playSource);
    }
    public void ResumeMusic()
    {
        StopCoroutine("StopMusicRoutine");
        StartCoroutine("ResumeMusicRoutine", playSource);
    }
    IEnumerator StopMusicRoutine(AudioSource audioSource)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    IEnumerator ResumeMusicRoutine(AudioSource audioSource)
    {
        while (audioSource.volume < 1)
        {
            audioSource.volume += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}
