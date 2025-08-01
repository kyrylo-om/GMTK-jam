using UnityEngine;
using System.Collections;
using static Unity.VisualScripting.Metadata;

public class BallsRhythm : MonoBehaviour
{
    public Transform[] children;
    public int beat;
    void Start()
    {
        beat = -1;
        children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        PlayerController.OnPlayerDeath += () =>
        {
            StartCoroutine(ScaleToTarget(new Vector3(.5f, .5f, .5f), 0.1f, beat));
            beat = -1;
        };


        RhythmManager.OnBeat += () =>
        {
            StartCoroutine(ScaleToTarget(new Vector3(.5f, .5f, .5f), 0.1f, beat));

            beat++;
            if (beat == 4)
            {
                beat = 0;
            }
            children[beat].localScale = new Vector3(1f, 1f, 1f);
        };

        IEnumerator ScaleToTarget(Vector3 target, float time, int beat)
        {
            if (beat >= 0)
            {
                Vector3 initialScale = children[beat].localScale;
                float elapsed = 0f;

                while (elapsed < time)
                {
                    children[beat].localScale = Vector3.Lerp(initialScale, target, elapsed / time);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}
