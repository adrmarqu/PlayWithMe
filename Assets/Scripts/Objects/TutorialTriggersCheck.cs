using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialTriggersCheck : MonoBehaviour
{
    bool triggered;

    public static event Action<string> TriggerReached = delegate { };
    void Start()
    {
        triggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            TriggerReached(transform.gameObject.tag);
        }
    }
}
