using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum slimeBossState
{
    Idle,
    Following,
    Slam,
    Spawning,
    Death,
};

public class SlimeBossController : MonoBehaviour
{
    public bool charging = false;
    public static int stateNum = 1;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public bool hit = false;

    public slimeBossState curState = slimeBossState.Idle;
    public Transform target;
    public float moveSpeed = 0.5f;
    public float targetRange = 2f; //distance threshold that triggers hostile mode
    public GameObject slimePrefab;

    Rigidbody2D rb;
    Animator animator;
    GameObject player;

    IEnumerator chargeUp()
    {
        yield return new WaitForSeconds(3);
        charging = false;
    }

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
        switch (curState)
        {
            case (slimeBossState.Idle):
                stateNum = 1;
                idle();
                break;
            case (slimeBossState.Following):
                stateNum = 2;
                following();
                break;
            case (slimeBossState.Death):
                stateNum = 3;
                death();
                break;
            case (slimeBossState.Slam):
                stateNum = 4;
                slam();
                break;
            case (slimeBossState.Spawning):
                stateNum = 5;
                spawning();
                break;
            default:
                stateNum = 1;
                idle();
                break;
        }

        if (inRange(targetRange) && curState != slimeBossState.Death)
        {
            curState = slimeBossState.Following;
            if(Random.Range(1, 300) == 1) {
                curState = slimeBossState.Spawning;
            }
        }
        else if (!inRange(targetRange) && curState != slimeBossState.Death)
        {
            curState = slimeBossState.Idle;
        }
    }

    void idle()
    {
        //do nothing
    }
    void following()
    {
        if (!hit)
        {
            if (transform.position.x > target.position.x)
            {
                //target is left
                transform.localScale = new Vector2(-1, 1);
                rb.velocity = new Vector2(-moveSpeed, 0f);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            }
            else if (transform.position.x < target.position.x)
            {
                //target is right
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(moveSpeed, 0f);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            }

            if (transform.position.y > target.position.y)
            {
                //target is above
                //transform.localScale = new Vector2(1, -1);
                rb.velocity = new Vector2(0f, -moveSpeed);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            }
            else if (transform.position.y < target.position.y)
            {
                //target is below
                //transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(0f, moveSpeed);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime);
            }
        }
    }
    void death()
    {
        //death
    }
    void slam()
    {
        rb.velocity = new Vector2(0f, 0f);
        charging = true;
        StartCoroutine(chargeUp());
        if(!charging)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * 3 * Time.deltaTime);
        }
    }

    void spawning() {
        Vector3 bossPos = transform.position;
        float x = bossPos.x + Random.Range(-1, 1);
        float y = bossPos.y + Random.Range(-1, 1);
        Instantiate(slimePrefab, new Vector2(x, y), Quaternion.identity); 
    }
}
