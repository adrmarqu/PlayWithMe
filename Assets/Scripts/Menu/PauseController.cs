using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script controla las animaciones del menu de pausa y la escala de tiempo asi como la primera pantalla de pausa
public class PauseController : MonoBehaviour
{
    Animator _anim;
    InicializeMenu _menu;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _menu = GetComponent<InicializeMenu>();
    }

    public void OpenPause() // Abrir el panel
    {
        _menu.Inicialize();         // Que siempre empieze por la primera pantalla
        _anim.SetTrigger("open");   // Animación para abrir pausa
    }

    public void ClosePause()    // Cerrar el panel
    {
        if (transform.gameObject.activeInHierarchy)
        {
            _anim.SetTrigger("close");  // Animacion para cerrar pausa
            Time.timeScale = 1.0f;      // Volver al flujo de tiempo normal
        }
    }

    public void GametoPause()   
    {
        MenuPause.gameIsPaused = true;  // Estado del juego parado 
        Time.timeScale = 0f;            // Parar el flujo de tiempo del juego
    }

    public void PauseToGame()   
    {
        MenuPause.gameIsPaused = false; // Estado del juego sin parar
        MenuPause.pauseOpen = false;
        gameObject.SetActive(false);    // Desactivar el panel
    }
}
