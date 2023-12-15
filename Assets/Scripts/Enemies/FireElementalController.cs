using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum fireElemState
{
    Wandering,
    Hostile,
    Death,
};
public class FireElementalController : MonoBehaviour
{
    public bool isWaiting = false;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public bool hit = false;
    public bool frozen = false;

    public fireElemState curState = fireElemState.Wandering;
    public Transform target;
    public float moveSpeed = 1f;
    public float targetRange = 3f; //distance threshold that triggers hostile mode
    public float health = 4;

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
            case (fireElemState.Wandering):
                wandering();
                break;
            case (fireElemState.Hostile):
                hostile();
                break;
            case (fireElemState.Death):
                death();
                break;
            default:
                wandering();
                break;
        }

        if(inRange(targetRange) && curState != fireElemState.Death)
        {
            curState = fireElemState.Hostile;
        }
        else if(!inRange(targetRange) && curState != fireElemState.Death)
        {
            curState = fireElemState.Wandering;
        }
    }

    public void damage(float amount) {
        health -= amount;
    }

    public void heal(float amount) {
        health += amount;
    }

    void death() {
        Destroy(this.gameObject);
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

    IEnumerator frozenDuration() { 
        frozen = true;
        yield return new WaitForSeconds(2);
        frozen = false;
    }

    public void setFrozen() {
        StartCoroutine(frozenDuration());
    }
}