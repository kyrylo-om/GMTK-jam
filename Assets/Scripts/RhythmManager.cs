using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.LowLevel;

public class RhythmManager : MonoBehaviour
{
    public static event Action OnBeat; // global beat event
    public static event Action OnGameBeat; // called only when the level is being played
    public static event Action OnBeatWindowEnd;

    public float bpm = 120f;
    public float beatOffset = 0f;

    public float beatInterval;
    private float dspSongStartTime;
    public float nextBeatTime;
    public float nextBeatStartTime;
    public float nextBeatFinishTime;
    public float moveWindow = 0.0f;

    public static bool canMove = false;
    public bool flashCamera = false;

    void Start()
    {
        ResetBeat();
    }

    public void StartLevel()
    {
        OnBeat?.Invoke();

        canMove = true;

        ResetBeat();

        GameManager.isPlaying = true;
    }

    void ResetBeat()
    {
        beatInterval = 60f / bpm;
        dspSongStartTime = (float)AudioSettings.dspTime;

        nextBeatTime = dspSongStartTime + beatOffset + beatInterval;
        nextBeatStartTime = nextBeatTime - moveWindow;
        nextBeatFinishTime = nextBeatTime - (beatInterval - moveWindow);
    }

    void Update()
    {
        if (AudioSettings.dspTime >= nextBeatTime)
        {
            Beat();
            OnBeat?.Invoke();
            if (GameManager.isPlaying)
            {
                OnGameBeat?.Invoke();
            }
            nextBeatTime += beatInterval;
        }
        if (AudioSettings.dspTime >= nextBeatStartTime)
        {
            canMove = true;
            if (flashCamera) Camera.main.backgroundColor = Color.white;
            nextBeatStartTime += beatInterval;
        }
        else if (AudioSettings.dspTime >= nextBeatFinishTime)
        {
            if (GameManager.isPlaying)
            {
                OnBeatWindowEnd?.Invoke();
            }
            canMove = false;
            if (flashCamera) Camera.main.backgroundColor = Color.black;
            nextBeatFinishTime += beatInterval;
        }
    }

    void Beat()
    {
        // audioSource.PlayOneShot(clip);
    }
}
