using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        var otherItemTranform = eventData.pointerDrag.transform;
        otherItemTranform.SetParent(transform);
        otherItemTranform.localPosition = Vector3.zero;
    }
}
