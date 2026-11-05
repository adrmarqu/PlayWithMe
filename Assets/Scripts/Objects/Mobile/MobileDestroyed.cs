using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MobileDestroyed : MonoBehaviour
{
    [SerializeField]
    AudioSource audioManager;
    [SerializeField]
    AudioClip clips;

    [SerializeField]
    GameObject gameManager;

    GameManager _gM;
    Animator _anim;

    int numChildsPhone;

    public static event Action<GameObject> returnCupPhone = delegate { };

    void OnEnable()
    {
        DetectSystemWaterPhone.detectCupPhone += DestroyAnim;
    }
    void OnDisable()
    {
        DetectSystemWaterPhone.detectCupPhone -= DestroyAnim;
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        numChildsPhone = transform.childCount;
        _gM = gameManager.GetComponent<GameManager>();
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Destructor"))
        {
            DestroyMobile();
        }
    }

    void DestroyAnim(GameObject cup)
    {
        _anim.SetTrigger("DestroyPhone");
        StartCoroutine(FinishAnim(cup));
    }

    void DestroyMobile()
    {
        for (int i = 0; i < numChildsPhone; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
        }
        audioManager.clip = clips;
        audioManager.Play();

        Destroy(transform.GetComponent<DropObject>());
        Destroy(transform.GetComponent<ActiveIcon>());
        Destroy(transform.GetComponent<GetObject>());
        Destroy(transform.GetComponent<CarryObject>());

        _gM.TaskComplete(1);
    }

    IEnumerator FinishAnim(GameObject _cup)
    {
        yield return new WaitForSeconds(1.15f);
        DestroyMobile();
        returnCupPhone(_cup);
    }
}
