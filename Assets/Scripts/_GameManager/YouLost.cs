using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLost : MonoBehaviour
{
    [SerializeField]
    GameObject[] image;

    int numCinematic;
    void Awake()
    {
        numCinematic = GameManager.lostCinematic;
        if (numCinematic == 0)
        {
            image[0].SetActive(true);
        } else if (numCinematic == 1)
        {
            image[1].SetActive(true);
        } else if (numCinematic == 2)
        {
            image[2].SetActive(true);
        } else
        {
            image[3].SetActive(true);
        }
    }

    public void GoToCredtis()
    {
        SceneManager.LoadScene("Credits");
    }
}
