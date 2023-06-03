using System;

public interface IInventoryItem
{
    IInventoryItemInfo info { get; }
    IInventoryItemState state { get; }
    Type type { get; }
    object obj { get; }
    void UseItem();
    void EquipItem();
    
    IInventoryItem Clone();
}
