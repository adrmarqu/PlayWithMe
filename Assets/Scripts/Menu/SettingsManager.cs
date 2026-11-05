using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropDown;

    static bool cameraDragRotation;

    void Start()
    {
        cameraDragRotation = false;
    }

    void Update()
    {
        if(dropDown.captionText.text == "Mouse Drag")
        {
            cameraDragRotation = true;
        }
        else
        {
            cameraDragRotation = false;
        }
    }

    public static bool ReturnCameraOption()
    {
        return cameraDragRotation;
    }
}
