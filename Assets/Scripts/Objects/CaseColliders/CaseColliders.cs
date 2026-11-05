using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseColliders : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    PushingAndPullingSystem pAP;

    void Start()
    {
        pAP = player.GetComponent<PushingAndPullingSystem>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == Constants.layerCase) 
        {
            other.gameObject.tag = "Untagged";
            pAP.CollisionExit();
        }
    }
}
