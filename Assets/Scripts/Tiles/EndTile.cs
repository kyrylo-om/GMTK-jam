using UnityEngine;

public class EndTile : MonoBehaviour
{
    public int beat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerController.OnPlayerMove += () =>
        {
            beat--;
            if (beat < 0) beat = 3;
            transform.GetChild(1).GetComponent<TMPro.TextMeshPro>().text = beat.ToString();
            if (beat == 0)
            {
                transform.GetChild(0).tag = "Win";
            }
            else
            {
                transform.GetChild(0).tag = "Death";
            }
        };
        PlayerController.OnPlayerDeath += () =>
        {
            beat = 4;
            transform.GetChild(1).GetComponent<TMPro.TextMeshPro>().text = beat.ToString();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
