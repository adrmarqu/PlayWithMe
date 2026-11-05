using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPieceBehaviour : MonoBehaviour
{
    int selected;

    void Start()
    {
        selected = 0;
    }

    void OnMouseDown()
    {
        selected = 1;
    }

    void OnMouseUp()
    {
        selected = 0;
    }

    public int ReturnSelection()
    {
        return selected;
    }

    public void ChangeSelection(int unselect)
    {
        selected = unselect;
    }
}
