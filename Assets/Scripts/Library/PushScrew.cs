using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script tira el destornillador al suelo cuando choca con el coche

public class PushScrew : MonoBehaviour
{
    Rigidbody _rb;
    bool disabled;

    void Start()
    {
        disabled = false;
        _rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!disabled)
        {
            _rb.AddForce(transform.forward * -300.0f);

            disabled = true;
        }
    }
}
