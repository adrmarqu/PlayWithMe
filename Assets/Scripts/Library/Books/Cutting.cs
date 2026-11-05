using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script desactiva las tijeras animadas y activa las fisicas

public class Cutting : MonoBehaviour
{
    GameObject scissor;
    DestroyBook _destroy;

    void Start()
    {
        _destroy = transform.parent.GetComponent<DestroyBook>();  // Componente
        scissor = _destroy.GetScissorObj();    // Objeto de tijeras
    }

    // Esta funcion la llama la animación de cortar

    public void CuttingBook()
    {
        _destroy.CutBook();
        scissor.SetActive(true);
        gameObject.SetActive(false);
    }
}
