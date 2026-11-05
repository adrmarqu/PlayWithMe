using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

#if __DEBUG_AVAILABLE__

using UnityEditor;

#endif

public class CinematicManager : MonoBehaviour
{
    [SerializeField]
    Transform player;

    float timeDialog;
    bool varTime;
    Vector3 playerPosition;
    bool varPosition;

    PlayerMovement playerM;
    Animator playerAnim;


    //Cinematic######################################
    public enum CinematicCommandId
    {
        enterCinematicMode,
        exitCinematicMode,
        wait,
        log,
        showDialog,
        hideDialog,
        dialog,
    };

    [System.Serializable]
    public struct CinematicCommand
    {
        public CinematicCommandId id;
        public string param1;
        public string param2;
        public string param3;
        public string param4;
    }

    [System.Serializable]
    public struct CinematicSequence
    {
        public string name;
        public CinematicCommand[] commands;
    };

    [Header("Cinematic system")]
    public CinematicSequence[] sequences;

    [Header("Dialog system")]
    //Cinematic######################################

    public Transform[] dialogCommon;
    public Transform[] dialogCharacters;
    public Transform[] dialogText;

    [System.Serializable]
    public struct DialogData
    {
        public int character;
        public string text;
    };
    public DialogData[] dialogsData;

    //Cinematic######################################
    int sequenceIndex;
    int commandIndex;
    bool waiting;
    float waitTimer;
    public bool isCinematicMode;

    bool showingDialog;
    bool hidingDialog;
    bool dialog;
    int dialogIndex;

    TextMeshPro[] dialogTextC;


    void Start()
    {
        //Cinematic######################################
        isCinematicMode = false;
        waiting = false;
        varTime = false;
        varPosition = false;

        showingDialog = false;
        hidingDialog = false;
        dialog = false;
        dialogIndex = 1;

        dialogTextC = new TextMeshPro[dialogText.Length];
        dialogTextC[0] = dialogText[0].GetComponent<TextMeshPro>();
        dialogTextC[1] = dialogText[1].GetComponent<TextMeshPro>();

        for (int i = 0; i < dialogCommon.Length; i++)
        {
            dialogCommon[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < dialogCharacters.Length; i++)
        {
            dialogCharacters[i].gameObject.SetActive(false);
        }

        playerAnim = player.GetComponent<Animator>();
        playerM = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isCinematicMode)
        {
            if (showingDialog)
            {
                playerM.ChangeSpeed(0);
                playerM.enabled = false;
                StartCoroutine(FrozenPlayerTransition());

                for (int i = 0; i < dialogCommon.Length; i++)
                {
                    dialogCommon[i].gameObject.SetActive(true);
                }
                for (int i = 0; i < dialogCharacters.Length; i++)
                {
                    dialogCharacters[i].gameObject.SetActive(false);
                }

                int character = dialogsData[dialogIndex].character;

                dialogCharacters[character].gameObject.SetActive(true);

                showingDialog = false;
                commandIndex++;

            }
            else if (dialog)
            {   
                //******************************************************
                if (!varTime)
                {
                    varTime = true;
                    timeDialog = 5;
                }
                timeDialog = timeDialog - Time.deltaTime;

                //******************************************************

                string text = dialogsData[dialogIndex].text;
                if (dialogIndex < 7)
                {
                    dialogTextC[0].text = text;
                }
                else
                {
                    dialogTextC[1].text = text;
                }
                    

                if (Input.GetKeyDown(KeyCode.Return) || timeDialog < 0)
                {
                    dialog = false;
                    varTime = false;
                }
            } 
            else if (hidingDialog)
            {
                for (int i = 0; i < dialogCommon.Length; i++)
                {
                    dialogCommon[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < dialogCharacters.Length; i++)
                {
                    dialogCharacters[i].gameObject.SetActive(false);
                }

                playerM.enabled = true;
                player.GetComponent<AudioSource>().enabled = true;
                player.GetComponent<JumpSystem>().enabled = true;
                player.GetComponent<CrawlSystem>().enabled = true;
                playerAnim.speed = 1;

                hidingDialog = false;
                commandIndex++;
            }
            else if (waiting)
            {
                if (!varPosition)
                {
                    varPosition = true;
                    playerPosition = player.localPosition;
                }
                if (waitTimer <= 0)
                {
                    waiting = false;
                    varPosition = false;
                    commandIndex++;
                }
                else
                {
                    Debug.Log(player.position.x);
                    player.localPosition = playerPosition;
                    waitTimer -= Time.deltaTime;
                }
            }


            else if (commandIndex < sequences[sequenceIndex].commands.Length)
            {
                CinematicCommand command = sequences[sequenceIndex].commands[commandIndex];

                if (command.id == CinematicCommandId.enterCinematicMode)
                {
                    isCinematicMode = true;
                }
                else if (command.id == CinematicCommandId.exitCinematicMode)
                {
                    isCinematicMode = false;
                }
                else if (command.id == CinematicCommandId.wait)
                {
                    float time = Single.Parse(command.param1);
                    waiting = true;
                    waitTimer = time;
                }
                else if (command.id == CinematicCommandId.log)
                {
                    string message = command.param1;
                    Debug.Log("Cinematic Log: " + message);
                }
                else if (command.id == CinematicCommandId.showDialog)
                {
                    int index = Int32.Parse(command.param1);

                    showingDialog = true;
                    dialogIndex = index;
                }
                else if (command.id == CinematicCommandId.dialog)
                {
                    int index = Int32.Parse(command.param1);

                    dialog = true;
                    dialogIndex = index;
                }
                else if (command.id == CinematicCommandId.hideDialog)
                {
                    hidingDialog = true;
                }
                else
                {
                    Debug.Log("Comando de cinematica no implementado");
                }

                if (!waiting && !showingDialog)
                {
                    commandIndex++;

                }
            }
            else
            {
                isCinematicMode = false;
            }
        }

    }

    public void OnTriggerCinematic(int index)
    {
        sequenceIndex = index;
        isCinematicMode = true;
        commandIndex = 0;
    }

    public bool IsCinematicMode()
    {
        return isCinematicMode;
    }

    IEnumerator FrozenPlayerTransition()
    {
        player.GetComponent<AudioSource>().enabled = false;
        player.GetComponent<JumpSystem>().enabled = false;
        player.GetComponent<CrawlSystem>().enabled = false;
        playerAnim.SetInteger("Run", 0);
        yield return new WaitForSeconds(0.5f);
        playerAnim.speed = 0;
    }
}