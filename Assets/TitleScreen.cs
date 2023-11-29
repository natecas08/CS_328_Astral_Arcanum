using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject titleScreenUI; 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        Time.timeScale = 1f;
        titleScreenUI.SetActive(false);
    }

    public void Exit() {
        Debug.Log("Exiting Application");
        Application.Quit();
    }
}
