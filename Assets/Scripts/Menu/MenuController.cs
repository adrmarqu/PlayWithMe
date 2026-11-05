using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("01_InitialCinematic");
    }

    public void OptionsGame()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void CreditsGame()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ControlsGame()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void ReturnOptionsGameToMainPanel()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ReturnControlsGameToOptionsGame()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void NextPageControls()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void ReturnToPageControls1()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
