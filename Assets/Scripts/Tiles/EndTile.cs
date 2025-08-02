using UnityEngine;
using UnityEngine.UI;

public class EndTile : MonoBehaviour
{
    public int beat;
    public TMPro.TextMeshPro textComponent;
    public bool activated = false;
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerController.OnPlayerMove += () =>
        {
            if (!activated)
            {
                beat--;
                if (beat < 0) beat = 3;
                textComponent.text = beat.ToString();
                if (beat == 0)
                {
                    transform.GetChild(0).tag = "Win";
                }
                else
                {
                    transform.GetChild(0).tag = "Death";
                }
            }
        };
        PlayerController.OnPlayerDeath += () =>
        {
            if (!activated)
            {
                beat = 4;
                textComponent.text = beat.ToString();
            }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(bool playAnim)
    {
        if (playAnim) animator.Play("End Block Activate");
        else transform.Rotate(180f, 0f, 0f);
        activated = true;
        textComponent.text = "✓";
        transform.GetChild(0).tag = "Untagged";
    }
}
