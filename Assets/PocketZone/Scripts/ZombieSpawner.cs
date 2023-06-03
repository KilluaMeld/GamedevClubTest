using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Zombie _enemyPrefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private int _radiusToSpawn;


    private ObjectsPool<Zombie> pool;

    private void Start()
    {
        pool = new ObjectsPool<Zombie>(_enemyPrefab, _poolCount, this.transform);
        pool.autoExpand = _autoExpand;

        SpawnZombie();
    }

    private void SpawnZombie()
    {
        for (int i = 0; i < _poolCount; i++)
        {
            var randomPosX = Random.Range(transform.position.x - _radiusToSpawn, transform.position.x + _radiusToSpawn);
            var randomPosY = Random.Range(transform.position.y - _radiusToSpawn, transform.position.y + _radiusToSpawn);
            var randomPos = new Vector3(randomPosX, randomPosY, 0f);
            var zombie = pool.GetFreeElement();
            zombie.transform.position = randomPos;
        }
    }
}
