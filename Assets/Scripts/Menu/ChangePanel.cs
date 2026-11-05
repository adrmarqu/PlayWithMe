using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    GameObject[] panels = new GameObject[5];
    int index;

    void Start()
    {
        index = 1;

        for (int i = 1; i < gameObject.transform.childCount; i++)
        {
            panels[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    public void NextPage()
    {  
        panels[index].SetActive(false);
        panels[index + 1].SetActive(true);

        index++;
    }

    public void PreviousPage()
    {
        panels[index].SetActive(false);
        panels[index - 1].SetActive(true);

        index--;
    }
}
