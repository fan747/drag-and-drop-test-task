using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//Контроллер rectTransform
public class DraggableItemRectTransformController
{
    private RectTransform _rectTransform;
    private Canvas _mainCanvas;

    //Стартовая точка курсора и rectTransform
    private Vector2 _originalLocalPointerPosition;
    private Vector2 _originalPosition;

    public DraggableItemRectTransformController(RectTransform rectTransform, Canvas canvas)
    {
        _rectTransform = rectTransform;
        _mainCanvas = canvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Получение стартовых локальных точек курсора, относительно канваса, и rectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_mainCanvas.transform as RectTransform,eventData.position, eventData.pressEventCamera, out _originalLocalPointerPosition);
        _originalPosition = _rectTransform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Получение локальную точку курсора, относительно канваса
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_mainCanvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition);

        //Перемещение rectTransform по смещению мышки
        Vector2 offset = localPointerPosition - _originalLocalPointerPosition;
        _rectTransform.localPosition = _originalPosition + offset;
    }

    public void MoveRectTransform(Vector3 position)
    {
        //Из глобальной позици в локальную, и рект туда
        Vector3 localPosition = _rectTransform.parent.InverseTransformPoint(position);
        _rectTransform.localPosition = localPosition;
    }

    //Получение "половины" высоты предмета, для того что бы красиво поставить
    public float GetRectTransformHalfHeight() => _rectTransform.rect.height / GameConstants.RectTransformHeightSeparator * _rectTransform.lossyScale.y;

    //получение позиции
    public Vector3 GetPosition() => _rectTransform.position;
}

