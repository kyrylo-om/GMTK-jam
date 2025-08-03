using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public Animator popupAnimator;
    public GameObject infoPopup;
    public TMP_Text popupText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowText(string text, int fontSize)
    {
        popupText.text = text;
        popupText.fontSize = fontSize;
        popupAnimator.Play("UI Popup Appear");
    }
}
