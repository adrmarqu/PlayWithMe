using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Activa y desactiva las tazas de agua correspondientes despues de la animacion

public class Wetting : MonoBehaviour
{
    GameObject cup;

    DestroyBook _destroy;

    void Start()
    {
        _destroy = gameObject.transform.parent.gameObject.GetComponent<DestroyBook>();  // Componente

        cup = gameObject.transform.parent.GetComponent<DestroyBook>().GetCupObj();    // Objeto de taza
    }

    // Esta animacion la llama la animacion de mojar

    public void WettingBook()
    {
        _destroy.WetBook();

        cup.SetActive(true);
        gameObject.SetActive(false);
    }
}
