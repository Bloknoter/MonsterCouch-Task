using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation loadOperation;

    private void Update()
    {
        if(loadOperation != null)
        {
            if (loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadActualScene(sceneName));        
        //StartCoroutine(LoadActualScene(loadOperation));
    }

    private IEnumerator LoadActualScene(string sceneName)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;
        while(!loadOperation.isDone)
        {
            yield return null;
        }
    }
}
