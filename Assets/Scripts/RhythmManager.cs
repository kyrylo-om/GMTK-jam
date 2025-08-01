using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.LowLevel;

public class RhythmManager : MonoBehaviour
{
    public static event Action OnBeat; // global beat event
    public static event Action OnBeatWindowEnd; // global beat event

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
        
    }

    public void StartLevel()
    {
        OnBeat?.Invoke();

        canMove = true;
        beatInterval = 60f / bpm;
        dspSongStartTime = (float)AudioSettings.dspTime;

        nextBeatTime = dspSongStartTime + beatOffset + beatInterval;
        nextBeatStartTime = nextBeatTime - moveWindow;
        nextBeatFinishTime = nextBeatTime - (beatInterval - moveWindow);

        GameManager.isPlaying = true;
    }

    void Update()
    {
        if (GameManager.isPlaying)
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
                // Camera.main.backgroundColor = Color.white;
                nextBeatStartTime += beatInterval;
            }
            else if (AudioSettings.dspTime >= nextBeatFinishTime)
            {
                OnBeatWindowEnd?.Invoke();
                canMove = false;
                // Camera.main.backgroundColor = Color.black;
                nextBeatFinishTime += beatInterval;
            }
        }
    }

    void Beat()
    {
        // audioSource.PlayOneShot(clip);
    }
}
