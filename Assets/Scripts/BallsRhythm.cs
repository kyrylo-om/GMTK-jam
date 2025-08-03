using UnityEngine;
using System.Collections;
using static Unity.VisualScripting.Metadata;

public class BallsRhythm : MonoBehaviour
{
    public Transform[] children;
    private int beat;
    public Material idle;
    public Material active;
    public Material accent;
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
            children[RhythmManager.beat].localScale = new Vector3(.7f, .7f, .7f);
            children[RhythmManager.beat].GetComponent<MeshRenderer>().material = active;
            if (RhythmManager.beat == 0)
            {
                children[RhythmManager.beat].GetComponent<MeshRenderer>().material = accent;
            }

            StartCoroutine(ScaleToTarget(new Vector3(.5f, .5f, .5f), 0.1f, RhythmManager.beat == 0 ? 3 : RhythmManager.beat - 1));
        };

        IEnumerator ScaleToTarget(Vector3 target, float time, int beat)
        {
            if (beat >= 0)
            {
                children[beat].GetComponent<MeshRenderer>().material = idle;
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
