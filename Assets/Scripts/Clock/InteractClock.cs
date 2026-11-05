using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractClock : MonoBehaviour
{
    [SerializeField]
    InputSystemKeyboard _input;

    ActiveIcon _aI;

    bool openedTimer;

    public event Action ShowCanvas = delegate { };

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
    }

    void Update()
    {
        if (_aI.NearObject())
        {
            if (_input.interact && _aI.IconEnabled() == true && openedTimer == false)
            {
                ShowCanvas();
                openedTimer = true;
            }
            else if (_input.interact && _aI.IconEnabled() == true && openedTimer == true)
            {
                ShowCanvas();
                openedTimer = false;
            }
        }
        else
        {
            openedTimer = false;
            ShowCanvas();
        }
    }

    public bool ReturnTimerStatus()
    {
        return openedTimer;
    }
}
