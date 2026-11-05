using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script desactiva el cubo de pintura animado y activa el fisico

public class Painting : MonoBehaviour
{
    GameObject bucket;

    GameObject paint;

    Renderer _rend;
    DestroyBook _destroy;

    void Start()
    {
        _destroy = gameObject.transform.parent.gameObject.GetComponent<DestroyBook>();  // Componente

        paint = _destroy.GetPaintSphere();  // Objeto esfera de pintura (la pintura del cubo)

        _rend = paint.GetComponent<Renderer>();

        _rend.material.color = SelectPaintingColor.colorPainting;

        bucket = gameObject.transform.parent.GetComponent<DestroyBook>().GetBucketObj();    // Cubo de pintura objeto
    }

    // Esta funcion la llama la animación de pintar

    public void PaintingBook()
    {
        _destroy.PaintBook();

        bucket.SetActive(true);
        gameObject.SetActive(false);
    }
}
