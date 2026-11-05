using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
    GetObject _getO;
    Rigidbody _rB;
    ActiveIcon _aI;
    Vector3 recoverPos;

    bool beginToCarry;
    bool colliding;

    void Awake()
    {
        _getO = GetComponent<GetObject>();
        _rB = GetComponent<Rigidbody>();
        _aI = GetComponent<ActiveIcon>();
    }

    void Start()
    {
        beginToCarry = false;
        colliding = false;
    }

    void FixedUpdate()
    {
        if(_getO.ObjectStatus())
        {
            Transform objParent = transform.parent.parent;
            recoverPos = objParent.GetChild(1).transform.position;

            if (!beginToCarry)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                transform.position = recoverPos;
                beginToCarry = true;
                _rB.drag = 10;
                _rB.useGravity = false;
            }

            if(colliding || objParent.GetComponent<CarryingObjectBehaviour>().PlayerColliding())
            {
                _rB.velocity = new Vector3(0, 0, 0);
            }
            else
            {
                _rB.velocity = objParent.GetComponent<Rigidbody>().velocity * 1.175f;
            }
            
            StartCoroutine(DisableActiveIcon());
        }
        else
        {
            beginToCarry = false;
        }
    }

    IEnumerator DisableActiveIcon()
    {
        yield return new WaitForSeconds(0.1f);
        _aI.DisableIcon();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != 10)
        {
            colliding = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != 10)
        {
            colliding = false;
        }
    }

    public bool ReturnCarryStatus()
    {
        return beginToCarry;
    }

    public bool ReturnColliding()
    {
        return colliding;
    }
}
