using System;
using System.Collections;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    public Animator animator;
    public bool steppedOn = false;
    public bool submerged = false;
    public GameObject spotLightPrefab;
    private GameObject spotLight;
    public event Action OnPlayerLeave;
    void Start()
    {
        PlayerController.OnPlayerDeath += Reset;
    }
    private void OnCollisionEnter(Collision collision)
    {
        steppedOn = true;
        animator.SetTrigger("OnStep");
        spotLight = Instantiate(spotLightPrefab, transform.position, Quaternion.identity);
    }
    private void OnCollisionExit(Collision collision)
    {
        steppedOn = false;
        Destroy(spotLight);
        if (GameManager.isPlaying)
        {
            OnPlayerLeave?.Invoke();
            GetComponent<Collider>().isTrigger = true;
            submerged = true;
            animator.SetTrigger("OnLeave");
            gameObject.tag = "NoBlock";
        }
    }
    void Reset()
    {
        GetComponent<Collider>().isTrigger = false;
        if (steppedOn)
        {
            animator.SetTrigger("OnLeave");
            animator.SetTrigger("Reset");
        }
        if (submerged)
        {
            animator.SetTrigger("Reset");
        }
        submerged = false;
        steppedOn = false;
        gameObject.tag = "Untagged";
    }
}
