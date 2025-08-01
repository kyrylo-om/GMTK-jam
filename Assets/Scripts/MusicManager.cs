using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static AudioSource waitSource;
    private static AudioSource playSource;
    private Coroutine stopMusic;
    private Coroutine resumeMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waitSource = gameObject.GetComponents<AudioSource>()[0];
        playSource = gameObject.GetComponents<AudioSource>()[1];

        RhythmManager.OnBeat += () =>
        {
            if (RhythmManager.beatNum % 4 == 0)
            {
                Debug.Log("Play!");
                Debug.Log(RhythmManager.beatNum);
                waitSource.Play();
            }
            // if (playStartBeat != 0 && (RhythmManager.beatNum - playStartBeat) % 16 == 0)
            if (RhythmManager.beatNum % 16 == 0)
            {
                playSource.Play();
            }
        };

        PlayerController.OnPlayerDeath += () =>
        {
            StopCoroutine("ResumeMusic");
            StartCoroutine("StopMusic", playSource);
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartLevel()
    {
        StopCoroutine("StopMusic");
        StartCoroutine("ResumeMusic", playSource);
    }
    IEnumerator StopMusic(AudioSource audioSource)
    {
        while (audioSource.volume > 0)
        {
            Debug.Log("stop");
            audioSource.volume -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    IEnumerator ResumeMusic(AudioSource audioSource)
    {
        while (audioSource.volume < 1)
        {
            Debug.Log("resume");
            audioSource.volume += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}
