using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudView : MonoBehaviour
{
    [SerializeField] private bool isEquipped;
    [SerializeField] private GameObject hud;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _magazineCapacity;
    [SerializeField] private UIInventory _inventory;
    [SerializeField] private Joystick shootButton;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private BulletPool pool;

    IInventorySlot slot;
    IGun _gun;
    Vector3 direction;
    bool readyShoot = true;
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }
    public void SetHudItem(IInventorySlot slot)
    {
        this.slot = slot;
        _icon.sprite = slot.item.info.spriteIcon;
        UpdateAmmunitionCount(this);
        hud.SetActive(true);
        SetGun();
    }
    private void Start()
    {
        hud.SetActive(false);
        _inventory.Inventory.OnInventoryStateChangedEvent += UpdateAmmunitionCount;
        _inventory.Inventory.OnInventoryStateChangedEvent += CheckSlot;
    }
    private void CheckSlot(object sender)
    {
        if (slot == null)
            return;
        if (slot.isEmpty)
        {
            hud.SetActive(false);
        }
    }
    private void UpdateAmmunitionCount(object sender)
    {
            var items = _inventory.Inventory.GetAllItems(typeof(Bullets545));
            var countAmmunition = 0;
            for (int i = 0; i < items.Length; i++)
            {
                countAmmunition += items[i].state.amount;
            }
            _magazineCapacity.text = $"{countAmmunition}";
    }
    private void SetGun()
    {
        if (slot == null)
            return;
        var gun = slot.item.obj;
        if (gun is IGun)
        {
            this._gun = (IGun)gun;
        }
    }
    private void Shoot()
    {
            if (_gun.Shoot(_inventory.Inventory))
            {
                pool.Shoot(direction, _gun.Damage, playerTransform);
            }
            else
                Debug.Log("Not Shoot");
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(_gun.ShootingSpeed);
        readyShoot = true;
    }

    private void Update()
    {
        if (_gun != null && slot != null)
        {
            if (readyShoot && (Mathf.Abs(shootButton.Horizontal) > 0.3f || Mathf.Abs(shootButton.Vertical) > 0.3f))
            {
                direction = new Vector3(shootButton.Horizontal, shootButton.Vertical, 0);
                Shoot();
                readyShoot = false;
                StartCoroutine(Timer());
            }
        }
    }
}
