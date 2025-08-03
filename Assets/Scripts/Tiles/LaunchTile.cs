using UnityEngine;

public class LaunchTile : MonoBehaviour
{
    private BaseTile tileScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileScript = GetComponent<BaseTile>();

        tileScript.OnPlayerLeave += () =>
        {
            tileScript.animator.Play("Launch Tile", 1);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
