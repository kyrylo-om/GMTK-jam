using System;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmManager : MonoBehaviour
{
    public event Action OnBeat; // global beat event

    private AudioClip clip;
    private AudioSource audioSource;
    public float bpm = 120f;
    public float beatOffset = 0f;

    public float beatInterval;
    private float dspSongStartTime;
    public float nextBeatTime;

    void Start()
    {
        beatInterval = 60f / bpm;
        dspSongStartTime = (float)AudioSettings.dspTime;
        nextBeatTime = dspSongStartTime + beatOffset;
        audioSource = GetComponent<AudioSource>();
        clip = audioSource.clip;
    }

    void Update()
    {
        if (AudioSettings.dspTime >= nextBeatTime)
        {
            Beat();
            OnBeat?.Invoke();
            nextBeatTime += beatInterval;
        }
    }

    void Beat()
    {
        audioSource.PlayOneShot(clip);
    }
}
