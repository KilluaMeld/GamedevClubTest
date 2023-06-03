using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private UIInventory _inventory;
    Button shootingButton;
    private void Start()
    {
        shootingButton = GetComponent<Button>();
        shootingButton.onClick.AddListener(Shoot);
    }

    void Shoot()
    {

    }
}
