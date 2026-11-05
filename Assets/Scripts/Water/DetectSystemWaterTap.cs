using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Script para detectar si un objeto esta en el rango o no

public class DetectSystemWaterTap : MonoBehaviour
{
    GameObject _cup;

    public static event Action<int> detectCup = delegate { };

    void OnEnable()
    {
        WaterHandle.returnCup += CupReturn;
    }
    void OnDisable()
    {
        WaterHandle.returnCup -= CupReturn;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Constants.layerCup)
        {
            _cup = other.gameObject;
            _cup.SetActive(false);
            detectCup(1);
        }
    }

    void CupReturn(int k)
    {
        _cup.SetActive(true);
        Transform water =_cup.transform.Find("Water");
        water.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
