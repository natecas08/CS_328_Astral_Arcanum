using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBottle : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.lightningUsed)
        {
            Debug.Log("Lightning has been used");
            rb.MovePosition(PlayerController.playerLocation);
            //animator.SetBool("isUsed", true);
        }
        else
        {
            Vector3 homePos;
            homePos.x = -30;
            homePos.y = 0;
            homePos.z = 0;

            rb.MovePosition(homePos);
            //animator.SetBool("isUsed", false);
        }
    }
}
