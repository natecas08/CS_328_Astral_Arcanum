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
        /*
        if (other.gameObject.CompareTag("Burnable") && PlayerController.fireEnabled == true && PlayerController.fireCasted == true)
        {
            Destroy(other.gameObject);
            //PlayerController.spellList[0] = true;
            //PlayerController.fireEnabled = true;
        }*/
    }
}

