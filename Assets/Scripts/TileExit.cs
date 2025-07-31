using UnityEngine;

public class TileExit : MonoBehaviour
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
        GetComponent<Collider>().enabled = false;
    }
    void Reset()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
