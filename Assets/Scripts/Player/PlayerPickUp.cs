using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public AudioSource healthPickup;

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
        //Spells
        if(other.gameObject.CompareTag("Fire Spell Sheet"))
        {
            Destroy(other.gameObject);
            PlayerController.fireEnabled = true;
        }

        if (other.gameObject.CompareTag("Repair Spell Sheet"))
        {
            Destroy(other.gameObject);
            PlayerController.repairEnabled = true;
        }

        //Powerups
        if(other.gameObject.CompareTag("Health Powerup")) {
            healthPickup.Play();
            Destroy(other.gameObject);
            PlayerController.playerHealth += 3;
        }
    }
}
