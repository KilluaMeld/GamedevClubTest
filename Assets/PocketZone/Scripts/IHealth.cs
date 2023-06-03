using System;

public interface IHealth
{
    public Action<float> onHealthChange { get; set; }
    public Action onDeath { get; set; }
    float HealthAmount { get;}
    float MaxHealthAmount { get;}
    public float GetHealthValue();
    public void PlusHealth(float value);
    public void MinusHealth(float value);
}
