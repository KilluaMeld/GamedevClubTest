using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UIInventory : MonoBehaviour
{
    string KEY = "Inventory";
    Saver saver = new Saver();
    InventoryWithSlots inventory = new InventoryWithSlots(15);
    public InventoryWithSlots Inventory { get => inventory;}
    LoadItemList inventoryList;



    public void LoadInventory()
    {
        inventoryList = GameObject.FindObjectOfType<LoadItemList>();
        saver.Load<SaveItem[]>(KEY, LoadData);
        inventory.OnInventoryStateChangedEvent += SaveData;
        inventory.InvokeAction();
    }

    void LoadData(SaveItem[] saveItems)
    {
        List<SaveItem> itemsForSave = new List<SaveItem>();
        foreach (SaveItem item in saveItems)
        {
            inventoryList.CreateItemOfType(item.ItemType, item.ItemAmount, item.isEquip);
        }
    }
    void SaveData(object sender)
    {
        List<SaveItem> itemsForSave = new List<SaveItem>();
        foreach (var item in inventory.GetAllItems())
        {
            var saveItem = new SaveItem();
            if (item.type != null)
            {
                saveItem.ItemType = item.type;
                saveItem.ItemAmount = item.state.amount;
                saveItem.isEquip = item.state.isEquipped;
                itemsForSave.Add(saveItem);
            }
            else
                itemsForSave.Add(saveItem);
        }
        saver.Save(KEY, itemsForSave.ToArray());
    }
}
[Serializable]
public class SaveItem
{
    public Type ItemType;
    public int ItemAmount;
    public bool isEquip;
}