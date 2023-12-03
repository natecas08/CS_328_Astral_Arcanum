using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Burnable") || other.gameObject.CompareTag("Slime") || other.gameObject.CompareTag("Ghost") || other.gameObject.CompareTag("slimeBoss")) && PlayerController.fireEnabled == true && PlayerController.fireCasted == true)
        {
            Destroy(other.gameObject);
            Debug.Log("this should only activate when the player casts the fire spell");
        }
        if (other.gameObject.CompareTag("Fixable") && PlayerController.repairEnabled == true && PlayerController.repairCasted == true)
        {
            Destroy(other.gameObject);
            Debug.Log("Fixable item has been repaired");
        }
    }
}

