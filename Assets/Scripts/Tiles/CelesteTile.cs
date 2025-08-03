using UnityEngine;

public class CelesteTile : MonoBehaviour
{
    public Animator animator;
    public bool active;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        RhythmManager.OnPlayerSyncedBeat += ChangeState;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ChangeState()
    {
        if (RhythmManager.playerSyncedBeat == 0 || RhythmManager.playerSyncedBeat == 2)
        {
            active = !active;
            if (active)
            {
                gameObject.tag = "Untagged";
                animator.Play("Celeste Tile Appear");
            }
            else
            {
                gameObject.tag = "NoBlock";
                animator.Play("Celeste Tile Disappear");
            }
        }
    }
}
