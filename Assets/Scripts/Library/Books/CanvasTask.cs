using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Este script controla el estado de las tareas del canvas de tareas

public class CanvasTask : MonoBehaviour
{
    public void TaskComplete(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
}
