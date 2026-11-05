using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingAndPullingSystem : MonoBehaviour
{
    PlayerMovement _pM;
    JumpSystem _jS;
    Animator _anim;
    CapsuleCollider _col;
    InputSystemKeyboard _input;
    Rigidbody _rB;
    RotateCamera _rotateC;
    CrawlSystem _cS;

    bool onPush;
    bool onPull;
    float originalRad;
    float onPushRad;
    bool transition;

    Rigidbody objRB;
    Collision colObj;

    void OnEnable()
    {
        GetComponent<InputSystemKeyboard>().Crawl += SetBoolFalse;
        LockPuzzleBehaviour.OpenCanvas += UnfreezePosition;
    }

    void OnDisable()
    {
        GetComponent<InputSystemKeyboard>().Crawl -= SetBoolFalse;
        LockPuzzleBehaviour.OpenCanvas -= UnfreezePosition;
    }

    void Awake()
    {
        _pM = GetComponent<PlayerMovement>();
        _jS = GetComponent<JumpSystem>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<CapsuleCollider>();
        _input = GetComponent<InputSystemKeyboard>();
        _rB = GetComponent<Rigidbody>();
        _rotateC = GetComponent<RotateCamera>();
        _cS = GetComponent<CrawlSystem>();
    }
    void Start()
    {
        onPush = false;
        onPull = false;
        originalRad = _col.radius;
        onPushRad = 1.20f;
        transition = false;
    }

    void FixedUpdate()
    {
        _anim.SetBool("Push", onPush);

        _anim.SetBool("Pull", onPull);

        if((onPush || onPull))
        {
            if (_input.ver == 0 && _input.hor == 0)
            {
                _anim.speed = 0;
            }
            else
            {
                _anim.speed = 1;
            }

            Vector3 hitPoint = colObj.GetContact(0).normal;
            float vectorDot = Vector3.Dot(hitPoint, Vector3.forward);

            if (Mathf.Approximately(vectorDot, 1) || Mathf.Approximately(vectorDot, -1))
                _rB.velocity = new Vector3(0, 0, _rB.velocity.z);
            else if(Mathf.Approximately(vectorDot, 0))
                _rB.velocity = new Vector3(_rB.velocity.x, 0, 0);
        }
        if(onPull)
        {
            if (_input.ver > 0 || _input.hor != 0)
            {
                _pM.ChangeSpeed(0);
                _anim.speed = 0;
            }
            else
            {
                _pM.ChangeSpeed((objRB.drag - objRB.mass) + 0.2f);

                if (_pM.ReturnCurrentSpeed() < 0)
                {
                    _pM.ChangeSpeed(0.1f);
                    _anim.speed = 1;
                }

                objRB.velocity = _rB.velocity * 2f;
            }  
        }
        else if(onPush)
        {
            if (_input.ver < 0 || _input.hor != 0)
            {
                _pM.ChangeSpeed(0);
                _anim.speed = 0;
            }
            else
            {
                _pM.ChangeSpeed((objRB.drag - objRB.mass) + 1);

                if (_pM.ReturnCurrentSpeed() < 0)
                {
                    _pM.ChangeSpeed(0.5f);
                    _anim.speed = 1;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        RaycastHit hit;

        if (collision.gameObject.tag == "Pusheable" && !_cS.CheckIfIsCrowded() && collision.gameObject.tag != "Wardrobe")
        {
            colObj = collision;

            if (Physics.Raycast(transform.GetChild(3).position, transform.TransformDirection(Vector3.forward), out hit, 2) && hit.transform == colObj.transform && !onPull && !transition && _jS.IsTouchingGround())
            {
                objRB = colObj.transform.GetComponent<Rigidbody>();
                objRB.constraints = RigidbodyConstraints.FreezeRotation;
                _rotateC.enabled = false;
                _cS.enabled = false;
                _jS.enabled = false;
                _col.radius = onPushRad;
                onPush = true;
            }

            if(onPush && _input.pull)
            {
                onPush = false;
                onPull = true;
            }
            else if(onPull && _input.interact)
            {
                onPull = false;
                onPush = true;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        RaycastHit hit;

        if (collision.gameObject.tag == "Pusheable" && !_cS.CheckIfIsCrowded())
        {
            colObj = collision;

            if (Physics.Raycast(transform.GetChild(3).position, transform.TransformDirection(Vector3.forward), out hit, 2) && hit.transform == colObj.transform && !onPull && !transition)
            {
                objRB = colObj.transform.GetComponent<Rigidbody>();
                objRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                _rotateC.enabled = false;
                _cS.enabled = false;
                _jS.enabled = false;
                _col.radius = onPushRad;
                onPush = true;
            }

            if (onPush && _input.pull)
            {
                onPush = false;
                onPull = true;
            }
            else if (onPull && _input.interact)
            {
                onPull = false;
                onPush = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Pusheable")
        {
            objRB = collision.transform.GetComponent<Rigidbody>();
            CollisionExit();
        }
    }

    void SetBoolFalse()
    {
        StartCoroutine(BoolFalseTime());
    }

    IEnumerator BoolFalseTime()
    {
        onPush = false;
        onPull = false;
        _anim.speed = 1;
        transition = true;
        yield return new WaitForSeconds(0.5f);
        transition = false;
    }

    void UnfreezePosition()
    {
        objRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void CollisionExit()
    {
        objRB.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        _rotateC.enabled = true;
        _cS.enabled = true;
        _jS.enabled = true;
        _anim.speed = 1;
        _col.radius = originalRad;
        _pM.ChangeSpeed(_pM.ReturnSavedSpeed());

        if (onPush)
            onPush = false;
        else if (onPull)
            onPull = false;
    }
}
