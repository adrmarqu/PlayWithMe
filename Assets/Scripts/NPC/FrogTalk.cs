using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogTalk : MonoBehaviour
{
    [SerializeField]
    GameObject froggyCam;
    [SerializeField]
    Transform cinematicManager;
    [SerializeField]
    GameObject mainCam;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject music;

    InputSystemKeyboard _input;
    ActiveIcon _aI;
    CinematicManager cineManagerC;
    AudioSource froggySource;
    AudioSource musicSource;

    int index;
    
    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
        cineManagerC = cinematicManager.GetComponent<CinematicManager>();
        froggySource = GetComponent<AudioSource>();
        musicSource = music.GetComponent<AudioSource>();
    }

    void Start()
    {
        index = 2;
    }

    void LateUpdate()
    {
        if (_aI.NearObject())
        {
            _input = _aI.SetParent().transform.parent.GetComponent<InputSystemKeyboard>();

            if (_input.interact && _aI.IconEnabled())
            {
                musicSource.enabled=false;
                froggySource.enabled = true;
                _aI.DisableIcon();
                _aI.enabled = false;
                mainCam.SetActive(false);
                froggyCam.SetActive(true);
                transform.LookAt(player.transform, Vector3.up);
                Quaternion angule = transform.rotation;
                Quaternion anguleGround = Quaternion.Euler(0, angule.eulerAngles.y-180, angule.eulerAngles.z);
                transform.rotation = anguleGround;
                GetComponent<FrogTalk>().enabled = false;
                GetComponent<FrogStopTalk>().enabled = true;
                cineManagerC.OnTriggerCinematic(index);
            }

        }
    }
}
