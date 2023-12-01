using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Enemy damage values
    public static int slimeDamage = 1;
    public static int ghostDamage = 1;

    //boss damage values
    public static int slimeBossDamage = 1;

    //public vars
    public static Vector3 playerLocation;
    public static Vector3 playerDirection;
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    //health variables
    public static int playerHealth = 10;
    public int totalPlayerHealth = 10;
    public bool invulnerable = false;
    public float invulnSec;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    //UI Objects
    public GameObject deathScreenUI;
    public GameObject hudUI;

    //private vars
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;


    //Spell casting list
    public static bool fireCasted = false;
    public static bool repairCasted = false;

    //spells discovered array
    public static bool fireEnabled = false;
    public static bool repairEnabled = false;

    //Spell selection
    public int spellSelected = 0;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(fireEnabled)
            {
                spellSelected = 1;
                Debug.Log("Fire Spell Selected");
            }
            else
            {
                Debug.Log("Invalid Spell Selected");
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(repairEnabled)
            {
                spellSelected = 2;
                Debug.Log("Repair Spell Selected");
            }
            else
            {
                Debug.Log("Invalid Spell Selected");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (spellSelected)
            {
                case 1:
                    fireCasted = true;
                    Debug.Log("Fire Casted Start");
                    StartCoroutine(fireDuration());
                    break;
                default:
                    //do nothing
                    break;
            }       
        }

    }

    IEnumerator fireDuration()
    {
        yield return new WaitForSeconds(2);
        fireCasted = false;
        Debug.Log("Fire Casted Complete");
    }

    private void FixedUpdate()
    {
        playerLocation = rb.position;

        animator.SetBool("isIdle", false);
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingUp", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingDown", false);

        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            //Debug.Log(movementInput);

            if(!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.y, 0));
                }
            }

            if(movementInput.x == 1.0)
            {
                playerDirection.x = 1;
                playerDirection.y = 0;
                //move right
                animator.SetBool("isMovingRight", true);

                animator.SetBool("isMovingLeft", false);
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingUp", false);

            }
            else if(movementInput.x == -1.0)
            {
                playerDirection.x = -1;
                playerDirection.y = 0;
                //move left
                animator.SetBool("isMovingLeft", true);

                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingUp", false);

            }
            else if (movementInput.y == 1.0)
            {
                playerDirection.y = 1;
                playerDirection.x = 0;
                //move up
                animator.SetBool("isMovingUp", true);

                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingLeft", false);
            }    
            else if (movementInput.y == -1.0)
            {
                playerDirection.y = -1;
                playerDirection.x = 0;
                //move down
                animator.SetBool("isMovingDown", true);

                animator.SetBool("isMovingUp", false);
                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingLeft", false);
            }
            
        }
        else
        {
            animator.SetBool("isIdle", true);
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

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(InputValue fireValue)
    {
        /*
        if(fireValue == '1' && fireEnabled = true)
        {

        }
        */
    }

    IEnumerator OnHit(int damage)
    {
        playerHealth -= damage;
        Debug.Log("player health: " + playerHealth);
        invulnerable = true;
        yield return new WaitForSeconds(invulnSec);
        invulnerable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!invulnerable)
        {

            if (other.gameObject.CompareTag("Slime"))
            {
                //player hit
                StartCoroutine(OnHit(slimeDamage));
            }

            if (other.gameObject.CompareTag("Ghost"))
            {
                //player hit
                StartCoroutine(OnHit(ghostDamage));
            }

            if (other.gameObject.CompareTag("slimeBoss"))
            {
                //player hit
                if(SlimeBossController.stateNum == 4)
                {
                    StartCoroutine(OnHit(slimeBossDamage * 3));
                }
                else
                {
                    StartCoroutine(OnHit(slimeBossDamage));
                }
            }
        }
    }

    private void checkHealth()
    {
        if(playerHealth > totalPlayerHealth) {
            playerHealth = totalPlayerHealth;
        }
        if(playerHealth <= 0) {
            playerHealth = 0;
            Debug.Log("Player died");
            playerDead();
        }

        for(int i = 0; i < hearts.Length; i++) {
            //set filled hearts to full, empty hearts to empty
            if(i < playerHealth) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            //enable all hearts
            if(i < totalPlayerHealth) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    private void playerDead() {
        Time.timeScale = 0f;
        hudUI.SetActive(false);
        deathScreenUI.SetActive(true);
        DeathScreen.deathScreenActive = true;
    }
}


