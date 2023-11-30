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
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    public slimeState curState = slimeState.Wandering;
    public Transform target;
    public float moveSpeed = 1f;
    public float targetRange = 2f; //distance threshold that triggers hostile mode

    Rigidbody2D rb;
    Animator animator;
    GameObject player;

    private bool inRange(float r)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= targetRange;
    }

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
                wandering();
                break;
            case (slimeState.Hostile):
                //hostile();
                break;
            case (slimeState.Death):
                //death();
                break;
            default:
                wandering();
                break;
        }

        if(inRange(targetRange) && curState != slimeState.Death)
        {
            curState = slimeState.Hostile;
        }
        else if(!inRange(targetRange) && curState != slimeState.Death)
        {
            curState = slimeState.Wandering;
        }

        void wandering()
        {
            StartCoroutine(waiting(2));
        }

        IEnumerator waiting(int sec)
        {
            float velX = Random.Range(0, 2);
            float velY = Random.Range(0, 2);
            int dir = Random.Range(0, 1);
            if (dir == 0)
            {
                dir = -1;
            }


            TryMove(new Vector2(velX * dir, velY * dir));
            yield return new WaitForSeconds(sec);

            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(sec*2);

            TryMove(new Vector2(velX * -dir, velY * -dir));
            yield return new WaitForSeconds(sec);

            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(sec*2);

        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
            return true;
        }
        else
        {
            return false;
        }
    }
}
