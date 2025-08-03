using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static AudioSource waitSource;
    private static AudioSource playSource;
    public AudioSource effectsSource;
    public AudioClip waitMusic;
    public AudioClip playMusic;
    public AudioClip splashSound;
    public AudioClip springSound;
    public AudioClip launchSound;
    public AudioClip flowerSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waitSource = gameObject.GetComponents<AudioSource>()[0];
        playSource = gameObject.GetComponents<AudioSource>()[1];
        effectsSource = gameObject.GetComponents<AudioSource>()[4];
        waitSource.PlayOneShot(waitMusic);
        playSource.PlayOneShot(playMusic);

        RhythmManager.OnBeat += () =>
        {
            if (RhythmManager.beatNum % 16 == 0)
            {
                waitSource.PlayOneShot(waitMusic);
            }
            // if (playStartBeat != 0 && (RhythmManager.beatNum - playStartBeat) % 16 == 0)
            if (RhythmManager.beatNum % 96 == 0)
            {
                playSource.PlayOneShot(playMusic);
            }
        };

        PlayerController.OnPlayerDeath += () =>
        {
            StopMusic();
        };

        // Invoke("DelayedStart", 0.1f);
    }
    void DelayedStart()
    {
        waitSource.Stop();
        playSource.Stop();
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

    public void Splash()
    {
        effectsSource.PlayOneShot(splashSound);
    }
}
