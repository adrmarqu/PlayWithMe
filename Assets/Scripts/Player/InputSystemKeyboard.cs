using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System;
using UnityEngine;

public class InputSystemKeyboard : MonoBehaviour
{
    public float hor { get; private set; }
    public float ver { get; private set; }
    public float mouseX { get; private set; }
    public bool interact { get; private set; }
    public bool run { get; private set; }
    public bool pull { get; private set; }
    public bool escape { get; private set; }
    public bool hammerHit { get; private set; }
    public bool endGame { get; private set; }
    public bool mouse0 { get; private set; }
    public bool mousepresent { get; private set; }

    //Jump event
    public event Action Jump = delegate { };

    //Crawl Event
    public event Action Crawl = delegate { };

    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        interact = Input.GetKeyUp(KeyCode.E);
        run = Input.GetKey(KeyCode.LeftShift);
        pull = Input.GetKeyUp(KeyCode.Q);
        escape = Input.GetKeyUp(KeyCode.Escape);
        hammerHit = Input.GetKeyUp(KeyCode.M);
        endGame = Input.GetKeyDown(KeyCode.L);
        mouse0 = Input.GetMouseButton(0);
        mousepresent = Input.mousePresent;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            Crawl();
        }

    }
}
