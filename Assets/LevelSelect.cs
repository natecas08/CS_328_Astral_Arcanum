using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject levelSelectUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1() {
        Time.timeScale = 1f;
        levelSelectUI.SetActive(false);
        //RETURN when other levels created - need to teleport player to spawn when selected
    }

    public void Level2() {
        //RETURN when level 2 created
        //teleport player to coordinates?
    }

    public void Level3() {
        //RETURN when level 3 created
    }
}
