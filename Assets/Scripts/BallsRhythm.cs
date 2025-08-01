using UnityEngine;
using System.Collections;
using static Unity.VisualScripting.Metadata;

public class BallsRhythm : MonoBehaviour
{
    public Transform[] children;
    private int beat;
    void Start()
    {
        children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        // PlayerController.OnPlayerDeath += () =>
        // {
        //     StartCoroutine(ScaleToTarget(new Vector3(.5f, .5f, .5f), 0.1f, beat));
        //     beat = -1;
        // };


        RhythmManager.OnBeat += () =>
        {
            // beat++;
            // if (beat == 4)
            // {
            //     beat = 0;
            // }
            children[RhythmManager.beat].localScale = new Vector3(1f, 1f, 1f);

            StartCoroutine(ScaleToTarget(new Vector3(.5f, .5f, .5f), 0.1f, RhythmManager.beat == 0 ? 3 : RhythmManager.beat - 1));
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
