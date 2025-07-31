using System;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmManager : MonoBehaviour
{
    public event Action OnBeat; // global beat event
    public event Action OnBeatWindowEnd; // global beat event

    private AudioClip clip;
    private AudioSource audioSource;
    public float bpm = 120f;
    public float beatOffset = 0f;

    public float beatInterval;
    private float dspSongStartTime;
    public float nextBeatTime;
    public float nextBeatStartTime;
    public float nextBeatFinishTime;
    public float moveWindow = 0.0f;

    public bool canMove = false;

    void Start()
    {
        beatInterval = 60f / bpm;
        dspSongStartTime = (float)AudioSettings.dspTime;

        nextBeatTime = dspSongStartTime + beatOffset;
        nextBeatStartTime = nextBeatTime - moveWindow;
        nextBeatFinishTime = nextBeatTime - (beatInterval - moveWindow);

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
        if (AudioSettings.dspTime >= nextBeatStartTime)
        {
            canMove = true;
            Camera.main.backgroundColor = Color.white;
            nextBeatStartTime += beatInterval;
        }
        else if (AudioSettings.dspTime >= nextBeatFinishTime)
        {
            OnBeatWindowEnd?.Invoke();
            canMove = false;
            Camera.main.backgroundColor = Color.black;
            nextBeatFinishTime += beatInterval;
        }
    }

    void Beat()
    {
        // audioSource.PlayOneShot(clip);
    }
}
