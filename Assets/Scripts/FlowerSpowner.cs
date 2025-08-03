using UnityEngine;

public class FlowerSpowner : MonoBehaviour
{
    public GameObject flowerPrefab;
    void Start()
    {
        Instantiate(flowerPrefab, gameObject.transform);
    }
}
