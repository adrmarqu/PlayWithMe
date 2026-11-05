using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileDestructionDialogue : MonoBehaviour
{
    [SerializeField]
    Text dialogueTextMobile;
    [SerializeField]
    GameObject textCanvas;
    [SerializeField]
    InputSystemKeyboard _input;
    [SerializeField]
    HammerDestroyer _hD;
    [SerializeField]
    GameObject player;

    ActiveIcon _aI;
    DropObject _dO;
    CarryObject _cO;
    GetObject _gO;

    float distancePlayerPhone;
    bool opened;

    void Awake()
    {
        _aI = GetComponent<ActiveIcon>();
        _dO = GetComponent<DropObject>();
        _cO = GetComponent<CarryObject>();
        _gO = GetComponent<GetObject>();
    }

    void Start()
    {
        opened = false;
    }

    void Update()
    {
        distancePlayerPhone = Vector3.Distance(player.transform.position, transform.position);

        if(_aI.NearObject() && _input.interact && !opened && !_hD.ReturnTakenStatus())
        {
            textCanvas.SetActive(true);
            dialogueTextMobile.text = "Mmmhh... I'll see what to do with this.";
            opened = true;
        }
        else if(_aI.NearObject() && _input.interact && opened && !_hD.ReturnTakenStatus() || !_aI.NearObject() && opened && !_hD.ReturnTakenStatus() || _hD.ReturnTakenStatus() && distancePlayerPhone > 0.5f)
        {
            textCanvas.SetActive(false);
            Destroy(GetComponent<MobileDestructionDialogue>());
            _dO.enabled = true;
            _cO.enabled = true;
            _gO.enabled = true;
            opened = false;
        }
    }
}