using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.Image;

//Контроллер падения предмета
public class FallingController
{
    //Контроллер RectTransform, нужен для его передвижения
    private DraggableItemRectTransformController _transformController;
    public FallingController(DraggableItemRectTransformController transformController)
    {
        _transformController = transformController;
    }

    //Поиск "пола" под предметом
    private Vector3 GetBottomColliderPoint()
    {
        //рейкаст в пол на условную длинну 100 и с маской Floor
        RaycastHit2D hit2D = Physics2D.Raycast(_transformController.GetPosition(), Vector3.down, GameConstants.RaycastDistance, LayerMask.GetMask(GameConstants.FloorLayerName));
        return hit2D.point;
    }

    //Корутина "падения"
    public IEnumerator FallingCoroutine()
    {
        float timer = 0f;
        float normalizedTimer = timer / GameConstants.FallingTime;
        //точка падения + половина спрайта предмета, что бы визуально поставить его не на край спрайта.
        Vector3 fallPoint = new Vector3 (GetBottomColliderPoint().x, GetBottomColliderPoint().y + _transformController.GetRectTransformHalfHeight());
        //стартовая точка rectTransform
        Vector3 startRectTransformPosition = _transformController.GetPosition();

        //обычный таймер
        while (timer < GameConstants.FallingTime)
        {
            timer += Time.deltaTime;

            //передвижение rectTransform через контроллер 
            _transformController.MoveRectTransform(Vector3.Lerp(startRectTransformPosition, fallPoint, normalizedTimer));
            yield return null;
        }
    }
}

