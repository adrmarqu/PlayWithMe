using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script se encarga de la animacion de abrir el cubo de pintura (parte destornillador)

public class PlugAnimation : MonoBehaviour
{
    GameObject screw;
    GameObject plug;

    void Awake()
    {
        screw = gameObject.transform.parent.parent.GetComponent<BucketController>().GetScrewObject();
    }

    // Void para iniciar animación de la primera tapa del cubo
    public void RemoveFirstPlug()
    {
        plug = gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;

        plug.GetComponent<BucketPlugAnimation>().RemovePlug();
    }

    // Void para iniciar animación de la segunda tapa del cubo
    public void RemoveSecondPlug()
    {
        plug = gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;

        plug.GetComponent<BucketPlugAnimation>().RemovePlug();
    }

    // Void al acabar la animación
    public void FinalizeAnimation()
    {
        screw.SetActive(true);
        gameObject.SetActive(false);
    }
}
