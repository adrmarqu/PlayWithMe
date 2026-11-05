using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class LockPuzzleBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform lockCam;
    [SerializeField]
    GameObject exitCanvas;
    [SerializeField]
    GameObject textCanvas;
    [SerializeField]
    TextMeshProUGUI dialogText;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject mainCam;
    [SerializeField]
    GameObject virtualCam;

    Transform[] letters;
    Animator _anim;
    Animator playerAnim;
    Rigidbody _rB;
    
    bool puzzleSolved;
    float rotateAmount;
    bool[] lockedPiece;
    float[] correctPos;
    float transitionSpeed;
    bool canvasOpen;
    float transitionTime;

    ActiveIcon _aI;
    LockPieceBehaviour lPB;
    PlayerMovement playerM;
    InputSystemKeyboard _input;
    CrawlSystem _cS;
    JumpSystem _jS;

    public static event Action OpenLock = delegate { };
    public static event Action OpenCanvas = delegate { };

    void Awake()
    {
        _input = player.GetComponent<InputSystemKeyboard>();
        playerM = player.GetComponent<PlayerMovement>();
        _cS = player.GetComponent<CrawlSystem>();
        _jS = player.GetComponent<JumpSystem>();
        _aI = GetComponent<ActiveIcon>();
        _anim = GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
        _rB = player.GetComponent<Rigidbody>();
    }

    void Start()
    {
        letters = new Transform[5];
        puzzleSolved = false;
        rotateAmount = 36;
        lockedPiece = new bool[5];
        correctPos = new float[] { 0, 72, 252, 0, 0 };
        transitionSpeed = 5;
        canvasOpen = false;
        transitionTime = 0.15f;

        for (int i = 0; i < lockedPiece.Length; i++)
        {
            lockedPiece[i] = true;
        }
    }

    void LateUpdate()
    {
        if(puzzleSolved)
        {
            _cS.enabled = true;
            player.GetComponent<AudioSource>().enabled = true;
            playerM.enabled = true;
            _jS.enabled = true;
            _cS.enabled = true;
            OpenLock();
            GC.Collect();
            this.enabled = false;
        }
        else if(_aI.NearObject())
        {
            if (_input.interact && _aI.IconEnabled() && !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                StartCoroutine(Dialog(playerAnim));
            }
            else if (_input.interact && _aI.IconEnabled() && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                MenuPause.gameIsPaused = true;
                virtualCam.SetActive(false);
                exitCanvas.SetActive(true);
                mainCam.transform.parent = null;
                canvasOpen = true;
                _rB.constraints = RigidbodyConstraints.FreezeAll;
                OpenCanvas();
            }
        }

        if(canvasOpen)
        {
            _cS.enabled = false;
            player.GetComponent<AudioSource>().enabled = false;
            playerM.enabled = false;
            _jS.enabled = false;
            _cS.enabled = false;
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, lockCam.position, Time.deltaTime);
            mainCam.transform.eulerAngles = Vector3.Lerp(mainCam.transform.eulerAngles, lockCam.eulerAngles, Time.deltaTime);

            if (Vector3.Distance(mainCam.transform.position, lockCam.position) < transitionTime)
                player.transform.GetChild(4).gameObject.SetActive(false);

            PlayPuzzle();
        }
        else
        {
            if(mainCam != null)
            {
                mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, virtualCam.transform.position, Time.deltaTime * transitionSpeed);
                mainCam.transform.eulerAngles = Vector3.Lerp(mainCam.transform.eulerAngles, virtualCam.transform.eulerAngles, Time.deltaTime * transitionSpeed);

                if (Vector3.Distance(mainCam.transform.position, virtualCam.transform.position) < transitionTime)
                {
                    virtualCam.SetActive(true);
                }
            } 
        }
    }

    void PlayPuzzle()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i] = transform.GetChild(i);

            lPB = letters[i].GetComponent<LockPieceBehaviour>();

            if (lPB.ReturnSelection() > 0)
            {
                Vector3 letterRot = new Vector3(0, 0, +rotateAmount);

                letters[i].Rotate(letterRot);
                lPB.ChangeSelection(0);

                if(letters[i].localEulerAngles.z < 0)
                {
                    letters[i].rotation = new Quaternion(0, 0, 0, 0);
                }
            }

            if(letters[i].localEulerAngles.z == correctPos[i])
            {
                lockedPiece[i] = false;
            }
        }

        if(!lockedPiece[0] && !lockedPiece[1] && !lockedPiece[2] && !lockedPiece[3] && !lockedPiece[4])
        {
            _anim.SetBool("Solved", true);

            if(_anim.GetCurrentAnimatorStateInfo(0).IsName("Unlocked"))
            {
                puzzleSolved = true;
                virtualCam.SetActive(true);
                exitCanvas.SetActive(false);
                player.transform.GetChild(4).gameObject.SetActive(true);
                mainCam.transform.parent = player.transform;
                playerAnim.speed = 1;
                _rB.constraints = RigidbodyConstraints.FreezeRotation;
                _aI.DisableIcon();
                _aI.enabled = false;
                canvasOpen = false;
                MenuPause.gameIsPaused = false;
            }
        }
    }

    IEnumerator Dialog(Animator pA)
    {
        textCanvas.SetActive(true);

        if(player.activeInHierarchy)
        {
            if (pA.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                dialogText.text = "I'll need to put myself on top of something in order for moving those gears.";
                yield return new WaitForSeconds(5);
            }
            else if (!pA.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                dialogText.text = "I can't move correctly those gears if I'm moving...";
                yield return new WaitForSeconds(5);
            }
        }

        textCanvas.SetActive(false);
    }

    public void CloseCanvas()
    {
        playerM.enabled = true;
        _cS.enabled = true;
        _jS.enabled = true;
        canvasOpen = false;
        mainCam.transform.parent = player.transform;
        exitCanvas.SetActive(false);
        player.transform.GetChild(4).gameObject.SetActive(true);
        playerAnim.speed = 1;
        _rB.constraints = RigidbodyConstraints.FreezeRotation;
        MenuPause.gameIsPaused = false;
    }
}