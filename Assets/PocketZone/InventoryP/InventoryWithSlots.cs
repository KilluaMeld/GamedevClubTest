using System;
using System.Collections.Generic;
using System.Linq;
public class InventoryWithSlots : IInventory
{
    public event Action<object, IInventoryItem, int> onInventoryItemAddadEvent;
    public event Action<object, Type, int> onInventoryItemRemovedEvent;
    public event Action<object> OnInventoryStateChangedEvent;

    public int capacity { get; set; }

    public bool isFull => _slots.All(slot => slot.isFull);

    private List<IInventorySlot> _slots;
    public InventoryWithSlots(int capacity)
    {
        this.capacity = capacity;
        _slots =  new List<IInventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }
    public void InvokeAction()
    {
        OnInventoryStateChangedEvent?.Invoke(this);
    }

    public IInventoryItem GetItem(Type itemType)
    {
        /*        var slotsWithItems = _slots.FindAll(slot => !slot.isEmpty);
                var item = slotsWithItems.Find(slot => slot.itemType == itemType).item;
                return item!= null ? item : null;*/
        return _slots.Find(slot => slot.itemType == itemType).item;
    }
    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();
        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
                allItems.Add(slot.item);
        }

        return allItems.ToArray();
    }

    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotOfType = _slots.
            FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
        foreach (var slot in slotOfType)
        {
            allItemsOfType.Add(slot.item);
        }

        return allItemsOfType.ToArray();
    }

    public IInventoryItem[] GetEquippedItems()
    {
        var requiredSlots = _slots.FindAll(slot => slot.item.state.isEquipped);
        var equippedItems = new List<IInventoryItem>();
        foreach (var slot in requiredSlots)
        {
            equippedItems.Add(slot.item);
        }

        return equippedItems.ToArray();
    }


    public int GetItemAmount(Type itemType)
    {
        var amount = 0;
        var allItemsSlot = _slots.
            FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
        foreach (var slot in allItemsSlot)
        {
            amount += slot.amount;
        }

        return amount;
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        /*        var slotWithSameItemsButNotEmpty = _slots.
                    Find(slot => slot.isEmpty && slot.itemType == item.type && !slot.isEmpty);*/
        var slotWithSameItemsButNotEmpty = _slots.Find(slot => slot.isEmpty);

        if (slotWithSameItemsButNotEmpty != null)
            return TryToAddToSlot(sender, slotWithSameItemsButNotEmpty, item);

        var emptySlot = _slots.Find(slot => slot.isEmpty);
        if(emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);


        return false;
    }

    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.amount + item.state.amount <= item.info.maxItemsInInventorySlot;
        var amountToAdd = fits ? item.state.amount: item.info.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.state.amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.state.amount = amountToAdd;

        if (slot.isEmpty)
            slot.SetItem(clonedItem);
        else
            slot.item.state.amount += amountToAdd;

        onInventoryItemAddadEvent?.Invoke(sender, item, amountToAdd);
        OnInventoryStateChangedEvent?.Invoke(sender);

        if (amountLeft <= 0)
            return true;

        item.state.amount = amountLeft;
        return TryToAdd(sender, item);
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.isEmpty)
            return;

        if (toSlot.isFull)
            return;

        if (!toSlot.isEmpty && fromSlot.itemType != toSlot.itemType)
            return;
        if (fromSlot == toSlot) 
            return;

        var slotCapacity = fromSlot.capacity;
        var fits = fromSlot.amount + toSlot.amount <= slotCapacity;
        var amountToAdd = fits ? fromSlot.amount : slotCapacity - toSlot.amount;
        var amountLeft = fromSlot.amount - amountToAdd;

        if (toSlot.isEmpty)
        {
            toSlot.SetItem(fromSlot.item);
            fromSlot.Clear();

            OnInventoryStateChangedEvent?.Invoke(sender);
        }

        toSlot.item.state.amount += amountToAdd;
        if(fits)
            fromSlot.Clear();
        else
            fromSlot.item.state.amount -= amountLeft;

        OnInventoryStateChangedEvent?.Invoke(sender);

    }
    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotWithItems = GetAllSlots(itemType);
        if (slotWithItems == null)
            return;

        var amountToRemove = amount;
        var count = slotWithItems.Length;

        for (int i = count-1; i >= 0; i--)
        {
            var slot = slotWithItems[i];
            if (slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if (slot.amount == 0)
                    slot.Clear();
                onInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);
                break;
            }
            var amountRemoved = slot.amount;
            amountToRemove -= slot.amount;
            slot.Clear();
            onInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }

    }
    public bool TryToRemove(object sender, Type itemType, int amount = 1)
    {
        var slotWithItems = GetAllSlots(itemType);
        if (slotWithItems == null)
            return false;

        var allCount = 0;
        foreach ( var slot in slotWithItems)
        {
            allCount += slot.item.state.amount;
        }

        if (allCount<amount)
            return false;

        var amountToRemove = amount;
        var count = slotWithItems.Length;

        for (int i = count - 1; i >= 0; i--)
        {
            var slot = slotWithItems[i];
            if (slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if (slot.amount == 0)
                    slot.Clear();
                onInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);
                break;
            }
            var amountRemoved = slot.amount;
            amountToRemove -= slot.amount;
            slot.Clear();
            onInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }
        return true;

    }

    public bool HasItem(Type itemType, out IInventoryItem item)
    {
        item = GetItem(itemType);
        return item != null;
    }
    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
    }

    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
    public IInventorySlot GetEmptySlot()
    {
        return _slots.Find(slot => slot.isEmpty == true);
    }



}
