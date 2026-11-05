using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialCinematic : MonoBehaviour
{
    public void GoToLoading()
    {
        SceneManager.LoadScene("02_Loading");
    }
}
