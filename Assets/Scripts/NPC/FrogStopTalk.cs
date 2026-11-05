using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogStopTalk : MonoBehaviour
{
    [SerializeField]
    GameObject froggyCam;
    [SerializeField]
    GameObject mainCam;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject music;
    [SerializeField]
    CinematicManager cineManagerC;

    ActiveIcon _aI;
    AudioSource froggySource;
    AudioSource musicSource;

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
        froggySource = GetComponent<AudioSource>();
        musicSource = music.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!cineManagerC.isCinematicMode)
        {
            _aI.enabled = true;
            mainCam.SetActive(true);
            froggyCam.SetActive(false);
            transform.position = new Vector3(4.0079999f, 3.07299995f, -7.39900017f);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            GetComponent<FrogTalk>().enabled = true;
            GetComponent<FrogStopTalk>().enabled = false;
            musicSource.enabled = true;
            froggySource.enabled = false;
        }
    }
}
