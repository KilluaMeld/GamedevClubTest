using UnityEngine;

public class DropItems : MonoBehaviour
{
    [SerializeField] GameObject[] itemsPrefabs;

    public void DropRandomItem()
    {
        var randPrefab = itemsPrefabs[Random.Range(0, itemsPrefabs.Length)];
        Instantiate(randPrefab, transform.position, Quaternion.identity);
    }
}
