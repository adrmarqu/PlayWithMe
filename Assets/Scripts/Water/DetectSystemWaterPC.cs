using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Script para detectar si un objeto esta en el rango o no

public class DetectSystemWaterPC : MonoBehaviour
{
    GameObject _cup;

    public static event Action<GameObject> detectCupPC = delegate { };

    void OnEnable()
    {
        DestructableSytem.returnCupPC += CupReturn;
    }
    void OnDisable()
    {
        DestructableSytem.returnCupPC -= CupReturn;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == Constants.layerCup)
        {
            if (other.transform.Find("Water").gameObject.activeInHierarchy)
            {
                _cup = other.gameObject;
                _cup.SetActive(false);
                detectCupPC(_cup);
            }
        }
    }

    void CupReturn(GameObject cup)
    {
        cup.SetActive(true);
        Transform water = cup.transform.Find("Water");
        water.gameObject.SetActive(false);
    }

}
