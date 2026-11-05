using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    AudioSource footStepsSource;
    JumpSystem _jS;
    CrawlSystem _cS;

    bool vActive;
    bool hActive;

    void Awake()
    {
        footStepsSource = GetComponent<AudioSource>();
        _cS = GetComponent<CrawlSystem>();
        _jS = GetComponent<JumpSystem>();
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetButtonDown("Horizontal") && !Input.GetButton("Jump"))
            {
                hActive = true;
                footStepsSource.Play();
            }
            if (Input.GetButtonDown("Vertical") && !Input.GetButton("Jump"))
            {
                vActive = true;
                footStepsSource.Play();
            }
            if (Input.GetButtonUp("Vertical") && !Input.GetButton("Vertical"))
            {
                vActive = false;

                if (hActive == false)
                {
                    footStepsSource.Pause();
                }
            }
            if (Input.GetButtonUp("Horizontal") && !Input.GetButton("Horizontal"))
            {
                hActive = false;

                if (vActive == false)
                {
                    footStepsSource.Pause();
                }
            }
            if (!_jS.IsTouchingGround() && Input.GetButton("Vertical") || !_jS.IsTouchingGround() && Input.GetButton("Horizontal"))
            {
                footStepsSource.Play();
            }
            if (_cS.CheckIfIsCrowded() == true)
            {
                footStepsSource.Pause();
            }
        }
        else
        {
            footStepsSource.Pause();
        }
    }
}
