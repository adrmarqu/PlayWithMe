using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script detecta donde y si tienes los objetos adecuados puedes tirar la libreria

public class ShotLibrary : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    LibraryFallSystem _fall;

    void Start()
    {
        _fall = target.GetComponent<LibraryFallSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Destructor"))
        {
            _fall.LibraryFall();
        }
    }

    public void ThrowLibrary()
    {
        _fall.LibraryFall();
    }
}
