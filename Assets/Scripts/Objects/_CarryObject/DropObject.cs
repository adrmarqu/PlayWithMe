using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    Rigidbody _rB;
    InputSystemKeyboard _input;
    CarryingObjectBehaviour _cOB;
    GetObject _gO;
    ActiveIcon _aI;

    bool withObject;
    float objMass;

    private void OnEnable()
    {
        CarryingObjectBehaviour.Drop += ObjectDrop;
    }
    private void OnDisable()
    {
        CarryingObjectBehaviour.Drop -= ObjectDrop;
    }

    void Awake()
    {
        _rB = GetComponent<Rigidbody>();
        _gO = GetComponent<GetObject>();
        _aI = GetComponent<ActiveIcon>();
    }

    void Start()
    {
        withObject = false;
        objMass = _rB.mass;
    }

    void FixedUpdate()
    {
        if(transform.parent.name == "HandPosition")
        {
            _input = transform.parent.parent.GetComponent<InputSystemKeyboard>();
            _cOB = transform.parent.parent.GetComponent<CarryingObjectBehaviour>();

            if (_cOB.CarryingObjectStatus() == (true, false) && _input.interact && !_aI.IconEnabled())
            {
                withObject = false;
                _cOB.ObjectObtained(withObject, transform.gameObject);
                _cOB.OneIconActive(withObject);
                _gO.ChangeObjectStatus(withObject);
                _rB.useGravity = true;
                _rB.drag = 0;
                _rB.mass = objMass;
                _rB.constraints = RigidbodyConstraints.None;
                transform.parent = _gO.MoveToOriginalParent();
            }
        }
    }
    public void WithObjectStatus(bool wO)
    {
        withObject = wO;
    }

    public void ObjectDrop()
    {
        if(_cOB != null)
        {
            withObject = false;
            _cOB.ObjectObtained(withObject, transform.gameObject);
            _gO.ChangeObjectStatus(withObject);
            _rB.useGravity = true;
            _rB.drag = 0;
            _rB.constraints = RigidbodyConstraints.None;
            transform.parent = _gO.MoveToOriginalParent();
        }
    }
}
