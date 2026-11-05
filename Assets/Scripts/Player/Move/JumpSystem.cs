using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSystem : MonoBehaviour
{
    [SerializeField]
    Transform feetPosition;
    [SerializeField]
    LayerMask floorMask;

    Animator _animPlayer;
    Rigidbody _rb;
    CrawlSystem _cS;

    float saveJumpForce;
    float jumpForce;

    void Start()
    {
        saveJumpForce = 3.5f;
        jumpForce = saveJumpForce;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animPlayer = GetComponent<Animator>();
        _cS = GetComponent<CrawlSystem>();
    }
    private void OnEnable()
    {
        GetComponent<InputSystemKeyboard>().Jump += DoJump;
    }
    private void OnDisable()
    {
        GetComponent<InputSystemKeyboard>().Jump -= DoJump;
    }

    void Update()
    {
        if(IsTouchingGround() || _cS.CheckIfIsCrowded())
        {
            _rb.mass = 5;
        }
        else
        {
            _rb.mass = 100;
        }
    }

    public void DoJump()
    {
        if(IsTouchingGround() && !_cS.CheckIfIsCrowded())
        {
            _animPlayer.SetTrigger("Jump");
            _rb.velocity = transform.up * jumpForce;
        }
    }

    public bool IsTouchingGround()
    {
        return Physics.CheckSphere(feetPosition.position, 0.15f, floorMask);
    }

    public void ChangeJumpForce(float f)
    {
        jumpForce = f;
    }

    public void RecoverJumpForce()
    {
        jumpForce = saveJumpForce;
    }
}
