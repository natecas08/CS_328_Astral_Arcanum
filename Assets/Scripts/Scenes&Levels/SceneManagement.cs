using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    int secondsToWait = 5;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //StartCoroutine(logoWait());

        //if (currentSceneIndex < SceneManager.sceneCount - 1)
        //{
          //  SceneManager.LoadScene(currentSceneIndex + 1);
        //}

        if(Time.time - startTime >= secondsToWait)
        {
            SceneManager.LoadScene(1);

        }
    }

    IEnumerator logoWait()
    {
        yield return new WaitForSeconds(5);
    }
}




