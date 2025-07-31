using UnityEngine;
using System;

public class RhythmManager : MonoBehaviour
{
    public float bpm = 120f;
    public float beatOffset = 0f;

    private float beatInterval;  // Time between beats
    private float songPosition;
    private float dspSongStartTime;

    public static event Action OnBeat;

    void Start()
    {
        beatInterval = 60f / bpm;
        dspSongStartTime = (float)AudioSettings.dspTime;
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongStartTime - beatOffset);

        if (songPosition >= beatInterval)
        {
            Debug.Log("Beat!");
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
