using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(MakeTheLoad("BaseScene"));
    }

    IEnumerator MakeTheLoad(string level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        
        while(!operation.isDone)
        {
            yield return null;
        }
    }
}
