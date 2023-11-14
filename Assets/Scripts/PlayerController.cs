using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //spells discovered array
    public static bool fireEnabled = false;


    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

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
            }
            
            if(movementInput.x == -1.0)
            {
                //move left
                animator.SetBool("isMovingLeft", true);
            }
            
            if (movementInput.y == 1.0)
            {
                //move up
                animator.SetBool("isMovingUp", true);
            }
            
            if (movementInput.y == -1.0)
            {
                //move down
                animator.SetBool("isMovingDown", true);
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
        //if(fireValue == )
    }
}


