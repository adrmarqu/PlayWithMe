using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exclamationAppear : MonoBehaviour
{
    Animator _anim;

    float timer;
    int numchilds;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        numchilds = transform.childCount;
        timer = 5;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(timer <= 0)
        {
            for (int i = 0; i < numchilds; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            _anim.SetBool("exOpened", false);

            timer = 0;
        }
    }
    public void AppearExclamation()
    {
        timer = 5;

        if (timer > 0)
        {
            for (int i = 0; i < numchilds; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }

            _anim.SetBool("exOpened", true);
        }
    }
}
