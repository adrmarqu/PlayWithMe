using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LockCanvasBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject lockObj;
    [SerializeField]
    GameObject player;

    LockPuzzleBehaviour lPB;
    InputSystemKeyboard _input;

    void Awake()
    {
        lPB = lockObj.GetComponent<LockPuzzleBehaviour>();
        _input = player.GetComponent<InputSystemKeyboard>();
    }

    void Update()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy && _input.escape)
        {
            CloseCanvas();
        }
    }

    public void CloseCanvas()
    {
        lPB.CloseCanvas();
    }
}