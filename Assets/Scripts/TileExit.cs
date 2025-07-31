using UnityEngine;

public class TileExit : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }
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
        GetComponent<Collider>().enabled = false;
    }
    void Reset()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
