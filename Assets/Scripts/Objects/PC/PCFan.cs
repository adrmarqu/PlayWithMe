using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCFan : MonoBehaviour
{
    [SerializeField]
    Transform[] fan;

    int vel;

    void Start()
    {
        vel = 200;
    }

    void Update()
    {
        for(int i = 0; i<fan.Length; i++)
        {
            fan[i].transform.Rotate(Vector3.up * vel * Time.deltaTime, Space.Self);
        }
        
    }
}
