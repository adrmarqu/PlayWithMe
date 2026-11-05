using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputSystemKeyboard _input;
    Animator _animPlayer;
    Rigidbody _rb;
    CrawlSystem _cS;

    float saveSpeed;
    float speed;

    void Awake()
    {
        _input = GetComponent<InputSystemKeyboard>();
        _animPlayer = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _cS = GetComponent<CrawlSystem>();
    }
    private void Start()
    {
        speed = 2;
        saveSpeed = speed;
    }

    void FixedUpdate()
    {
        //Movement
        if (!_cS.CheckIfIsCrowded())
        {
            _rb.velocity = (_input.ver * transform.forward * speed) + (transform.up * _rb.velocity.y) + transform.right * speed * _input.hor;
        }
        else
        {
            _rb.velocity = transform.forward * speed * _input.ver + (transform.up * _rb.velocity.y);
        }

        if(_input.run && !_cS.CheckIfIsCrowded())
        {
            speed = 3;
        }
        else if(!_input.run)
        {
            speed = saveSpeed;
        }
        //Animations

        _animPlayer.SetInteger("Run", 0);

        if (_input.ver < 0)
        {
            _animPlayer.SetInteger("Run", 2);
        }
        else if (_input.ver > 0)
        {
            _animPlayer.SetInteger("Run", 1);
        }

        if (_input.hor > 0)
        {
            _animPlayer.SetInteger("Run", 4);
        }
        else if (_input.hor < 0)
        {
            _animPlayer.SetInteger("Run", 3);
        }
    }
    
    //Unfreeze position before the hammer animation
    public void DisableFreezePosition()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _rb.freezeRotation = enabled;
    }

    public float ReturnSavedSpeed()
    {
        return saveSpeed;
    }

    public float ReturnCurrentSpeed()
    {
        return speed;
    }

    public void ChangeSpeed(float s)
    {
        speed = s;
    }
}
