using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnableVideo : MonoBehaviour
{
    [SerializeField]
    Destroy dS;

    VideoPlayer _vP;
    ActiveIcon _aI;
    MeshRenderer _mR;
    InputSystemKeyboard _input;

    void Awake()
    {
        _vP = GetComponent<VideoPlayer>();
        _aI = GetComponent<ActiveIcon>();
        _mR = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (_aI.NearObject())
        {
            _input = _aI.SetParent().transform.parent.GetComponent<InputSystemKeyboard>();

            if (_input.interact && _aI.IconEnabled() && !_mR.enabled)
            {
                _vP.SetDirectAudioVolume(0, 1);
                _aI.DisableIcon();
                StartCoroutine(EnableMeshRenderer());
            }
            else if (_input.interact && _aI.IconEnabled() && _mR.enabled)
            {
                _mR.enabled = false;
                _vP.SetDirectAudioVolume(0, 0);
                _aI.DisableIcon();
            }
        }
        if(dS.ReturnDestroyStatus())
        {
            if(_mR.enabled)
            {
                _mR.enabled = false;
                _vP.SetDirectAudioVolume(0, 0);
            }

            _aI.DisableIcon();
            _aI.enabled = false;
            enabled = false;
        }
    }

    IEnumerator EnableMeshRenderer()
    {
        yield return new WaitForSeconds(0.1f);
        _mR.enabled = true;
    }
}
