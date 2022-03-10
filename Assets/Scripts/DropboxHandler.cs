using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DropboxHandler : MonoBehaviour
{
    [SerializeField] private Dropdown _controlsDropbox;
    // �������, �� ������� ����� �����������
    public static event Action ControlsToDrag, ControlsToArrows;
    public int controlsMode;

    void Start()
    {
        // ����������� ���� ����� ������� ���������� ������ � ���������� ������
        _controlsDropbox.onValueChanged.AddListener(delegate { GetDropboxValue(_controlsDropbox); });
    }

     public void GetDropboxValue(Dropdown dropdown)
    {
        controlsMode = dropdown.value;

        // �������� ����������� ����������� � ����������� �� ����
        // ����� ������� ���������� ��� ������
        if (controlsMode == 0)
        {
            ControlsToDrag?.Invoke();
        }
        if (controlsMode == 1)
        {
            ControlsToArrows?.Invoke();
        }
    }

    // ���������� ������ � ��������� �������� ����������
    // ��� ������� ������ Save
    public void SaveControls()
    {
        SaveSystem.SaveControls(this);
        Debug.Log("Controls saved " + controlsMode);
    }

    // �������� ������ � ��������� �������� ����������
    // ��� ������� ������ Load
    public void LoadControls()
    {
        ControlsData data = SaveSystem.LoadControls();

        controlsMode = data.controlsMode;

        if (controlsMode == 0)
        {
            ControlsToDrag?.Invoke();
            _controlsDropbox.SetValueWithoutNotify(0);
        }
        if (controlsMode == 1)
        {
            ControlsToArrows?.Invoke();
            _controlsDropbox.SetValueWithoutNotify(1);
        }
        Debug.Log("Controls loaded " + controlsMode);
    }
}
