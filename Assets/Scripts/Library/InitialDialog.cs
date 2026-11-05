using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialDialog : MonoBehaviour
{
    GameObject panelText;

    int i;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        panelText = transform.GetChild(4).gameObject;
        i = 0;
        time = 0.5f;

        StartCoroutine(PressE());
    }

    IEnumerator PressE()
    {
        panelText.SetActive(true);
        yield return new WaitForSeconds(time);

        if (i < 2)
        {
            i++;
            time = 2;
            StartCoroutine(PressE());
        }
        else
        {
            panelText.SetActive(false);
            Destroy(GetComponent<InitialDialog>());
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            panelText.SetActive(false);
            Destroy(GetComponent<InitialDialog>());
        }
    }
}
