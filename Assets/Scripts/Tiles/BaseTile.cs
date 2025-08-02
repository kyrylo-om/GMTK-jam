using System.Collections;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private Animator animator;
    public bool steppedOn = false;
    public bool submerged = false;
    private bool skipReset = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController.OnPlayerDeath += Reset;
    }
    private void OnCollisionEnter(Collision collision)
    {
        steppedOn = true;
        animator.SetTrigger("OnStep");
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
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
        Debug.Log("Reset");
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
        GetComponent<Collider>().isTrigger = false;
        gameObject.tag = "Untagged";
    }
}
