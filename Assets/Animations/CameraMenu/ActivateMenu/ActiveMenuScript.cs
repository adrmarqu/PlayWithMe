using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMenuScript : MonoBehaviour
{
    public GameObject AnimationToDo;
    Animator animationBackground;

    public void Awake()
    {
        Time.timeScale = 1.0f;

        animationBackground = AnimationToDo.GetComponent<Animator>();
    }

    public void ActiveAnimationBackground()
    {
        animationBackground.SetBool("open", true);
    }
}
