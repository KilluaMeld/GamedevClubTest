using UnityEngine;

public class Zombie : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    private DropItems _dropItems;

    private void Start()
    {
        _health.onDeath += Death;
        _dropItems = GetComponent<DropItems>();
    }
    public void TakeDamage(float damage)
    {
        _health.MinusHealth(damage);
    }
    void Death()
    {
        gameObject.SetActive(false);
        _dropItems.DropRandomItem();
    }

}
