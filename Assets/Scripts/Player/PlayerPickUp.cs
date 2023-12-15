using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public AudioSource healthPickup;
    public GameObject fire_spell_bar;
    public GameObject repair_spell_bar;
    public GameObject freeze_spell_bar;
    public GameObject shield_spell_bar;
    public GameObject health_spell_bar;
    public GameObject lightning_spell_bar;

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
            fire_spell_bar.SetActive(true);
        }

        if (other.gameObject.CompareTag("Repair Spell Sheet"))
        {
            Destroy(other.gameObject);
            PlayerController.repairEnabled = true;
            repair_spell_bar.SetActive(true);
        }

        if(other.gameObject.CompareTag("Freeze Spell Sheet")) {
            Destroy(other.gameObject);
            PlayerController.freezeEnabled = true;
            freeze_spell_bar.SetActive(true);
        }

        //Powerups
        if(other.gameObject.CompareTag("Health Powerup")) {
            healthPickup.Play();
            Destroy(other.gameObject);
            PlayerController.numHealthPowerups += 1;
            PlayerController.discoveredHealthPowerup = true;
            health_spell_bar.SetActive(true);
        }
        if(other.gameObject.CompareTag("Lightning Powerup")) {
            //play lightning pickup sound
            Destroy(other.gameObject);
            PlayerController.numLightningPowerups += 1;
            PlayerController.discoveredLightningPowerup = true;
            lightning_spell_bar.SetActive (true);
        }
        if(other.gameObject.CompareTag("EndLevel1"))
        {
            Destroy(other.gameObject);
            PlayerController.level1Complete = true;
        }
        if (other.gameObject.CompareTag("EndLevel2"))
        {
            Destroy(other.gameObject);
            PlayerController.level2Complete = true;
        }
    }
}
