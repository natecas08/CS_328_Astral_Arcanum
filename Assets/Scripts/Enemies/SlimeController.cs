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
    public bool isWaiting = false;
    IEnumerator waiting(int sec)
    {
        isWaiting = true;
        float start = Time.time;
        float velX = Random.Range(0, 2);
        float velY = Random.Range(0, 2);
        int dir = Random.Range(0, 1);
        if (dir == 0)
        {
            dir = -1;
        }


        while (Time.time < (start + sec))
        {
            TryMove(new Vector2(velX * dir, velY * dir));
            yield return null;
        }

        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(sec * 2);

        start = Time.time;
        while (Time.time < (start + sec))
        {
            TryMove(new Vector2(velX * -dir, velY * -dir));
            yield return null;
        }

        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(sec * 2);
        
        isWaiting = false;
  
    }

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public bool hit = false;

    public slimeState curState = slimeState.Wandering;
    public Transform target;
    public float moveSpeed = 0.8f;
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
                hostile();
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

        
    }
    void wandering()
    {
        if (!isWaiting)
        {
            StartCoroutine(waiting(2));
        }
    }


    void hostile()
    {
        if (!hit)
        {
            if (transform.position.x > target.position.x)
            {
                //target is left
                transform.localScale = new Vector2(-1, 1);
                rb.velocity = new Vector2(-moveSpeed, 0f);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, target.position.y), moveSpeed * Time.deltaTime);
            }
            else if (transform.position.x < target.position.x)
            {
                //target is right
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(moveSpeed, 0f);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, target.position.y), moveSpeed * Time.deltaTime);
            }

            if (transform.position.y > target.position.y)
            {
                //target is left
                //transform.localScale = new Vector2(1, -1);
                rb.velocity = new Vector2(0f, -moveSpeed);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, target.position.y), moveSpeed * Time.deltaTime);
            }
            else if (transform.position.y < target.position.y)
            {
                //target is right
                //transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(0f, moveSpeed);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, target.position.y), moveSpeed * Time.deltaTime);
            }
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

    IEnumerator playerBreak()
    {
        /*
        rb.velocity = new Vector2(moveSpeed*2, 0f);
        Vector2 backOff;
        backOff.x = ((target.position.x) + 0.1f * (-rb.velocity.x));
        backOff.y = transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, backOff, moveSpeed * 2 * Time.deltaTime);
        yield return new WaitForSeconds(5);
        */

        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(2);
        hit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hit = true;
            Debug.Log("Player Hit"); 
            StartCoroutine(playerBreak());
        }
    } 
}
 