using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverTalk : MonoBehaviour
{
    [SerializeField]
    GameObject alonsoCam;
    [SerializeField]
    GameObject canvasMission;
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
    Animator _anim;
    CollectUpdateCanvas _collect;
    CinematicManager cineManagerC;
    AudioSource alonsoSource;
    AudioSource musicSource;

    int index;

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
        _anim = GetComponent<Animator>();
        _collect = GetComponent<CollectUpdateCanvas>();
        cineManagerC = cinematicManager.GetComponent<CinematicManager>();
        alonsoSource = GetComponent<AudioSource>();
        musicSource = music.GetComponent<AudioSource>();
    }

    void Start()
    {
        index = 0;
    }

    void Update()
    {
        if(_collect.ReturnCar()>0 && _collect.ReturnPieces() == 10)
        {
            index = 1;
        }
    }
    void LateUpdate()
    {
        if (_aI.NearObject())
        {
            _input = _aI.SetParent().transform.parent.GetComponent<InputSystemKeyboard>();

            if (_input.interact && _aI.IconEnabled())
            {
                musicSource.enabled = false;
                alonsoSource.enabled = true;
                _anim.SetBool("Talking", true);
                _aI.DisableIcon();
                _aI.enabled = false;
                mainCam.SetActive(false);
                alonsoCam.SetActive(true);
                canvasMission.SetActive(true);
                transform.LookAt(player.transform, Vector3.up);
                Quaternion angule = transform.rotation;
                Quaternion anguleGround = Quaternion.Euler(0, angule.eulerAngles.y, angule.eulerAngles.z);
                transform.rotation = anguleGround;
                GetComponent<DriverTalk>().enabled = false;
                GetComponent<DriverStopTalk>().enabled = true;
                cineManagerC.OnTriggerCinematic(index);
            }

        }
    }

    public int ReturnIndex()
    {
        return index;
    }
}
