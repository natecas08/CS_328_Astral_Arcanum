using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum slimeBossState
{
    Idle,
    Following,
    Spawning,
    Death,
};

public class SlimeBossController : MonoBehaviour
{
    public static float health = 3;
    public float maxHealth = 3;
    public Slider healthBar;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public bool hit = false;

    public slimeBossState curState = slimeBossState.Idle;
    public Transform target;
    public float moveSpeed = 2f;
    public float targetRange = 2.5f; //distance threshold that triggers hostile mode
    public GameObject slimePrefab;

    public Sprite normalSprite;
    public Sprite squishedSprite;

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
        if(health <= 0)
        {
            death();
        }
        switch (curState)
        {
            case (slimeBossState.Idle):
                idle();
                break;
            case (slimeBossState.Following):
                following();
                break;
            case (slimeBossState.Death):
                death();
                break;
            case (slimeBossState.Spawning):
                spawning();
                break;
            default:
                idle();
                break;
        }

        if (inRange(targetRange) && curState != slimeBossState.Death)
        {
            curState = slimeBossState.Following;
            if(Random.Range(1, 400) == 1) {
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
            Vector3 direction = (target.position - transform.position).normalized;
            Vector2 moveDirection = direction;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hit = true;
            Debug.Log("Player Hit"); 
            StartCoroutine(playerBreak());
        }

        if(other.gameObject.CompareTag("Fire Spell"))
        {
            hit = true;
            healthBar.value = health/maxHealth;
            hit = false;
        }
    }

    IEnumerator playerBreak()
    {
        /* something about this is broken
        rb.velocity = new Vector2(moveSpeed*2, 0f);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2((target.position.x) + 0.5f * (-rb.velocity.x), transform.position.y), moveSpeed * 2 * Time.deltaTime);
        yield return new WaitForSeconds(5);
        */

        rb.velocity = new Vector2(0f, 0f);
        GetComponent<SpriteRenderer>().sprite = squishedSprite;
        yield return new WaitForSeconds(6);
        GetComponent<SpriteRenderer>().sprite = normalSprite;
        hit = false;
    }

    void death()
    {
        //death
        Destroy(rb.gameObject);
    }

    void spawning() {
        Vector3 bossPos = transform.position;
        float x = bossPos.x + Random.Range(-1, 1);
        float y = bossPos.y + Random.Range(-1, 1);
        Instantiate(slimePrefab, new Vector2(x, y), Quaternion.identity); 
    }
}
