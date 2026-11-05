using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupPosition : MonoBehaviour
{
    [SerializeField]
    GameObject handle;
    [SerializeField]
    ActiveIcon _aI;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.layerLiquid)
        {
            handle.SetActive(false);
            _aI.enabled = false;
            Transform water = other.gameObject.transform.GetChild(0);
            water.gameObject.SetActive(true);
        }
    }
}
