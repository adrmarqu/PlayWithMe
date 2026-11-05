using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTriggersSystem : MonoBehaviour
{
    [SerializeField]
    GameObject nextText;
    [SerializeField]
    GameObject[] canvasObj;
    [SerializeField]
    GameObject[] canvasText;
    [SerializeField]
    GameObject player;

    InputSystemKeyboard _input;
    PlayerMovement _playerM;
    JumpSystem _playerJ;
    CarryingObjectBehaviour _playerC;
    Animator _playerAnim;
    RotateCamera _rC;

    int id;
    string triggerT;
    bool canvasS;
    bool canvasT;
    int totalT;

    int recordText;
    bool passingText;

    public TutorialTriggersSystem(int triggerId, string triggerType, bool canvasStatus, bool canvasTriggered, int totalTexts)
    {
        id = triggerId;
        triggerT = triggerType;
        canvasS = canvasStatus;
        canvasT = canvasTriggered;
        totalT = totalTexts;
    }

    List<TutorialTriggersSystem> triggersSystem;

    void OnEnable()
    {
        TutorialTriggersCheck.TriggerReached += SetCanvas;
    }
    void OnDisable()
    {
        TutorialTriggersCheck.TriggerReached -= SetCanvas;
    }

    void Awake()
    {
        _input = player.GetComponent<InputSystemKeyboard>();
        _playerM = player.GetComponent<PlayerMovement>();
        _playerJ = player.GetComponent<JumpSystem>();
        _playerC = player.GetComponent<CarryingObjectBehaviour>();
        _playerAnim = player.GetComponent<Animator>();
        _rC = player.GetComponent<RotateCamera>();
    }
    void Start()
    {
        triggersSystem = new List<TutorialTriggersSystem>();

        triggersSystem.Add(new TutorialTriggersSystem(0, "Pusheable", false, false, 5));
        triggersSystem.Add(new TutorialTriggersSystem(1, "Carryable", false, false, 0));

        recordText = 0;
        passingText = false;
    }

    void FixedUpdate()
    {
        foreach(TutorialTriggersSystem triggerCanvas in triggersSystem)
        {
            if(triggerCanvas.canvasS && !triggerCanvas.canvasT)
            {
                if(_playerC.CarryingObjectStatus() == (false, false))
                {
                    _playerC.OneIconActive(true);
                }
                    
                if (recordText == 0)
                {
                    StartCoroutine(FrozenPlayerTransition());
                }
                
                canvasObj[triggerCanvas.id].SetActive(true);
                canvasText[recordText].SetActive(true);
                nextText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && !passingText)
                {
                    passingText = true;
                    StartCoroutine(NextTextTransition());
                }

                if (recordText == triggerCanvas.totalT)
                {
                    _playerM.enabled = true;
                    _playerJ.enabled = true;
                    _input.enabled = true;

                    if(!_playerC.enabled)
                    {
                        _playerC.enabled = true;
                        _playerC.ObjectObtained(false, null);
                    }

                    _playerAnim.speed = 1;
                    _rC.ChangeSpeed(_rC.ReturnSpeed());
                    nextText.SetActive(false);
                    canvasObj[triggerCanvas.id].SetActive(false);
                    triggerCanvas.canvasT = true;
                }
            }
        }
    }
    void SetCanvas(string triggerType)
    {
        foreach(TutorialTriggersSystem triggerCanvas in triggersSystem)
        {
            if(triggerCanvas.triggerT == triggerType && !triggerCanvas.canvasS)
            {
                triggerCanvas.canvasS = true;
            }
        }
    }

    IEnumerator FrozenPlayerTransition()
    {
        _playerM.enabled = false;
        _playerJ.enabled = false;
        _input.enabled = false;

        if (_playerC.CarryingObjectStatus() == (false, false))
        {
            _playerC.OneIconActive(false);
            _playerC.enabled = false;
        }

        _playerAnim.SetInteger("Run", 0);
        yield return new WaitForSeconds(0.5f);
        _playerAnim.speed = 0;
        _rC.ChangeSpeed(0);
    }

    IEnumerator NextTextTransition()
    {
        canvasText[recordText].SetActive(false);
        recordText++;
        yield return new WaitForSeconds(0.25f);
        passingText = false;
    }
}
