using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbing : MonoBehaviour
{
    InputSystemKeyboard _input;
    Rigidbody _rb;
    Animator _anim;

    float speed;
    float jumpForce;
    float timer;
    float recoverTime;

    void Start()
    {
        speed = 1;
        jumpForce = 5;
        timer = 0.5f;
        recoverTime = timer;
    }
    void Awake()
    {
        _input = GetComponent<InputSystemKeyboard>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _rb.velocity = (_input.ver * transform.up * speed);

        if (_input.ver ==0 && timer <= 0)
        {
            _anim.speed = 0;
        }
        else
        {
            _anim.speed = 1;
        }
        timer = timer - Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.layerRope)
        {
            timer = recoverTime;
            _anim.SetBool("Climb", false);
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<RotateCamera>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<RopeMovement>().enabled = false;
            _anim.SetTrigger("Jump");
            _rb.velocity = transform.up * jumpForce;
            GetComponent<PlayerClimbing>().enabled = false;
        }
    }
}
