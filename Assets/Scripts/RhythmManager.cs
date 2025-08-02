using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.LowLevel;

public class RhythmManager : MonoBehaviour
{
    public static event Action OnBeat; // global beat event
    public static event Action OnGameBeat; // called only when the level is being played
    public static event Action OnPlayerSyncedBeat; // global beat event
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
    public static bool canStartMoving = false;
    public bool flashCamera = false;
    public static int beat = 0;
    public static int playerSyncedBeat = 0;
    public static int beatNum = 0;
    public static bool beforeBeatWindow;
    public static bool afterBeatWindow;

    void Start()
    {
        PlayerController.OnPlayerBeat += () =>
        {
            if (GameManager.isPlaying)
            {
                OnPlayerSyncedBeat?.Invoke();
            }
        };
        OnBeat += () =>
        {
            if (!GameManager.isPlaying)
            {
                OnPlayerSyncedBeat?.Invoke();
            }
        };
        OnPlayerSyncedBeat += () =>
        {
            playerSyncedBeat++;
            if (playerSyncedBeat == 4)
            {
                playerSyncedBeat = 0;
            }
        };

        ResetBeat();
    }

    public void StartLevel()
    {
        canMove = true;

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
        }
        if (AudioSettings.dspTime >= nextBeatStartTime)
        {
            canMove = true;
            beforeBeatWindow = true;
            if (beat == 3) canStartMoving = true;
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
            afterBeatWindow = false;
            canStartMoving = false;
            if (flashCamera) Camera.main.backgroundColor = Color.black;
            nextBeatFinishTime += beatInterval;
        }
    }

    void Beat()
    {
        beat++;
        beatNum++;
        if (beat == 4)
        {
            beat = 0;
        }
        OnBeat?.Invoke();
        if (GameManager.isPlaying)
        {
            OnGameBeat?.Invoke();
        }
        beforeBeatWindow = false;
        afterBeatWindow = true;

        nextBeatTime += beatInterval;
    }

    public static void ExecuteOnNextBeat(Action function)
    {
        OnBeat += Subscribe;
        void Subscribe()
        {
            function();
            OnBeat -= Subscribe;
        }
    }
}
