using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script elige el color de la pintura

public class SelectPaintingColor : MonoBehaviour
{
    public static Color colorPainting;  // Color seleccionado

    [SerializeField]
    Color[] colors;              // Lista de colores

    Renderer _rend;

    void Start()
    {
        _rend = GetComponent<Renderer>();

        ChangeColor();
    }

    void ChangeColor()
    {
        int x = Random.Range(0, colors.Length);

        _rend.material.color = colorPainting = colors[x];
    }
}
