using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    float rotationSpeed;
    float mouseX;
    float saveSpeed;

    InputSystemKeyboard _input;

    void Awake()
    {
        _input = GetComponent<InputSystemKeyboard>();
    }

    void Start()
    {
        rotationSpeed = 3;
        saveSpeed = rotationSpeed;
    }

    void Update()
    {
        CamControl();
    }
    
    void CamControl()
    {
        if(!SettingsManager.ReturnCameraOption())
        {
            if (_input.mouse0 && !MenuPause.gameIsPaused)
            {
                mouseX += _input.mouseX * rotationSpeed;

                transform.eulerAngles = new Vector3(0.0f, mouseX, 0.0f);
            }
        }
        else
        {
            if (_input.mousepresent && !MenuPause.gameIsPaused)
            {
                mouseX += _input.mouseX * rotationSpeed;

                transform.eulerAngles = new Vector3(0.0f, mouseX, 0.0f);
            }
        }
    }

    public void ChangeSpeed(float s)
    {
        rotationSpeed = s;
    }

    public float ReturnSpeed()
    {
        return saveSpeed;
    }
}
