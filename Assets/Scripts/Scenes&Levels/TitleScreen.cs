using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {
    public static bool titleScreenActive = true;
    public GameObject titleScreenUI;
    public GameObject levelSelectUI;
    public GameObject hudUI;

    // Start is called before the first frame update
    void Start() {
        titleScreenUI.SetActive(true);
        hudUI.SetActive(false);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Play() {
        titleScreenUI.SetActive(false);
        titleScreenActive = false;
        levelSelectUI.SetActive(true);
        LevelSelect.levelSelectActive = true;
    }

    public void Quit() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
