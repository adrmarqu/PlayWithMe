using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script hace la animcaión de quitar la tapa del cubo de pintura
// Esta animacion es controlada por el destornillador

public class BucketPlugAnimation : MonoBehaviour
{
    Animator _anim;

    [SerializeField]
    BucketController _controller;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void RemovePlug()
    {
        _anim.SetTrigger("open");
    }

    public void FinishAnimation()
    {
        _controller.OpenendPlug();

        gameObject.SetActive(false);
    }
}
