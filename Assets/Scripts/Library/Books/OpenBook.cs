using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Para abrir y leer los libros del mapa

public class OpenBook : MonoBehaviour
{
    public static bool bookOpened = false;

    [SerializeField]
    GameObject book;

    bool openBook;

    ActiveIcon _aI;

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
    }

    void Start()
    {
        openBook = false;
        book.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            openBook = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            openBook = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (openBook && Input.GetKeyDown(KeyCode.E) && _aI.IconEnabled() && !MenuPause.pauseOpen)
        {
            bookOpened = true;
            book.SetActive(true);
            MenuPause.gameIsPaused = true;
            MenuPause.pauseOpen = true;
            Time.timeScale = 0;
        }
    }
}
