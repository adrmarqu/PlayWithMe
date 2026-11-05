using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int lostCinematic;
    public static bool winBool;
    public Sprite complete;
    public GameObject[] canvasTasks;

    bool[] destroyedNum;
    bool[] conditions;
    int count;

    CanvasTask[] taskC;
    HalfTask _half;
    Animator _animBackground;
    GameObject canvas;

    void Start()
    {
        winBool = false;
        taskC = new CanvasTask[10];
        destroyedNum = new bool[4];
        conditions = new bool[4];
        count = 1;  // Contador de los controladores

        // Inicializar taskC

        for (int i = 0; i < canvasTasks.Length; i++)
        {
            taskC[i] = canvasTasks[i].GetComponent<CanvasTask>();
        }

        // Conseguir CanvasLibrary y inicializar _half

        canvas = canvasTasks[0].transform.parent.parent.parent.parent.gameObject;

        _half = canvas.GetComponent<HalfTask>();             // Es solo de los controles

        _animBackground = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    // Funcion para cambiar el estado de las tareas en el canvas

    public void TaskComplete(int numTask)
    {

        if (numTask == 2 && count < 2)      // Activar texto
        {
            count++;
            _half.Activate();      
        }
        else
        {
            if (numTask == 2 && count >= 2) // Desactivar texto
            {
                _half.Desactivate();      
            }

            conditions[numTask] = true;                 // Condicion para ganar hecha
            taskC[numTask].TaskComplete(complete);      // Condicion hecha en el canvas
            destroyedNum[numTask] = true;
        }

        CompleteGame();
    }


    // Funcion que comprueba si hemos acabado el juego

    public void CompleteGame()
    {
        if (conditions[0] && conditions[1] && conditions[2] && conditions[3])
        {
            WinGame();
        }
    }

    public void TimeLimit()
    {
        LostGame();
    }

    public void WinGame()
    {
        winBool = true;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        _animBackground.SetTrigger("finalGame");
    }

    public void LostGame()
    {
        if (destroyedNum[0] == false)
        {
            lostCinematic = 0;
        } else if(destroyedNum[1] == false)
        {
            lostCinematic = 1;
        } else if(destroyedNum[2] == false)
        {
            lostCinematic = 2;
        } else
        {
            lostCinematic = 3;
        }
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        _animBackground.SetTrigger("finalGame");
    }
}
