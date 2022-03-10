using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler
{
    [SerializeField] private Camera _cam;
    private Vector3 leftBound, rightBound;
    private float xRange = 6f;
    private float timeToMove = 0.5f;
    // Управление по умолчанию Drag
    private bool dragEnabled = true;

    private void Start()
    {
        // Определение границ передвижения
        leftBound = new Vector3(-xRange, transform.position.y, transform.position.z);
        rightBound = new Vector3(xRange, transform.position.y, transform.position.z);
    }

    private void OnEnable()
    {
        // Подписка на события по изменению управления
        DropboxHandler.ControlsToArrows += EnableArrows;
        DropboxHandler.ControlsToDrag += EnableDrag;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Конвертация координат экрана в глобальные
        Vector3 mousePoint = _cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f));
        // Определение направления перетаскивания относительно положения мыши по оси х
        Vector3 dragPosition = new Vector3(mousePoint.x, transform.position.y, transform.position.z);

        // Проверка ограничений движения
        if (dragPosition.x <= -xRange)
        {
            dragPosition = leftBound;
            
        }
        if (dragPosition.x >= xRange)
        {
            dragPosition = rightBound;
        }

        // Перетаскивание мышью только если выбран вариант управления Drag
        if (dragEnabled)
        {
            StartCoroutine(LerpPosition(dragPosition, timeToMove));
        }
    }

    private void Update()
    {
        // Передвижение клавишами если выбран вариант управления Arrow Keys
        // и только в доступных границах
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !dragEnabled)
        {
            StartCoroutine(LerpPosition(leftBound, timeToMove));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !dragEnabled)
        {
            StartCoroutine(LerpPosition(rightBound, timeToMove));
        }
    }

    private void EnableDrag()
    {
        // Включает drag, если он был выбран в меню паузы
        dragEnabled = true;
        Debug.Log("Drag Enabled");
    }
    private void EnableArrows()
    {
        // Выключает drag, если в меню паузы были выбраны Arrow Keys,
        // чтобы передвижение с помощию клавиш стало доступно
        dragEnabled = false;
        Debug.Log("Arrows Enabled");
    }

    // передвижение с задержкой
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
