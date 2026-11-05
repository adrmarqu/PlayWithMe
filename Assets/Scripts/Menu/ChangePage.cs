using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour
{
    [SerializeField]
    GameObject currentPage;

    [SerializeField]
    GameObject nextPage;

    public void ChangePanel()
    {
        nextPage.SetActive(true);
        currentPage.SetActive(false);
    }
}
