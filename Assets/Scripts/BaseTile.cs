using UnityEngine;

public class BaseTile : MonoBehaviour
{
    void OnEnable()
    {
        PlayerController.OnPlayerDeath += Reset;
    }
    void OnDisable()
    {
        PlayerController.OnPlayerDeath -= Reset;
    }
    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
        gameObject.tag = "Death";
    }
    void Reset()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().isTrigger = false;
        gameObject.tag = "Untagged";
    }
}
