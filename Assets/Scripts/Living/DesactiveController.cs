using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveController : MonoBehaviour
{
    public int indexPhone;
    public int indexController;

    [SerializeField]
    GameObject gameManager;

    GameManager _task;

    bool firstTime;

    void Start()
    {
        firstTime = false;
    }

    void OnTriggerEnter(Collider other)
    {

        if (!firstTime)
        {
            //gameManager = GameObject.FindGameObjectWithTag("GameController");
            _task = gameManager.GetComponent<GameManager>();

            firstTime = true;
        }

        other.gameObject.SetActive(false);

        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Controllers"))
        {
            _task.TaskComplete(indexController);
        }

        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Phone"))
        {
            _task.TaskComplete(indexPhone);
        }
    }
}
