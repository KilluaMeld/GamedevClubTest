using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public Action<float> onHealthChange { get; set; }
    public Action onDeath { get; set; }

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    public float HealthAmount { get => health; }
    public float MaxHealthAmount { get => maxHealth; }

    public void Start()
    {
        onHealthChange?.Invoke(health);
    }
    public float GetHealthValue()
    {
        return health;  
    }
    public void PlusHealth(float value)
    {
        health += value;
        if(health> MaxHealthAmount)
            health = MaxHealthAmount;

        onHealthChange?.Invoke(health);
    }
    public void MinusHealth(float value)
    {
        health -= value;
        if (health <= 0)
        {
            health = 0;
            onDeath?.Invoke();
        }

        onHealthChange?.Invoke(health);
        //death
    }
}
