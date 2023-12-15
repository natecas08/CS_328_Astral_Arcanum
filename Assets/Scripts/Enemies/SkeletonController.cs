using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum skeletonState
{
    Wandering,
    Hostile,
    Death,
};
public class SkeletonController : MonoBehaviour
{
    public bool isWaiting = false;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public bool hit = false;
    public bool frozen = false;

    public skeletonState curState = skeletonState.Wandering;
    public Transform target;
    public float moveSpeed = 1.1f;
    public float targetRange = 4f; //distance threshold that triggers hostile mode
    public float health = 3;
    public bool hasEmerged = false;

    Rigidbody2D rb;
    Animator animator;
    GameObject player;

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

    private bool inRange(float r)
    {
        return Vector3.Distance(transform.position, target.position) <= targetRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            death();
        }

        switch(curState)
        {
            case (skeletonState.Wandering):
                wandering();
                break;
            case (skeletonState.Hostile):
                hostile();
                break;
            case (skeletonState.Death):
                death();
                break;
            default:
                wandering();
                break;
        }

        if(inRange(targetRange) && curState != skeletonState.Death)
        {
            if(!hasEmerged) {
                StartCoroutine(emerge());
            }
            curState = skeletonState.Hostile;
        }
        else if(!inRange(targetRange) && curState != skeletonState.Death)
        {
            curState = skeletonState.Wandering;
        }
    }

    IEnumerator emerge() {
        animator.SetTrigger("emerge");
        yield return new WaitForSeconds(0.8f);
        hasEmerged = true;
    }

    public void damage(float amount) {
        health -= amount;
    }

    void death() {
        Destroy(this.gameObject);
    }

    void wandering()
    {
        if (!isWaiting && hasEmerged)
        {
            StartCoroutine(waiting(2));
        }
    }

    void hostile()
    {
        if (!hit && hasEmerged)
        {
            animator.SetBool("isMoving", true);
            Vector3 direction = (target.position - transform.position).normalized;
            Vector2 moveDirection = direction;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
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
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(1.5f);
        hit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hit = true;
            animator.SetTrigger("attack");
            StartCoroutine(playerBreak());
        }
    } 

    IEnumerator frozenDuration() { 
        frozen = true;
        yield return new WaitForSeconds(1.5f);
        frozen = false;
    }

    public void setFrozen() {
        StartCoroutine(frozenDuration());
    }
}
