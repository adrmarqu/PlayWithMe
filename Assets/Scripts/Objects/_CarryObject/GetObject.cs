using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject : MonoBehaviour
{
    Transform originalParent;
    InputSystemKeyboard _input;
    CarryingObjectBehaviour _cOB;
    DropObject _dO;
    ActiveIcon _aI;
    CrawlSystem _cS;

    bool getObject;

    void Awake()
    {
        _dO = GetComponent<DropObject>();
        _aI = GetComponent<ActiveIcon>();
    }

    void Start()
    {
        getObject = false;
        originalParent = transform.parent;
    }

    void LateUpdate()
    {
        if (_aI.NearObject())
        {
            _input = _aI.SetParent().transform.parent.GetComponent<InputSystemKeyboard>();
            _cOB = _aI.SetParent().transform.parent.GetComponent<CarryingObjectBehaviour>();
            _cS = _aI.SetParent().transform.parent.GetComponent<CrawlSystem>();

            if (_input.interact && _cOB.CarryingObjectStatus() == (false, true) && _aI.IconEnabled() && !_cS.CheckIfIsCrowded())
            {
                transform.parent = _aI.SetParent();
                _cOB.OneIconActive(getObject);
                getObject = true;
                _cOB.ObjectObtained(getObject, transform.gameObject);
                _dO.WithObjectStatus(getObject);
            }
        }
    }

    public void ChangeObjectStatus(bool gO)
    {
        getObject = gO;
    }

    public bool ObjectStatus()
    {
        return getObject;
    }

    public Transform MoveToOriginalParent()
    {
        return originalParent;
    }
}
