using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AK74 : IInventoryItem, IGun
{
    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Type type { get=> GetType(); }
    Type bullets = typeof(Bullets545);
    public Type Bullets { get => bullets;}
    float damage = 35;

    public int MagazineCapacity { get=> magazineCapacity; }
    int magazineCapacity = 30;

    public float ShootingSpeed { get => shootingSpeed; }
    float shootingSpeed = 0.3f;
    public float Damage { get => damage; }
    public object obj { get => this; }

    public void UseItem()
    {

    }    
    public void EquipItem()
    {

    }
    public AK74(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public IInventoryItem Clone()
    {
        var clonnedItem = new AK74(info);
        clonnedItem.state.amount = state.amount;
        return clonnedItem;
    }

    public bool Shoot(InventoryWithSlots inventory)
    {
        return inventory.TryToRemove(this, Bullets, 1);
    }
}
