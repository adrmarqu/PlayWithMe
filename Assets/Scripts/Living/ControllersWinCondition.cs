using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersWinCondition : MonoBehaviour
{
    [SerializeField]
    AudioSource audioManager;
    [SerializeField]
    AudioClip clips;

    [SerializeField]
    GameObject gameManager;

    GameManager _gM;

    void Awake()
    {
        _gM = gameManager.GetComponent<GameManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Destructor"))
        {
            audioManager.clip = clips;
            audioManager.Play();
            _gM.TaskComplete(2);
        }
    }
}
