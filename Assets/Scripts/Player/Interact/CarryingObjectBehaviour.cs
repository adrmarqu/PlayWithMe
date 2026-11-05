using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class CarryingObjectBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform handPos;

    bool carryingObject;
    bool objectFar;
    bool recoverPos;
    float objDistance;
    int objForce;
    bool colliding;
    float carrySpeed;
    float posX;
    float posY;
    bool iconStatus;
    float maxObjDist;

    GameObject carryedObject;
    Rigidbody _rb;
    Animator _anim;
    PlayerMovement _pM;
    InputSystemKeyboard _input;
    JumpSystem _jS;

    public static event Action Drop = delegate { };

    void OnEnable()
    {
        ActiveIcon.IconActivated += OneIconActive;
    }
    void OnDisable()
    {
        ActiveIcon.IconActivated -= OneIconActive;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pM = GetComponent<PlayerMovement>();
        _input = GetComponent<InputSystemKeyboard>();
        _anim = GetComponent<Animator>();
        _jS = GetComponent<JumpSystem>();
    }

    void Start()
    {
        carryingObject = false;
        objectFar = false;
        recoverPos = false;
        objForce = 500;
        colliding = false;
        carrySpeed = 1;
        posX = transform.position.x;
        posY = transform.position.y;
        iconStatus = false;
        maxObjDist = 0.225f;
    }

    void FixedUpdate()
    {
        if(carryedObject != null && carryedObject.name != "Hammer")
            _anim.SetBool("Carry", carryingObject);

        if (carryedObject != null && carryedObject.GetComponent<CarryObject>().ReturnCarryStatus())
        {
            objDistance = Vector3.Distance(carryedObject.transform.position, handPos.position);
            Rigidbody objRB = carryedObject.GetComponent<Rigidbody>();
            Vector3 playerForceDir = (carryedObject.transform.position - transform.position);
            Vector3 objForceDir = (transform.position - carryedObject.transform.position);
            bool objColliding = carryedObject.GetComponent<CarryObject>().ReturnColliding();
            _pM.ChangeSpeed(carrySpeed);
            _jS.ChangeJumpForce(4);

            if ((colliding || transform.position.y > -0.9f) && !objColliding)
                objRB.mass = 0.1f;
            else
                objRB.mass = _rb.mass;

            if(_input.ver < 0 && objColliding)
            {
                objRB.AddForce(objForceDir);
                _rb.AddForce(-playerForceDir * 500);
            }
            else if((_input.ver == 0 || _input.ver > 0) && objColliding)
            {
                recoverPos = false;
                objRB.drag = 10;
            }

            if(_input.ver != 0 && colliding && recoverPos)
            {
                if (_pM.ReturnCurrentSpeed() > 0)
                {
                    _pM.ChangeSpeed(0.1f);
                }
            }
            else
            {
                objRB.isKinematic = false;
                _pM.ChangeSpeed(carrySpeed);
            }

            if(objDistance > 0.15f || objDistance < -0.15f)
            {
                objectFar = true;
                objRB.AddForce(objForceDir * objForce);
            }
            else
            {
                objectFar = false;
            }

            if (objDistance > maxObjDist)
            {
                objRB.mass = _rb.mass;
                StartCoroutine(DropTransition());
            }

            if(objectFar)
            {
                playerForceDir = new Vector3(playerForceDir.x, 0, playerForceDir.z);
                objForceDir = new Vector3(objForceDir.x, 0, objForceDir.z);

                _rb.AddForce(playerForceDir * 500);
                objRB.AddForce(objForceDir * objForce);
                recoverPos = true;

                if(_pM.ReturnCurrentSpeed() > 0)
                {
                    _pM.ChangeSpeed(0.1f);
                }
            }
            else
            {
                if(objDistance < 0.15f && recoverPos)
                {
                    objDistance = Vector3.Distance(carryedObject.transform.position, handPos.position);

                    recoverPos = false;
                    objRB.drag = 10;
                }

                if(Vector3.Dot(handPos.localPosition, carryedObject.transform.localPosition) < -0.75f)
                {
                    objRB.mass = _rb.mass;
                    StartCoroutine(DropTransition());
                }

                _pM.ChangeSpeed(carrySpeed);
            }

            if(posY < transform.position.y)
            {
                objRB.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else if((posX < transform.position.x || posX > transform.position.x) && colliding)
            {
                carryedObject.transform.position = handPos.position;
            }
            else if(carryingObject)
            {
                objRB.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                posX = transform.position.x;

                if(_rb.velocity.y > 0)
                {
                    carryedObject.transform.position = handPos.position;
                }
            }

            posY = transform.position.y + 0.005f;
        }

        if(!carryingObject)
        {
            carryedObject = null;
            colliding = false;
            _jS.RecoverJumpForce();
        }
    }

    public void ObjectObtained(bool cO, GameObject carryedO)
    {
        carryingObject = cO;
        carryedObject = carryedO;
    }

    public (bool, bool) CarryingObjectStatus()
    {
        return (carryingObject, iconStatus);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != 6 && carryedObject != null && collision.gameObject.name != carryedObject.name)
        {
            colliding = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(_input.ver != 0 && colliding)
        {
            objDistance = 0;
            recoverPos = false;
            posY = transform.position.y;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer != 6 && carryedObject != null && collision.gameObject.name != carryedObject.name)
        {
            colliding = false;
        }
    }

    public void OneIconActive(bool iS)
    {
        iconStatus = iS;
    }

    public bool PlayerColliding()
    {
        return colliding;
    }

    IEnumerator DropTransition()
    {
        yield return new WaitForSeconds(0.5f);
        Drop();
    }
}