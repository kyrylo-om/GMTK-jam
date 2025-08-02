using System.Collections;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    public Animator animator;
    public bool steppedOn = false;
    public bool submerged = false;
    private bool skipReset = false;
    void Start()
    {
        PlayerController.OnPlayerDeath += Reset;
    }
    private void OnCollisionEnter(Collision collision)
    {
        steppedOn = true;
        animator.SetTrigger("OnStep");
    }
    private void OnCollisionExit(Collision collision)
    {
        steppedOn = false;
        if (!skipReset)
        {
            GetComponent<Collider>().isTrigger = true;
            submerged = true;
            animator.SetTrigger("OnLeave");
            gameObject.tag = "Death";
        }
        else
        {
            skipReset = false;
        }
    }
    void Reset()
    {
        GetComponent<Collider>().isTrigger = false;
        if (submerged)
        {
            animator.SetTrigger("Reset");
        }
        else if (!submerged && steppedOn)
        {
            skipReset = true;
            animator.SetTrigger("OnLeave");
            animator.SetTrigger("Reset");
        }
        submerged = false;
        gameObject.tag = "Untagged";
    }
}
