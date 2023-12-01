using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {
    public static bool levelSelectActive = false;
    public static int levelSelected = 0;
    public GameObject levelSelectUI;
    public GameObject hudUI;
    public GameObject player;

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
        hudUI.SetActive(true);
        levelSelected = 1;
        Time.timeScale = 1f;
        player.transform.position = new Vector3(0, 0, 0);
    }

    public void Level2() {
        levelSelectUI.SetActive(false);
        levelSelectActive = false;
        hudUI.SetActive(true);
        levelSelected = 2;
        Time.timeScale = 1f;
        //return when create level 2
    }

    public void Level3() {
        levelSelectUI.SetActive(false);
        levelSelectActive = false;
        hudUI.SetActive(true);
        levelSelected = 3;
        Time.timeScale = 1f;
        //return when create level 3
    }
}
