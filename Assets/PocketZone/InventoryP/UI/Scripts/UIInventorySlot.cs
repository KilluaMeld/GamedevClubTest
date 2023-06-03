using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot
{
    [SerializeField] private UIInventoryItem uiInventoryItem;
    public IInventorySlot slot { get; private set; }
    private UIInventory uiInventory;
    private void Awake()
    {
        uiInventory = GetComponentInParent<UIInventory>();  
    }
    public void SetSlot(IInventorySlot newSlot)
    {
        slot = newSlot; 
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.slot;
        var invetory = uiInventory.Inventory;

        invetory.TransitFromSlotToSlot(this,otherSlot,slot);

        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if(slot != null)
        {
            uiInventoryItem.Refresh(slot);
        }
    }
}
