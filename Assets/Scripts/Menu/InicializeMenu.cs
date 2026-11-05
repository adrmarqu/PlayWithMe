using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script pone a todos los objetos menos al primero en false

public class InicializeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[] components;

    void Start()
    {
        Inicialize();
    }

    public void Inicialize()
    {
        components[0].SetActive(true);

        for (int i = 1; i < components.Length; i++)
        {
            components[i].SetActive(false);
        }
    }
}
