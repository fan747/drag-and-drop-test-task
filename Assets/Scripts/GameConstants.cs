using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Константы
public static class GameConstants
{
    //Назначения видны в названиях
    public const string FloorColliderTagName = "FloorCollider";
    public const string ShelfColliderTagName = "ShelfCollider";
    public const float ClosestColliderPointMaxDelta = 0.05f;
    public const float RaycastDistance = 1000f;
    public const string FloorLayerName = "Floor";
    public const float FallingTime = 0.5f;
    public const float FallingToShelfTime = 0.2f;

    //Условные делители и умножители для "красивой" постановки предмета
    public const int ColliderToShelfHeightMultiplie = 4;
    public const float RectTransformHeightSeparator = 5;
}


