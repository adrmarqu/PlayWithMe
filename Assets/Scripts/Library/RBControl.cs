using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBControl : MonoBehaviour
{
    [SerializeField]
    GameObject books;

    public void AddRigidBody()
    {
        for (int i = 0; i < books.transform.childCount; i++)
        {
            books.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
        }
        StartCoroutine(TimeRB());
    }

    public void RemoveRigidBody()
    {
        for (int i = 0; i < books.transform.childCount; i++)
        {
            Destroy(books.transform.GetChild(i).gameObject.GetComponent<Rigidbody>());
        }
    }

    IEnumerator TimeRB()
    {
        yield return new WaitForSeconds(3);
        RemoveRigidBody();
    }
}
