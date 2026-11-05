using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasClock : MonoBehaviour
{
    [SerializeField]
    InteractClock _iC;

    Image backgroundCanvas;
    Text textTimer;

    void Awake()
    {
        backgroundCanvas = GetComponent<Image>();
        textTimer = GetComponentInChildren<Text>();
    }
    void OnEnable()
    {
        _iC.ShowCanvas += ShowTimer;
    }

    public void ShowTimer()
    {
        if (_iC.ReturnTimerStatus())
        {
            backgroundCanvas.enabled = true;
            textTimer.enabled = true;
        }
        else
        {
            backgroundCanvas.enabled = false;
            textTimer.enabled = false;
        }
    }
   
}
