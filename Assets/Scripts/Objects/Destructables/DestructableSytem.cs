using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestructableSytem : MonoBehaviour
{
    [SerializeField]
    GameObject destroyedObj;

    Animator _anim;

    public static event Action<GameObject> returnCupPC = delegate { };

    void OnEnable()
    {
        DetectSystemWaterPC.detectCupPC += DestroyAnim;
    }
    void OnDisable()
    {
        DetectSystemWaterPC.detectCupPC -= DestroyAnim;
    }

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == Constants.layerDestructor)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        destroyedObj.SetActive(true);
    }

    public void DestroyAnim(GameObject k)
    {
        _anim.SetTrigger("DestroyPC");
        StartCoroutine(FinishAnim(k));
    }
    IEnumerator FinishAnim(GameObject _cup)
    {
        yield return new WaitForSeconds(1.15f);
        returnCupPC(_cup);
        Destroy();
    }
}