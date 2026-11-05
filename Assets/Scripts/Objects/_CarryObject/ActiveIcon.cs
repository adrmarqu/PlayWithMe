using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveIcon : MonoBehaviour
{
    [SerializeField]
    float nearObjectDistance;
    [SerializeField]
    Material iconMat;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform icon;

    Transform parent;
    CarryingObjectBehaviour _cOB;

    bool near;

    public static event Action<bool> IconActivated = delegate { };

    void Awake()
    {
        _cOB = player.transform.GetComponent<CarryingObjectBehaviour>();
    }

    void Start()
    {
        near = false;
    }
    void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < nearObjectDistance && _cOB.CarryingObjectStatus() == (false, false) && !near)
        {
            icon.gameObject.SetActive(true);
            icon.GetComponent<MeshRenderer>().material = iconMat;
            near = true;
            parent = player.transform.GetChild(0);
            IconActivated(near);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) > nearObjectDistance && parent != null)
        {
            icon.gameObject.SetActive(false);
            near = false;
            parent = null;
            IconActivated(near);
        }

        if (!icon.gameObject.activeInHierarchy && _cOB.CarryingObjectStatus() == (false, true))
        {
            StartCoroutine(ReactiveIcon());
        }
    }

    public bool NearObject()
    {
        return near;
    }

    public Transform SetParent()
    {
        return parent;
    }

    public bool IconEnabled()
    {
        return near;
    }

    public void DisableIcon()
    {
        icon.gameObject.SetActive(false);
        near = false;
    }

    IEnumerator ReactiveIcon()
    {
        yield return new WaitForSeconds(0.25f);
        near = false;
        IconActivated(near);
    }
}
