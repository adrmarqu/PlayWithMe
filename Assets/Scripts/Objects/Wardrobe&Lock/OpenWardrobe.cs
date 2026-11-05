using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWardrobe : MonoBehaviour
{
    [SerializeField]
    GameObject lockObj;

    Animator _anim;
    Animator lockAnim;

    private void OnEnable()
    {
        LockPuzzleBehaviour.OpenLock += EnableWardrobeAnim;
    }
    private void OnDisable()
    {
        LockPuzzleBehaviour.OpenLock -= EnableWardrobeAnim;
    }

    void Awake()
    {
        _anim = GetComponent<Animator>();
        lockAnim = lockObj.GetComponent<Animator>();
    }

    void EnableWardrobeAnim()
    {
        _anim.SetBool("Opened", true);
        lockAnim.SetBool("Unlocked", true);
    }
}
