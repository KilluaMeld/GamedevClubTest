using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIShowItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI decription;
    [SerializeField] private Button delete;
    [SerializeField] private Button equip;
    [SerializeField] private HudView hud;
    IInventorySlot slot;
    UIInventory inventory;
    private void Start()
    {
        inventory = GameObject.FindObjectOfType<UIInventory>();
        delete.onClick.AddListener(RemoveItem);
        equip.onClick.AddListener(EquipItem);
    }
    private void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
    public void SetItemInfo(IInventorySlot slot)
    {
        this.slot = slot;
        icon.sprite = slot.item.info.spriteIcon;
        title.text = slot.item.info.title;
        decription.text = slot.item.info.description;
        this.gameObject.SetActive(true);
    }
    void RemoveItem()
    {
        //inventory.Inventory.Remove(this, slot.item.type, slot.item.state.amount);
        slot.Clear();
        inventory.Inventory.InvokeAction();
        this.gameObject.SetActive(false);
    }
    void EquipItem()
    {
        var gun = slot.item.obj;
        if (gun is IGun)
        {
            hud.SetHudItem(slot);
        }
    }
}
