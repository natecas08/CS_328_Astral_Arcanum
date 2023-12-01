using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour {
    public static bool deathScreenActive = false;
    public GameObject deathScreenUI;
    public GameObject levelSelectUI;

    // Start is called before the first frame update
    void Start() {
        deathScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Continue() {
        deathScreenUI.SetActive(false);
        deathScreenActive = false;
        levelSelectUI.SetActive(true);
        LevelSelect.levelSelectActive = true;
    }

    public void Quit() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
