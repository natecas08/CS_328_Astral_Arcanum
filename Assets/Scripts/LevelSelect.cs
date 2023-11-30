using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {
    public static bool levelSelectActive = false;
    public GameObject levelSelectUI;
    // Start is called before the first frame update
    void Start() {
        levelSelectUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Level1() {
        levelSelectUI.SetActive(false);
        levelSelectActive = false;
        Time.timeScale = 1f;
        //need to teleport player to starting coordinates
    }

    public void Level2() {
        levelSelectUI.SetActive(false);
        levelSelectActive = false;
        Time.timeScale = 1f;
        //return when create level 2
    }

    public void Level3() {
        levelSelectUI.SetActive(false);
        levelSelectActive = false;
        Time.timeScale = 1f;
        //return when create level 3
    }
}
