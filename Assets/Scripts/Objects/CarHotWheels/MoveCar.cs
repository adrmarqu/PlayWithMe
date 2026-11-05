using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCar : MonoBehaviour
{
    Transform _marker;
    Animator _anim;
    bool onAnimCurve;
    bool onAnimNormal;
    
    void Start()
    {
        onAnimCurve = false;
        onAnimNormal = false;
    }

    void Update()
    {
        if (onAnimNormal || onAnimCurve)
        {
            transform.position = _marker.position;
            transform.rotation = _marker.rotation;
        }
        transform.Rotate(transform.rotation.x, transform.rotation.y, 0.0f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarMarkerNorm")
        {
            _marker = other.transform;
            _anim = other.GetComponent<Animator>();
            _anim.SetTrigger("AnimOn");
            onAnimNormal = true;
        }
    }
}
