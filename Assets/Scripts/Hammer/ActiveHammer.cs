using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ActiveHammer : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    HammerDestroyer _hD;

    Animator _anim;
    int numChilds;
    Rigidbody _rb;

    JumpSystem _jS;
    CrawlSystem _cS;
    InputSystemKeyboard _input;

    void OnEnable()
    {
        HammerDestroyer.EnableHammer += ActivateHammer;
    }
    void OnDisable()
    {
        HammerDestroyer.EnableHammer -= ActivateHammer;
    }

    void Awake()
    {
        numChilds = transform.childCount;
        _anim = player.GetComponent<Animator>();
        _rb = player.GetComponent<Rigidbody>();
    }

    void Start()
    {
        _jS = player.GetComponent<JumpSystem>();
        _cS = player.GetComponent<CrawlSystem>();
        _input = player.GetComponent<InputSystemKeyboard>();
    }

    void FixedUpdate()
    {
        if(_hD.ReturnTakenStatus() && !_cS.CheckIfIsCrowded() && _jS.IsTouchingGround())
        {
            if (_input.hammerHit)
            {
                _anim.SetTrigger("Punch");
                _rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
    public void ActivateHammer()
    {
        if (_hD.ReturnTakenStatus())
        {
            for (int i = 0; i < numChilds; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < numChilds; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
