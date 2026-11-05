using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrawlSystem : MonoBehaviour
{
    [SerializeField]
    TriggerDownBed _tDB;

    PlayerMovement _pM;
    Animator _animPlayer;
    Rigidbody _rb;
    InputSystemKeyboard _input;
    CapsuleCollider _cC;
    CarryingObjectBehaviour _cOB;

    Vector3 NotCrowdedCenter;
    bool isCrowded;
    float NotCrowdedHeigh;
    int NotCrowdedDirection;

    public event Action SetCameraPosition = delegate { };

    void OnEnable()
    {
        GetComponent<InputSystemKeyboard>().Crawl += DoCrawl;
    }

    void OnDisable()
    {
        GetComponent<InputSystemKeyboard>().Crawl -= DoCrawl;
    }

    void Awake()
    {
        _pM = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
        _animPlayer = GetComponent<Animator>();
        _input = GetComponent<InputSystemKeyboard>();
        _cC = GetComponent<CapsuleCollider>();
        _cOB = GetComponent<CarryingObjectBehaviour>();
    }

    void Start()
    {
        isCrowded = false;
        NotCrowdedCenter = _cC.center;
        NotCrowdedHeigh = _cC.height;
        NotCrowdedDirection = _cC.direction;
    }
    
    void Update()
    {
        if (isCrowded)
        {
            _pM.ChangeSpeed(0.5f);

            if (_input.ver != 0)
            {
                _animPlayer.SetInteger("Crawl", 2);
            }
            else
            {
                _animPlayer.SetInteger("Crawl", 1);
            }
        }
    }

    public void DoCrawl()
    {
        if (!isCrowded && (_cOB.CarryingObjectStatus() == (false, false) || _cOB.CarryingObjectStatus() == (false, true)))
        {
            _rb.velocity = transform.forward * _pM.ReturnCurrentSpeed();
            _animPlayer.SetInteger("Crawl", 1);
            _cC.height = 2;
            _cC.center = new Vector3(0, 0.5f, 0.5f);
            _cC.direction = 2;
            isCrowded = true;
        }
        else if (isCrowded && (_cOB.CarryingObjectStatus() == (false, false) || _cOB.CarryingObjectStatus() == (false, true)))
        {
            if (!_tDB.CanStandUp())
            {
                _animPlayer.SetInteger("Crawl", 0);
                _cC.height = NotCrowdedHeigh;
                _cC.center = NotCrowdedCenter;
                _cC.direction = NotCrowdedDirection;
                isCrowded = false;
            }
        }
        SetCameraPosition();
    }
    public bool CheckIfIsCrowded()
    {
        return isCrowded;
    }
}
