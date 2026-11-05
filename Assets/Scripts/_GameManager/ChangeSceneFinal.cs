using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneFinal : MonoBehaviour
{
    public void ChargeSceneCinematicLose()
    {
        if (!GameManager.winBool)
        {
            SceneManager.LoadScene("04_EndCinematicLose");

        }
        else
        {
            SceneManager.LoadScene("05_EndCinematicWin");
        }
            
    }
}
