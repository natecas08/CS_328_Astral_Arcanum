using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //spells discovered array
    public static bool fireEnabled = false;

    public static Vector3 playerLocation;


    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    //Spell casting list
    public static bool fireCasted = false;

    //Spell selection
    public int spellSelected = 0;


    Vector2 movementInput;
    Rigidbody2D rb;

    Animator animator;

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
        if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            spellSelected = 1;
            Debug.Log("Fire Spell Selected");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (spellSelected)
            {
                case 1:
                    fireCasted = true;
                    InstantiatePrefab();
                    Debug.Log("Fire Casted Start");
                    StartCoroutine(fireDuration());
                    break;
                default:
                    break;
            }       
        }
    }

    private void InstantiatePrefab()
    {
        //GameObject instance = InstantiatePrefab<GameObject>(FireSpell);
        //instance.transform.position = rb.position;
    }

    IEnumerator fireDuration()
    {
        yield return new WaitForSeconds(2);
        fireCasted = false;
        Debug.Log("Fire Casted Complete");
    }

    private void FixedUpdate()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingUp", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingDown", false);

        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            Debug.Log(movementInput);

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
                //move right
                animator.SetBool("isMovingRight", true);

                animator.SetBool("isMovingLeft", false);
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingUp", false);

            }
            else if(movementInput.x == -1.0)
            {
                //move left
                animator.SetBool("isMovingLeft", true);

                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingUp", false);

            }
            else if (movementInput.y == 1.0)
            {
                //move up
                animator.SetBool("isMovingUp", true);

                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingDown", false);
                animator.SetBool("isMovingLeft", false);
            }    
            else if (movementInput.y == -1.0)
            {
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
            playerLocation = rb.position;
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
}


