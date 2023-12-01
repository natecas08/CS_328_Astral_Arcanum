using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject titleScreenUI;
    public GameObject hudUI;

    // Start is called before the first frame update
    void Start() {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && !TitleScreen.titleScreenActive && !LevelSelect.levelSelectActive && !DeathScreen.deathScreenActive) {
            if(isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        //hudUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        //hudUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu() {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        titleScreenUI.SetActive(true);
        TitleScreen.titleScreenActive = true;
    }

    public void ExitGame() {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
