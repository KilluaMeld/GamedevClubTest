using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image healthLine;
    [SerializeField] private IHealth _health;

    private void Start()
    {
        _health = GetComponentInParent<IHealth>();
        _health.onHealthChange += UpdateHealthLine;
        UpdateHealthLine(_health.GetHealthValue());

    }
    void UpdateHealthLine(float valueHealth)
    {
        healthLine.fillAmount = valueHealth / _health.MaxHealthAmount;
    }
}
