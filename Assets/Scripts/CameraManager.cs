using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GridManager gridManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public IEnumerator MoveCamera(int tiles, float duration)
    {
        float start = transform.position.z;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            float easedT = Mathf.SmoothStep(0, 1, t);

            float newZ = Mathf.Lerp(start, start + tiles * gridManager.tileSize, easedT);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

            yield return null;
        }
    }
}
