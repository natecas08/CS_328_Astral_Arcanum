using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false; 

    public GameObject pauseMenuUI;   
    public GameObject titleScreenUI; 

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(false);
        titleScreenUI.SetActive(true);
    }

    public void QuitGame() {
        Debug.Log("Quitting Application");
        Application.Quit();
    }
}
