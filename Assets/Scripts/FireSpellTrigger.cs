using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FireSpellTrigger : MonoBehaviour
{
    public float yOffset = 0;
    public float xOffset = 0;
    public float zOffset = 0;

    Rigidbody2D rb;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(PlayerController.fireCasted = true)
        {
            rb.MovePosition(PlayerController.pos)
        }
        */
        if (PlayerController.fireCasted)
        {
            Vector3 fireOffset;
            fireOffset.x = xOffset;
            fireOffset.y = yOffset;
            fireOffset.z = zOffset;

            rb.MovePosition(PlayerController.playerLocation + (PlayerController.playerDirection / 2) + fireOffset);
            animator.SetBool("IsCasted", true);
        } 
        else
        {
            Vector3 homePos;
            homePos.x = -30;
            homePos.y = 0;
            homePos.z = 0;

            rb.MovePosition(homePos);
            animator.SetBool("IsCasted", false);
        }
    }
}
