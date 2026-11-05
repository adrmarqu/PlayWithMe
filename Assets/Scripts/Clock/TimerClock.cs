using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TimerClock : MonoBehaviour
{
    [SerializeField]
    exclamationAppear _eA;
    [SerializeField]
    GameObject radio;
    [SerializeField]
    GameObject music;
    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    InputSystemKeyboard _input;

    float timer = 0.0f; //1190.0f para avanzar el juego al final
    int minutes;
    int seconds;
    Text timerText;
    int[] minutesToClaim = { 05 , 10 , 15 };
    float[] valuePitch = {1.25f, 1.5f, 1.75f};

    AudioSource radioSource;
    AudioSource musicSource;
    GameManager _gM;
    private void Awake()
    {
        timerText = GetComponent<Text>();
        radioSource = radio.GetComponent<AudioSource>();
        musicSource = music.GetComponent<AudioSource>();

        _gM = gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (_input.endGame)
        {
            timer = 1190.0f;
        }

        timer += Time.deltaTime;
        //Función de mostrar el tiempo
        DisplayTime();
    }

    void DisplayTime()
    {
        minutes = Mathf.FloorToInt(timer / 60.0f);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //Función que muestra la exclamación cada 5 minutos
        for(int i = 0; i < minutesToClaim.Length; i++)
        {
            if (minutes == minutesToClaim[i])
            {
                if (seconds < 8)
                {
                    radioSource.enabled = true;
                    _eA.AppearExclamation();
                    musicSource.pitch = valuePitch[i];
                }
                else
                {
                    radioSource.enabled = false;
                }
            }
        }

        if (minutes >= 20)
        {
            _gM.TimeLimit();
            enabled = false;
        }
    }
}
