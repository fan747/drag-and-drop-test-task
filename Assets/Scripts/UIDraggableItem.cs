using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

//Реализация наследуемого класса DraggableItem
public class UIDraggableItem : DraggableItem
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }
}
