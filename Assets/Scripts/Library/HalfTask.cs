using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfTask : MonoBehaviour
{
    [SerializeField]
    GameObject text;

    void Start()
    {
        Desactivate();    
    }

    public void Activate()
    {
        text.SetActive(true);
    }

    public void Desactivate()
    {
        text.SetActive(false);
    }
}
