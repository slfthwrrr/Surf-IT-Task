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
    // ���������� �� ��������� Drag
    private bool dragEnabled = true;

    private void Start()
    {
        // ����������� ������ ������������
        leftBound = new Vector3(-xRange, transform.position.y, transform.position.z);
        rightBound = new Vector3(xRange, transform.position.y, transform.position.z);
    }

    private void OnEnable()
    {
        // �������� �� ������� �� ��������� ����������
        DropboxHandler.ControlsToArrows += EnableArrows;
        DropboxHandler.ControlsToDrag += EnableDrag;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ����������� ��������� ������ � ����������
        Vector3 mousePoint = _cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f));
        // ����������� ����������� �������������� ������������ ��������� ���� �� ��� �
        Vector3 dragPosition = new Vector3(mousePoint.x, transform.position.y, transform.position.z);

        // �������� ����������� ��������
        if (dragPosition.x <= -xRange)
        {
            dragPosition = leftBound;
            
        }
        if (dragPosition.x >= xRange)
        {
            dragPosition = rightBound;
        }

        // �������������� ����� ������ ���� ������ ������� ���������� Drag
        if (dragEnabled)
        {
            StartCoroutine(LerpPosition(dragPosition, timeToMove));
        }
    }

    private void Update()
    {
        // ������������ ��������� ���� ������ ������� ���������� Arrow Keys
        // � ������ � ��������� ��������
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
        // �������� drag, ���� �� ��� ������ � ���� �����
        dragEnabled = true;
        Debug.Log("Drag Enabled");
    }
    private void EnableArrows()
    {
        // ��������� drag, ���� � ���� ����� ���� ������� Arrow Keys,
        // ����� ������������ � ������� ������ ����� ��������
        dragEnabled = false;
        Debug.Log("Arrows Enabled");
    }

    // ������������ � ���������
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
