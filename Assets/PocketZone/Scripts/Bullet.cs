using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTimeBullet;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private Vector3 moveDirection;
    public void SetProperties(Vector3 direction, float damage)
    {
        moveDirection = direction;
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<IDamageable>();
        if(health != null)
        {
            health.TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }
    IEnumerator LifeBullet()
    {
        yield return new WaitForSecondsRealtime(_lifeTimeBullet);
        this.gameObject.SetActive(false);
    }
    private void Move(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.fixedDeltaTime);
    }
    private void FixedUpdate()
    {
        Move(moveDirection);
    }
}
