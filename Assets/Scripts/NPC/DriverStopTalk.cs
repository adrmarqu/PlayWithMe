using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverStopTalk : MonoBehaviour
{
    [SerializeField]
    GameObject alonsoCam;
    [SerializeField]
    GameObject mainCam;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject music;
    [SerializeField]
    GameObject missionParts;
    [SerializeField]
    CinematicManager cineManagerC;

    ActiveIcon _aI;
    Animator _anim;
    DriverTalk _talk;
    AudioSource alonsoSource;
    AudioSource musicSource;

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
        _anim = GetComponent<Animator>();
        _talk = GetComponent<DriverTalk>();
        alonsoSource = GetComponent<AudioSource>();
        musicSource = music.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!cineManagerC.isCinematicMode)
        {
            _anim.SetBool("Talking", false);
            _aI.enabled = true;
            mainCam.SetActive(true);
            alonsoCam.SetActive(false);
            transform.position = new Vector3(-2.68890882f, -0.822300017f, 0.420034885f);
            transform.rotation = new Quaternion(0, 0.218167916f, 0, 0.975911319f);
            GetComponent<DriverTalk>().enabled = true;
            if (_talk.ReturnIndex() == 1)
            {
                missionParts.SetActive(true);
                gameObject.SetActive(false);
            }
            GetComponent<DriverStopTalk>().enabled = false;
            musicSource.enabled = true;
            alonsoSource.enabled = false;
        }
    }
}
