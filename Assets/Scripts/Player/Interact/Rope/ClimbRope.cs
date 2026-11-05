using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbRope : MonoBehaviour
{
    [SerializeField]
    GameObject botAglet;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject textCanvas;
    [SerializeField]
    GameObject upRope;

    InputSystemKeyboard _inputSystem;
    ActiveIcon _aI;
    Animator _animPlayer;

    float angleYMax;
    float angleYMin;
    float timerCanvas;

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
        _animPlayer = player.GetComponent<Animator>();
    }

    void Start()
    {
        angleYMax = 100;
        angleYMin = 80;
    }

    void Update()
    {
        if (textCanvas.activeInHierarchy)
        {
            timerCanvas = timerCanvas - Time.deltaTime;
            if (timerCanvas <= 0)
            {
                textCanvas.SetActive(false);
            }
        }
    }
    void LateUpdate()
    {
        if (_aI.NearObject())
        {
            _inputSystem = _aI.SetParent().transform.parent.GetComponent<InputSystemKeyboard>();

            if (_inputSystem.interact && _aI.IconEnabled())
            {
                if (player.transform.eulerAngles.y < angleYMax && player.transform.eulerAngles.y > angleYMin)
                {
                    _animPlayer.SetBool("Climb", true);
                    player.GetComponent<PlayerMovement>().enabled = false;
                    player.GetComponent<PlayerClimbing>().enabled = true;
                    player.GetComponent<RotateCamera>().enabled = false;
                    player.GetComponent<Rigidbody>().useGravity = false;
                    upRope.GetComponent<RopeMovement>().enabled = true;
                    _aI.DisableIcon();
                    _aI.enabled = false;
                    player.transform.position = new Vector3(4.0125699f, 0.705921412f, -5.15518284f);
                } 
                else
                {
                    textCanvas.SetActive(true);
                    timerCanvas = 3;
                }
            }
            
        }
        else if (!_aI.enabled && !upRope.GetComponent<RopeMovement>().enabled)
        {
            _aI.enabled = true;
        }
    }
}
