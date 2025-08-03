using UnityEngine;

public class FlowerSpowner : MonoBehaviour
{
    public GameObject flowerPrefab;
    void Start()
    {
        if (Random.Range(0f, 1f) > 0.95f)
        {
            Instantiate(flowerPrefab, gameObject.transform);
        }
    }
}
