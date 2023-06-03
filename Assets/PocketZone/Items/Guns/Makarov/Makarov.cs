using System;
using UnityEngine;

public class Makarov : IInventoryItem, IGun
{
    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }
    public Type type { get => GetType(); }
    Type bullets = typeof(Bullets545);
    public Type Bullets { get => bullets; }
    float damage = 15;
    public float Damage { get => damage; }

    public float ShootingSpeed { get => shootingSpeed; }
    float shootingSpeed = 1f;

    public int MagazineCapacity { get => magazineCapacity; }
    int magazineCapacity = 30;

    public object obj { get => this; }

    public Makarov(IInventoryItemInfo info)
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
    public bool Shoot(InventoryWithSlots inventory)
    {
        return inventory.TryToRemove(this, Bullets, 1);
    }
    public IInventoryItem Clone()
    {
        var clonnedItem = new Makarov(info);
        clonnedItem.state.amount = state.amount;
        return clonnedItem;
    }
}
