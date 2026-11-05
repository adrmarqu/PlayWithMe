using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Script para detectar si un objeto esta en el rango o no

public class DetectSystemWaterPhone : MonoBehaviour
{
    GameObject _cup;
    Transform _parent;

    public static event Action<GameObject> detectCupPhone = delegate { };

    void OnEnable()
    {
        MobileDestroyed.returnCupPhone += CupReturn;
    }
    void OnDisable()
    {
        MobileDestroyed.returnCupPhone -= CupReturn;
    }

    private void Awake()
    {
        _parent = transform.parent.transform;
    }

    void OnTriggerStay(Collider other)
    {
        if((_parent.rotation.eulerAngles.x <= 91 && _parent.rotation.eulerAngles.x >=89) ||
            _parent.rotation.eulerAngles.x <= 271 && _parent.rotation.eulerAngles.x >= 269)
        {
            if (other.gameObject.layer == Constants.layerCup)
            {
                if (other.transform.Find("Water").gameObject.activeInHierarchy)
                {
                    if(_parent.rotation.eulerAngles.x <= 271 && _parent.rotation.eulerAngles.x >= 269)
                    {
                        _parent.Rotate(new Vector3(180, _parent.rotation.y, _parent.rotation.z));
                    }
                    _cup = other.gameObject;
                    _cup.SetActive(false);
                    detectCupPhone(_cup);
                }
            }
        }
        else
        {
            Debug.Log(transform.parent.transform.rotation.eulerAngles.x);
        } 
    }

    void CupReturn(GameObject cup)
    {
        cup.SetActive(true);
        Transform water = cup.transform.Find("Water");
        water.gameObject.SetActive(false);
    }
}
