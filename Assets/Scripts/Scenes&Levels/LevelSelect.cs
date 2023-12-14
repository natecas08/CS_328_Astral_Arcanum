using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    public static bool levelSelectActive = false;
    public static int levelSelected = 0;
    public GameObject levelSelectUI;
    public GameObject hudUI;
    public GameObject player;
    public Button level2Button;
    public Button level3Button;

    // Start is called before the first frame update
    void Start() {
        levelSelectUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(PlayerController.level1Complete) {
            level2Button.interactable = true;
        }
        if(PlayerController.level2Complete) {
            level3Button.interactable = true;
        }
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
