using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CrawlCameraPosition : MonoBehaviour
{
    [SerializeField]
    CrawlSystem _cS;

    public CinemachineVirtualCamera _cVC;

    Animator virtualAnim;
    
    private void Awake()
    {
        virtualAnim = _cVC.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _cS.SetCameraPosition += SetCameraCrouchPosition;
    }

    public void SetCameraCrouchPosition()
    {
        if(_cS.CheckIfIsCrowded())
        {
            virtualAnim.SetBool("isCrouched", true);
        }
        else
        {
            virtualAnim.SetBool("isCrouched", false);
        }
    }
}
