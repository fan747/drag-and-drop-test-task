Механика реализована с помощью коллайдеров. 

Абстрактный класс DraggableItem основной, он управляет 3 контроллерами: 
  FallingController - контроллер падения;
  ShelvingController - контроллер "приклеивания" к полкам;
  DraggableItemRectTransformController - контроллер rectTransforn.
  
Когда игрок держит яблоко, яблоко перемещается при помощи DraggableItemRectTransformController, методами OnDrag, OnBeginDrag.

Когда игрок отпускает яблоко, если оно не на полу, то начинается корутина ( в классе DraggableItem метод OnEndDrag) , которая перемещает предмет вниз за некоторое время, 
точка падения определяется в классе FallingController, в начале корутины FallingCoroutine, при помощи рейкаста находиться любая поверхность ( метод GetBottomColliderPoint) , после чего яблоко перемещается туда

Если на пути яблока во время падения встретится полка ( в классе DraggableItem метод OnTriggerEnter2D ),  то в классе ShelvingController запуститься корутина "прилипания" к полке ShelvingCoroutine, которая возмет
ближайшую точку коллайдера полки ( метод SnapToClosestPoin ) и переместит яблоко туда.

Перемещение происходит на поверхностях за счет проверок булевых в классе DraggableItem.
