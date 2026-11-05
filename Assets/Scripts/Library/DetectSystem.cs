using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para detectar si un objeto esta en el rango o no

public class DetectSystem : MonoBehaviour
{
    bool objTouched;

    void Awake()
    {
        objTouched = false;
    }

    void OnTriggerEnter(Collider other)
    {
        objTouched = true;
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
