using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterHandle : MonoBehaviour
{
    [SerializeField]
    GameObject waterEmitter;
    [SerializeField]
    GameObject waterCup;
    [SerializeField]
    Transform handle;

    Animator _anim;
    Animator _animWater;

    bool activeWater;
    float timerWater;
    bool brokenHandle;

    public static event Action<int> returnCup = delegate { };

    void OnEnable()
    {
        DetectSystemWaterTap.detectCup += FillCup;
    }
    void OnDisable()
    {
        DetectSystemWaterTap.detectCup -= FillCup;
    }

    void Awake()
    {
        _anim = handle.GetComponent<Animator>();
        _animWater = waterEmitter.GetComponent<Animator>();
    }
    void Start()
    {
        activeWater = false;
        timerWater = 6;
        brokenHandle = false;
    }

    void Update()
    {
        if (activeWater && timerWater > 0)
        {
            _anim.SetBool("Open", true);
            _animWater.SetBool("Close", false);
            waterEmitter.SetActive(true);
            waterCup.SetActive(true);
            timerWater = timerWater - Time.deltaTime;
        }
        else
        {
            _anim.SetBool("Open", false);
        }

        if (timerWater<0)
        {
            activeWater = false;
            StartCoroutine(DeactivateWater(1));
        }
        
    }
    IEnumerator DeactivateWater(float _delay)
    {
        _animWater.SetBool("Close", true);
        yield return new WaitForSeconds(_delay);
        waterEmitter.SetActive(false);
        waterCup.SetActive(false);
        returnCup(1);
        brokenHandle = true;
        handle.gameObject.SetActive(false);
    }

    void FillCup(int k)
    {
        if (!brokenHandle)
        {
            activeWater = true;
            timerWater = 6;
        }
    }
}
