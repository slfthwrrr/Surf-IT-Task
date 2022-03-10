using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DropboxHandler : MonoBehaviour
{
    [SerializeField] private Dropdown _controlsDropbox;
    // События, на которые можно подписаться
    public static event Action ControlsToDrag, ControlsToArrows;
    public int controlsMode;

    void Start()
    {
        // Определение того какой вариант управления выбран в выпадающем списке
        _controlsDropbox.onValueChanged.AddListener(delegate { GetDropboxValue(_controlsDropbox); });
    }

     public void GetDropboxValue(Dropdown dropdown)
    {
        controlsMode = dropdown.value;

        // Отправка уведомлений подписчикам в зависимости от того
        // какой вариант управления был выбран
        if (controlsMode == 0)
        {
            ControlsToDrag?.Invoke();
        }
        if (controlsMode == 1)
        {
            ControlsToArrows?.Invoke();
        }
    }

    // Сохранение данных о выбранном варианте управления
    // при нажатии кнопки Save
    public void SaveControls()
    {
        SaveSystem.SaveControls(this);
        Debug.Log("Controls saved " + controlsMode);
    }

    // Загрузка данных о выбранном варианте управления
    // при нажатии кнопки Load
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
