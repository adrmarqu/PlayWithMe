using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Este script cambia la imagen que se ve en el canvas del destruible

public class DestroyedBookMaterial : MonoBehaviour
{
    [SerializeField]
    Image[] paintImage;  // Imagenes de la pintura

    GameObject[] canvasImages = new GameObject[5];  // Imagenes mojadas y cortadas

    GameObject paint;

    void Start()
    {
        // Conseguir el objeto/imagen y desactivarlo

        for (int i = 0; i < gameObject.transform.GetChild(3).GetChild(1).childCount; i++)
        {
            canvasImages[i] = gameObject.transform.GetChild(3).GetChild(1).GetChild(i).gameObject;
            canvasImages[i].SetActive(false);
        }

        // Activar la primera imagen (la imagen predeterminada)

        canvasImages[0].SetActive(true);

        // Desactivar objeto de pintura

        paint = paintImage[0].gameObject.transform.parent.gameObject;
        paint.SetActive(false);
    }

    // Activar la pintura

    public void BookPainted()
    {
        paint.SetActive(true);

        for (int i = 0; i < paintImage.Length; i++)
        {
            paintImage[i].color = SelectPaintingColor.colorPainting;
        }
    }

    // Activar la pagina mojada

    public void BookWet()
    {
        canvasImages[0].SetActive(false);
        canvasImages[1].SetActive(true);
    }

    // Activar la pagina cortada

    public void BookCut()
    {
        canvasImages[0].SetActive(false);
        canvasImages[2].SetActive(true);
    }
}
