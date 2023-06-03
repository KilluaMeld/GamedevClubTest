using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets545 : IInventoryItem
{
    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Type type { get => GetType(); }
    public object obj { get => this; }

    public Bullets545(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public void UseItem()
    {

    }
    public void EquipItem()
    {
        
    }
    public IInventoryItem Clone()
    {
        var clonnedItem = new Bullets545(info);
        clonnedItem.state.amount = state.amount;
        return clonnedItem;
    }
}
