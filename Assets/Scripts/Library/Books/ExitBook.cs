using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script es para cuando sales del libro 

public class ExitBook : MonoBehaviour
{
    GameObject[] openBooks = new GameObject[10];

    void Start()
    {

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            openBooks[i] = gameObject.transform.GetChild(i).gameObject;
        }

        desactiveCanvas();
    }

    // Desactiva el libro y pone el juego en marcha

    public void desactiveCanvas()
    {
        // Desactivar canvas

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            openBooks[i].SetActive(false);
        }

        // Quitar pausa, tiempo normal y quitar libro

        MenuPause.gameIsPaused = false;
        MenuPause.pauseOpen = false;
        Time.timeScale = 1.0f;
        OpenBook.bookOpened = false;
    }
}
