using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDownBed : MonoBehaviour
{
    bool _cSU;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _cSU = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _cSU = false;
        }
    }

    public bool CanStandUp()
    {
        return _cSU;
    }
}
