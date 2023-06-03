using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadItemList : MonoBehaviour
{
    [SerializeField] private List<InventoryItemInfo> listItemsInfo = new List<InventoryItemInfo>();
    [SerializeField] private UIInventory inventory;
    [SerializeField] private List<UIInventorySlot> uiSlots;

    private void Start()
    {
        inventory.Inventory.OnInventoryStateChangedEvent += RefreshUIInventorySlots;
        inventory.LoadInventory();
        RefreshUIInventorySlots(this);
    }

    void RefreshUIInventorySlots(object sender)
    {
        var allSlots = inventory.Inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }

    }
    public void CreateItemOfType(Type type, int amount = 1, bool isEquip = false)
    {
        switch (type)
        {
            case Type t when t == typeof(AK74): CreateNewAK74(amount, isEquip); break;
            case Type t when t == typeof(Makarov): CreateNewMakarov(amount, isEquip); break;
            case Type t when t == typeof(Bullets545): CreateNewBullets545(amount, isEquip); break;
        }
    }
    public void CreateNewAK74(int amount =1, bool isEquip = false)
    {
        string id = "ak74";
        var info = listItemsInfo.Find(itemInfo => itemInfo.id == id);
        var item = new AK74(info);
        item.state.amount = amount;
        item.state.isEquipped = isEquip;
        inventory.Inventory.TryToAdd(this, item);
        RefreshUIInventorySlots(this);
    }
    public void CreateNewMakarov(int amount = 1, bool isEquip = false)
    {
        string id = "makarov";
        var info = listItemsInfo.Find(itemInfo => itemInfo.id == id);
        var item = new Makarov(info);
        item.state.amount = amount;
        item.state.isEquipped = isEquip;
        inventory.Inventory.TryToAdd(this, item);
        RefreshUIInventorySlots(this);
    }
    public void CreateNewBullets545(int amount = 1, bool isEquip = false)
    {
        string id = "bullets545";
        var info = listItemsInfo.Find(itemInfo => itemInfo.id == id);
        var item = new Bullets545(info);
        item.state.amount = amount;
        item.state.isEquipped = isEquip;
        inventory.Inventory.TryToAdd(this, item);
        RefreshUIInventorySlots(this);
    }


}
