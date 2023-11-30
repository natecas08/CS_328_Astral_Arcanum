using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum slimeState
{
    Wandering,
    Hostile,
    Death,
};

public class SlimeController : MonoBehaviour
{
    public slimeState curState = slimeState.Wandering;
    public Transform target;
    public float moveSpeed = 1f;
    public float targetRange = 2f; //distance threshold that triggers hostile mode

    Rigidbody2D rb;
    Animator animator;
    GameObject player;

   /* private bool inRange(float r)
    {
        //return Vector3.Distance(transform.position, player.transform.position);
    }*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(curState)
        {
            case (slimeState.Wandering):
                //wandering();
                break;
            case (slimeState.Hostile):
                //hostile();
                break;
            case (slimeState.Death):
                //death();
                break;
            default:
                //wandering();
                break;
        }

        /*if(inRange(targetRange) && curState != slimeState.Death)
        {

        }*/
    }
}
