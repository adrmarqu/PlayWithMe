using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script se encarga de activar la animacion de la libreria y de cambiar su material

public class LibraryFallSystem : MonoBehaviour
{
    bool libraryDestroyed;          // Si la librería ha sido tirada

    Animator _anim;
    RBControl _rb;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<RBControl>();
    }

    // Easter Egg

    public void LibraryFall()
    {
        if (DestroyBook.bookDestroyed && !libraryDestroyed) // Solo se puede tirar si el libro ha sido destruido o si la libreria no ha sido tirada
        {
            libraryDestroyed = true;

            _rb.AddRigidBody();
            _anim.SetTrigger("fall");
        }
    }
}
