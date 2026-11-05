using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWater : MonoBehaviour
{
    bool objTouched;

    void Awake()
    {
        objTouched = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            objTouched = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        objTouched = false;
    }

    // Devolver si el objeto esta dentro del rango o no

    public bool GetCurrentState()
    {
        return objTouched;
    }
}
