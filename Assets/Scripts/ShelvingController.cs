using System.Collections;
using UnityEngine;


public class ShelvingController
{
    //Коллайдер "соприкосновения" яблока и поверхности
    private CapsuleCollider2D _sourceCollider;
    private DraggableItemRectTransformController _rectTransformController;

    public ShelvingController(CapsuleCollider2D sourceCollider, DraggableItemRectTransformController rectTransformController)
    {
        _sourceCollider = sourceCollider;
        _rectTransformController = rectTransformController;
    }

    //Получение ближайшей точки коллайдера
    private Vector3 SnapToClosestPoint(Collider2D targetCollider)
    {
        //Центр коллайдера яблока
        Vector3 sourceCenter = _sourceCollider.bounds.center;

        //Ближайшая точка коллайдера поверхности
        Vector3 closestPoint = new Vector3(targetCollider.ClosestPoint(sourceCenter).x, targetCollider.bounds.center.y);

        //Отклонение ближайшей точки коллайдера поверхности от центра яблока, нужно что бы понять стоит ли яблоко на краю или нет
        float deltaX = closestPoint.x - sourceCenter.x;

        Vector3 targetPoint;

        //Если отклонение большое, то целевая точка это позиция на половину коллайдера яблока левее или правее.
        if (deltaX > GameConstants.ClosestColliderPointMaxDelta)
        {
            targetPoint = new Vector3(closestPoint.x + _sourceCollider.bounds.size.x / 2, closestPoint.y);
        }
        else if (deltaX < -GameConstants.ClosestColliderPointMaxDelta)
        {
            targetPoint = new Vector3(closestPoint.x - _sourceCollider.bounds.size.x / 2, closestPoint.y);
        }
        //Если отклонение маленькое то целевая точка это ближайшая точка коллайдера поверхности
        else { 
            targetPoint = closestPoint;
        }

        return targetPoint;
    }

    //Корутина постановки яблока на полку
    public IEnumerator ShelvingCoroutine(Collider2D targetCollider)
    {
        //Получение точки постановки яблока
        Vector3 closestPoint =  SnapToClosestPoint(targetCollider);
        //Cумма для того что бы визуально поставить яблоко на поверхность
        Vector3 targetPoint = new Vector3(closestPoint.x, closestPoint.y + _rectTransformController.GetRectTransformHalfHeight() * 2);

        Vector3 startColliderPosition = _sourceCollider.transform.position;

        float timer = 0f;

        //Обычный таймер
        while (timer < GameConstants.FallingToShelfTime)
        {
            float normalizedTimer = timer / GameConstants.FallingToShelfTime;
            timer += Time.deltaTime;

            //От стартовой позиции к поверхности
            _rectTransformController.MoveRectTransform(Vector3.Lerp(startColliderPosition, targetPoint, normalizedTimer));
            yield return null;
        }
    }
}
