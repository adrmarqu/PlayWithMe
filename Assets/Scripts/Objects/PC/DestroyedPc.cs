using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedPc : MonoBehaviour
{
    [SerializeField]
    int indexDestructable;
    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    GameObject[] screen;

    GameManager _task;

    void Awake()
    {
        screen[0].SetActive(false);
        screen[1].SetActive(false);
        _task = gameManager.GetComponent<GameManager>();
    }

    void Start()
    {
        _task.TaskComplete(indexDestructable);
    }
}
