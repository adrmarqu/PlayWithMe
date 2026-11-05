using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HammerDestroyer : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    HammerCollider _hC;
    [SerializeField]
    GameObject _hP;
    [SerializeField]
    GameObject parentObjectHammer;

    InputSystemKeyboard _input;
    CarryingObjectBehaviour _cOB;
    BoxCollider[] _bc;
    Rigidbody _rb;
    ActiveIcon _aI;
    int numChilds;
    int numBoxColliders;
    bool taken;

    public static event Action EnableHammer = delegate { };

    void Awake()
    {
        _bc = GetComponents<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        _aI = GetComponent<ActiveIcon>();
    }

    void Start()
    {
        numChilds = transform.childCount;
        numBoxColliders = _bc.Length;
        _cOB = player.GetComponent<CarryingObjectBehaviour>();
        _input = player.GetComponent<InputSystemKeyboard>();
    }
    void Update()
    {
        if(_input.interact && Vector3.Distance(transform.position, player.transform.position) < 0.5f && _cOB.CarryingObjectStatus() == (false, true) && !taken)
        {
            _aI.DisableIcon();
            for(int i = 0; i < numChilds; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            for(int i = 0; i < numBoxColliders; i++)
            {
                _bc[i].enabled = false;
            }

            _rb.constraints = RigidbodyConstraints.FreezeAll;
            taken = true;
            EnableHammer();
        }
        else if(_input.interact && taken)
        {
            for (int i = 0; i < numBoxColliders; i++)
            {
                _bc[i].enabled = true;
            }

            for (int i = 0; i < numChilds; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            _rb.constraints = RigidbodyConstraints.None;
            taken = false;
            EnableHammer();
        }

        if(taken)
        {
            _aI.DisableIcon();
            transform.position = _hP.transform.position;
        }

        if(_hC.CheckIfHammerIsDestroyed())
        {
            DestroyHammer();
        }
    }

    public void DestroyHammer()
    {
        taken = false;
        for (int i = 0; i < numChilds; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
        }
        for (int i = 0; i < numChilds; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < numBoxColliders; i++)
        {
            Destroy(_bc[i]);
        }

        Destroy(gameObject.transform.GetComponent<DropObject>());
        Destroy(gameObject.transform.GetComponent<ActiveIcon>());
        Destroy(gameObject.transform.GetComponent<GetObject>());
        Destroy(gameObject.transform.GetComponent<CarryObject>());

        _cOB.ObjectObtained(false, null);
        gameObject.transform.SetParent(parentObjectHammer.transform);
        _hC.ChangeUsedStatus(false);
    }

    public bool ReturnTakenStatus()
    {
        return taken;
    }
}
