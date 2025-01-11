using UnityEngine;
using UnityEngine.EventSystems;

//Абстрактный класс "переносимого" предмета, наследующий соответсвубщий интерфейс IDraggable, а также наследующий интерфейсы для работы с "зажиманием"
public abstract class DraggableItem : MonoBehaviour, IDraggable, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //Классы контроллеры
    private FallingController _fallingController;
    private DraggableItemRectTransformController _rectTransformController;
    private ShelvingController _colliderSnapper;

    private bool _onFloor = false;
    private bool _onDrag;

    private void Start()
    {
        //Инициализация классов контроллеров
        _rectTransformController = new(GetObject<RectTransform>(), GetObjectInParent<Canvas>());
        _fallingController = new(_rectTransformController);
        _colliderSnapper = new(GetObject<CapsuleCollider2D>(), _rectTransformController);

        //если не на полу, то падает
        if (!_onFloor)
        {
            StartCoroutine(_fallingController.FallingCoroutine());
        }
    }

    //Методы для получения компонентов и обработки ошибок
    private T GetObject<T>() where T : Object
    {
        var gameObject = GetComponent<T>();
        if (gameObject == null)
        {
            throw new MissingComponentException($"{typeof(T)} not found on {gameObject.name}");
        }
        return gameObject;
    }

    private T GetObjectInParent<T>() where T : Object
    {
        var gameObject = GetComponentInParent<T>();
        if (gameObject == null)
        {
            throw new MissingComponentException($"{typeof(T)} not found on {gameObject.name}");
        }
        return gameObject;
    }
    //

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _onDrag = true;

        // Остановка всех корутин, для того чтобы, можно было "поймать" яблоко
        StopAllCoroutines();

        // Получение позиций курсора и яблока в классе контроллера
        _rectTransformController.OnBeginDrag(eventData);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        // Перемещение яблока в классе контроллера
        _rectTransformController.OnDrag(eventData);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _onDrag = false;

        //Если не на поверхности, начинает падать до коллайдера под яблоком
        if (!_onFloor)
        {
            StartCoroutine(_fallingController.FallingCoroutine());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Если не на полу и не на полке, то _onFloor = true
        if (collision.CompareTag(GameConstants.FloorColliderTagName) || collision.CompareTag(GameConstants.ShelfColliderTagName))
        {
            if (!_onFloor)
            {
                _onFloor = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Если не на полу и не на полке, то _onFloor = false
        if (collision.CompareTag(GameConstants.FloorColliderTagName) || collision.CompareTag(GameConstants.ShelfColliderTagName))
        {
            _onFloor = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Если яблоко не взято и оно падая попадает на полку, то остановка корутин и начанается корутина постановки на полку
        if (collision.CompareTag(GameConstants.ShelfColliderTagName) && !_onDrag)
        {
            StopAllCoroutines();

            StartCoroutine(_colliderSnapper.ShelvingCoroutine(collision));
        }
    }
}
