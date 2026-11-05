using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePosition : MonoBehaviour
{
    [SerializeField]
    GameObject Rope;

    DropObject _drop;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.layerRope)
        {     
            Rope.SetActive(true);
            gameObject.SetActive(false);
            _drop = other.GetComponent<DropObject>();
            _drop.ObjectDrop();
            other.gameObject.SetActive(false);
        }
    }
}
