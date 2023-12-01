using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoClipScript : MonoBehaviour
{
    private TilemapCollider2D mapCollisions;
    bool noClipToggle = false;


    // Start is called before the first frame update
    void Start()
    {
        mapCollisions = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            //noclip
            if (noClipToggle)
            {
                noClipToggle = false;
                mapCollisions.enabled = false;
                Debug.Log("No-clip enabled");
            }
            else
            {
                noClipToggle = true;
                mapCollisions.enabled = true;
                Debug.Log("No-clip disabled");
            }
        }
    }
}