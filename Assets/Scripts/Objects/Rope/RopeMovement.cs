using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMovement : MonoBehaviour
{
    Transform rope;

    int i = 0;
    float timeMove = 0.5f;

    void Start()
    {
        rope = GetComponent<Transform>();
        i = 0;
        timeMove = 0.5f;
        StartCoroutine(StartLittleMove());
    }

    IEnumerator StartLittleMove()
    {
        if (i == 0)
        {
            rope.position = new Vector3(rope.position.x, rope.position.y, rope.position.z + 0.01f);
            i++;
        }
        else
        {
            rope.position = new Vector3(rope.position.x, rope.position.y, rope.position.z - 0.01f);
            i = 0;
        }
        yield return new WaitForSeconds(timeMove);
        StartCoroutine(StartLittleMove());
    }

}
