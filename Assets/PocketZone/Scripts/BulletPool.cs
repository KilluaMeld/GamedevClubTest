using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _poolCount;
    [SerializeField] private bool _autoExpand;

    private ObjectsPool<Bullet> pool;

    private void Start()
    {
        pool = new ObjectsPool<Bullet>(_bulletPrefab, _poolCount, this.transform);
        pool.autoExpand = _autoExpand;
    }
    public void Shoot(Vector3 direction, float damage, Transform shootPos)
    {
        var bullet = pool.GetFreeElement();
        bullet.transform.position = shootPos.position;
        bullet.SetProperties(direction, damage);
    }
}
