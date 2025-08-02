using UnityEngine;

public class DeathTextPopup : MonoBehaviour
{
    public TMPro.TextMeshPro textMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
