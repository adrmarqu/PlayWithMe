using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCollider : MonoBehaviour
{
    int numChildsOther;
    bool used;
    int usedTimes;
    bool hasCollided;

    BoxCollider _bC;
    Rigidbody _rb;

    void Start()
    {
        used = false;
        hasCollided = false;
    }

    void OnCollisionEnter(Collision other)
    {
        _bC = other.gameObject.GetComponent<BoxCollider>();
        _rb = other.gameObject.GetComponent<Rigidbody>();

        numChildsOther = other.gameObject.transform.childCount;

        if (other.gameObject.CompareTag("DestructibleObjects") && !hasCollided)
        {

            hasCollided = true;

            for (int i = 0; i < numChildsOther; i++)
            {
                other.gameObject.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                other.gameObject.transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
            }

            Destroy(_rb);
            Destroy(_bC);

            //gameObject.SetActive(false);

            Destroy(other.gameObject.transform.GetComponent<DropObject>());
            Destroy(other.gameObject.transform.GetComponent<ActiveIcon>());
            Destroy(other.gameObject.transform.GetComponent<GetObject>());
            Destroy(other.gameObject.transform.GetComponent<CarryObject>());

            //used = true;
            usedTimes++;
            Debug.Log(usedTimes);

            if(usedTimes >= 2)
            {
                used = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        hasCollided = false;
    }
    public bool CheckIfHammerIsDestroyed()
    {
        return used;
    }

    public void ChangeUsedStatus(bool u)
    {
        used = u;
    }
}
