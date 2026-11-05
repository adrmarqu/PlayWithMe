using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    int type;

    InputSystemKeyboard _input;
    ActiveIcon _aI;

    public static event Action<int> collectObject = delegate { };

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
    }

    void LateUpdate()
    {
        if (_aI.NearObject())
        {
            _input = _aI.SetParent().transform.parent.GetComponent<InputSystemKeyboard>();

            if (_input.interact && _aI.IconEnabled())
            {
                _aI.DisableIcon();
                _aI.enabled = false;
                collectObject(type);
                this.gameObject.SetActive(false);
            }
        }
    }
}
