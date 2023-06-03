using UnityEngine;
using UnityEngine.UI;

public class ClickOnItem : MonoBehaviour
{
    [SerializeField] private UIShowItem uIShowItem;
    [SerializeField] private UIInventorySlot slot;
    [SerializeField] private Button button;
    public void ShowItems()
    {
        if (!slot.slot.isEmpty)
            uIShowItem.SetItemInfo(slot.slot);
    }
}
