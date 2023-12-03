using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSpell : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.repairCasted)
        {

            rb.MovePosition(PlayerController.playerLocation);
        }
        else
        {
            Vector3 homePos;
            homePos.x = -30;
            homePos.y = 0;
            homePos.z = 0;

            rb.MovePosition(homePos);
        }
    }

    
}
