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

        if(other.gameObject.CompareTag("Freeze Spell Sheet")) {
            Destroy(other.gameObject);
            PlayerController.freezeEnabled = true;
        }

        //Powerups
        if(other.gameObject.CompareTag("Health Powerup")) {
            healthPickup.Play();
            Destroy(other.gameObject);
            PlayerController.numHealthPowerups += 1;
            PlayerController.discoveredHealthPowerup = true;
        }
        if(other.gameObject.CompareTag("Lightning Powerup")) {
            //play lightning pickup sound
            Destroy(other.gameObject);
            PlayerController.numLightningPowerups += 1;
            PlayerController.discoveredLightningPowerup = true;
        }
        if(other.gameObject.CompareTag("EndLevel1"))
        {
            Destroy(other.gameObject);
            PlayerController.level1Complete = true;
        }
    }
}
