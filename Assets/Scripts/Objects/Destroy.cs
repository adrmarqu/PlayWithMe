using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField]
    AudioSource audioManager;
    [SerializeField]
    AudioClip clips;

    BoxCollider[] bC;
    Rigidbody rb;
    GetObject _gO;
    ActiveIcon _aI;
    DropObject _dO;
    CarryObject _cO;
    float distance;
    bool destroyed;

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
        _gO = GetComponent<GetObject>();
        _dO = GetComponent<DropObject>();
        _cO = GetComponent<CarryObject>();
        bC = GetComponents<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        destroyed = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            distance = Vector3.Distance(transform.position, other.gameObject.transform.position);

            if (distance > 3.5f)
            {
                int numChilds = transform.childCount;

                for (int i = 0; i < bC.Length; i++)
                {
                    Destroy(bC[i]);
                }

                Destroy(rb);

                for (int y = 0; y < numChilds; y++)
                {
                    transform.GetChild(y).gameObject.AddComponent<Rigidbody>();
                    transform.GetChild(y).gameObject.AddComponent<BoxCollider>();
                }
                Destroy(_aI);
                Destroy(_gO);
                Destroy(_dO);
                Destroy(_cO);
                destroyed = true;

                audioManager.clip = clips;
                audioManager.Play();
            }
        }
    }

    public bool ReturnDestroyStatus()
    {
        return destroyed;
    }
}
