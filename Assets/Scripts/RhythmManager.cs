using System;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmManager : MonoBehaviour
{
    public float bpm = 120f;
    public float beatOffset = 0f;

    private float beatInterval;  // Time between beats
    private float songPosition;
    private float dspSongStartTime;

    public static event Action OnBeat;

    private AudioClip clip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clip = audioSource.clip; // Ensure the clip is set before playing

        beatInterval = 60f / bpm;
        dspSongStartTime = (float)AudioSettings.dspTime;
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongStartTime - beatOffset);

        if (songPosition >= beatInterval)
        {
            Debug.Log("Beat!");
            
            audioSource.PlayOneShot(clip);
            

            songPosition -= beatInterval;
            OnBeat?.Invoke();
        }
    }

    public float GetTimeUntilNextBeat()
    {
        float beatInterval = 60f / bpm;
        float currentTime = (float)AudioSettings.dspTime;
        float timeSinceLastBeat = currentTime % beatInterval;
        return Mathf.Min(timeSinceLastBeat, beatInterval - timeSinceLastBeat);
    }
}
