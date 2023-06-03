using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] Health health;
    private void Start()
    {
        health.onDeath += Death;
    }
    public void TakeDamage(float damage)
    {
        health.MinusHealth(damage);
    }
    void Death() { 
        Destroy(gameObject);
    }
}
