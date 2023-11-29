using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject titleScreenUI; 
    public GameObject levelSelectUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        titleScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        titleScreenUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }

    public void Exit() {
        Debug.Log("Exiting Application");
        Application.Quit();
    }
}
