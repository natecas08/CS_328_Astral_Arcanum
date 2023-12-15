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
        //damage slime
        if (other.gameObject.CompareTag("Slime") && PlayerController.fireCasted == true && PlayerController.fireEnabled == true) {
            other.gameObject.GetComponent<SlimeController>().damage(1);
            //Debug.Log("slime health: " + SlimeController.health);
        }
        if(other.gameObject.CompareTag("Slime") && PlayerController.freezeCasted == true && PlayerController.freezeEnabled == true) {
            other.gameObject.GetComponent<SlimeController>().damage(1);
            other.gameObject.GetComponent<SlimeController>().setFrozen();
        }

        //kill ghost
        if (other.gameObject.CompareTag("Ghost") && PlayerController.fireEnabled == true && PlayerController.fireCasted == true) {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Ghost") && PlayerController.freezeEnabled == true && PlayerController.freezeCasted == true) {
            Destroy(other.gameObject);
        }

        //damage skeleton
        if (other.gameObject.CompareTag("Skeleton") && PlayerController.fireCasted == true && PlayerController.fireEnabled == true) {
            other.gameObject.GetComponent<SkeletonController>().damage(1);
        }
        if(other.gameObject.CompareTag("Skeleton") && PlayerController.freezeCasted == true && PlayerController.freezeEnabled == true) {
            other.gameObject.GetComponent<SkeletonController>().damage(1);
            other.gameObject.GetComponent<SkeletonController>().setFrozen();
        }

        //damage fire elemental
        if(other.gameObject.CompareTag("Fire Elemental") && PlayerController.fireEnabled == true && PlayerController.fireCasted == true) {
            other.gameObject.GetComponent<FireElementalController>().heal(1);
        }
        if(other.gameObject.CompareTag("Fire Elemental") && PlayerController.freezeCasted == true && PlayerController.freezeEnabled == true) {
            other.gameObject.GetComponent<FireElementalController>().damage(2);
        }

        //destroy burnables
        if (other.gameObject.CompareTag("Burnable") && PlayerController.fireEnabled == true && PlayerController.fireCasted == true)
        {
            Destroy(other.gameObject);
        }
        
        //repair spell
        if (other.gameObject.CompareTag("Fixable") && PlayerController.repairEnabled == true && PlayerController.repairCasted == true)
        {
            Destroy(other.gameObject);
            Debug.Log("Fixable item has been repaired");
        }

        //damage slime boss
        if (other.gameObject.CompareTag("slimeBoss") && PlayerController.fireCasted == true && PlayerController.fireEnabled == true)
        {
            SlimeBossController.health -= 1;
        }
        if (other.gameObject.CompareTag("slimeBoss") && PlayerController.freezeCasted == true && PlayerController.freezeCasted == true)
        {
            SlimeBossController.health -= 1;
        }
        

        //lighting potion
        if ((other.gameObject.CompareTag("Slime") || other.gameObject.CompareTag("Ghost") || other.gameObject.CompareTag("Skeleton") || other.gameObject.CompareTag("Fire Elemental")) && PlayerController.lightningUsed == true)
        {
            Destroy(other.gameObject);
            Debug.Log("lightning Used");
        }
    }
}

