using UnityEngine.EventSystems;

//Интерфейс для всех "переносимых"
public interface IDraggable
{
    void OnBeginDrag(PointerEventData eventData);
    void OnDrag(PointerEventData eventData);
    void OnEndDrag(PointerEventData eventData);
}
