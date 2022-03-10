using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ControlsData
{
    public int controlsMode;

    public ControlsData(DropboxHandler dropdown)
    {
        controlsMode = dropdown.controlsMode;
    }
}
