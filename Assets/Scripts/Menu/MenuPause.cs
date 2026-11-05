using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Controla cuando pones pausa o la quitas con el ESC

public class MenuPause : MonoBehaviour
{
    [SerializeField]
    InputSystemKeyboard _input;

    [SerializeField]
    ExitBook _book;

    public static bool gameIsPaused;
    public static bool pauseOpen;

    PauseController _controller;
    GameObject gameSon;

    void Start()
    {
        gameSon = gameObject.transform.GetChild(0).gameObject;

        _controller = gameSon.GetComponent<PauseController>();

        gameSon.SetActive(false);

        gameIsPaused = false;
        pauseOpen = false;
    }

    void Update()
    {
        if (_input.escape)
        {
            if (!gameIsPaused && !OpenBook.bookOpened)  
            {
                pauseOpen = true;
                OnPauseGame();                        
            }
            else if (gameIsPaused && !OpenBook.bookOpened)                                       
            {
                OnGame();                             
            }
            else if (gameIsPaused && OpenBook.bookOpened)
            {
                _book.desactiveCanvas();
            }
        }
    }

    public void OnPauseGame()           // Para meter pausa
    {
        gameSon.SetActive(true);
        gameIsPaused = true;
        _controller.OpenPause();
    }

    public void OnGame()                // Para quitar pausa
    {
        gameIsPaused = false;
        _controller.ClosePause();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("02_Loading");
    }
    public void OptionsGame()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
    public void BackToMenuGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
