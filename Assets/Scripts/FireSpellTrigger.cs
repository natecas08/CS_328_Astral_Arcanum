using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpellTrigger : MonoBehaviour
{
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
        if(PlayerController.fireCasted)
        {
            rb.MovePosition(PlayerController.playerLocation + PlayerController.playerDirection);
        } 
    }
}
