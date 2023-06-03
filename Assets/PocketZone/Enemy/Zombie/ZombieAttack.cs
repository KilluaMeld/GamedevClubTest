using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _damage;
    bool readyShoot = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.GetComponent<Player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = null;
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1);
        readyShoot = true;
    }

    private void Attack()
    {
        _player.TakeDamage(_damage);
    }

    private void Update()
    {
        if (readyShoot && _player != null)
        {
            Attack();
            readyShoot = false;
            StartCoroutine(Timer());
        }
    }
}
