using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    float xMove = 0f;
    public float xSpeed = 5f;
    Rigidbody myRigidbody;
    public float jumpForce = 350f;
    public bool isGrounded = false;
    public bool isRWalled = false;
    public bool isLWalled = false;
    bool shouldJump = false;
    bool shouldDoubleJump = false;
    bool hasDoubleJumped = false;
    bool shouldDash = false;
    bool hasDashed = false;
    bool shouldWallJump = false;
    public LayerMask ground;
    public LayerMask wall;
    public Transform groundCheck;
    public Transform rWallCheck;
    public Transform lWallCheck;

    MasterItemCheckScript itemCheck;
    private KeyCode lastKeyHit;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        itemCheck = gameObject.GetComponent<MasterItemCheckScript>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        Move();

        CheckJump();
        CheckWallJump();
        CheckDash();

        CheckGround();
        CheckWalls();
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal") * xSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            shouldJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && itemCheck.canDoubleJump && !hasDoubleJumped)
        {
            shouldDoubleJump = true;
        }
        if(Input.GetKeyDown(KeyCode.Q) && itemCheck.canDash && !hasDashed)
        {
            shouldDash = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) && itemCheck.canWallJump && (isRWalled || isLWalled))
        {
            shouldWallJump = true;
        }
    }

    void Move()
    {
        if(!shouldDash)
        {
            myRigidbody.velocity = new Vector3(xMove, myRigidbody.velocity.y, 0);
            if (myRigidbody.velocity.x > 0.0)
            {
                lastKeyHit = KeyCode.D;
            }
            else if (myRigidbody.velocity.x < 0.0)
            {
                lastKeyHit = KeyCode.A;
            }
        }
    }

    void CheckJump()
    {
        if (shouldJump || shouldDoubleJump)
        {
            Jump();
        }
    }

    void Jump()
    {
        if(itemCheck.canDoubleJump)
        {
            if(!isGrounded)
            {
                if(!hasDoubleJumped)
                {
                    myRigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    shouldDoubleJump = false;
                    myRigidbody.AddForce(Vector3.up * jumpForce);
                    hasDoubleJumped = true;
                }
            }
            else
            {
                shouldJump = false;
                myRigidbody.AddForce(Vector3.up * jumpForce);
            }
        }
        else
        {
            shouldJump = false;
            myRigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    void CheckDash()
    {
        if (shouldDash)
        {
            Dash();
        }
    }

    void Dash()
    {
        if(lastKeyHit == KeyCode.D)
        {
            myRigidbody.velocity = Vector3.right * dashSpeed;


        }
        else if(lastKeyHit == KeyCode.A)
        {
            myRigidbody.velocity = Vector3.left * dashSpeed;
        }

        dashTime -= Time.deltaTime;

        if (dashTime <= 0)
        {
            shouldDash = false;
            hasDashed = true;
            dashTime = startDashTime;
        }
    }

    void CheckWallJump()
    {
        if(shouldWallJump)
        {
            WallJump();
        }
    }

    void WallJump()
    {
        if(isLWalled)
        {
            myRigidbody.AddForce(new Vector3(5.0f, 1.0f) * jumpForce);
        }
        if(isRWalled)
        {
            myRigidbody.AddForce(new Vector3(-5.0f, 1.0f) * jumpForce);
        }
        shouldWallJump = false;
    }

    void CheckGround()
    {
        Collider[] coll = Physics.OverlapSphere(groundCheck.position, 0.1f, ground);
        if (coll.Length == 0)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
            hasDoubleJumped = false;
            hasDashed = false;
        }
    }

    void CheckWalls()
    {
        Collider[] coll1 = Physics.OverlapSphere(rWallCheck.position, 0.1f, wall);
        Collider[] coll2 = Physics.OverlapSphere(lWallCheck.position, 0.1f, wall);

        if(coll1.Length == 0 && coll2.Length == 0)
        {
            isRWalled = false;
            isLWalled = false;
        }
        else
        {
            hasDoubleJumped = false;
            hasDashed = false;

            if(coll1.Length != 0)
            {
                isRWalled = true;
            }
            if(coll2.Length != 0)
            {
                isLWalled = true;
            }
        }
    }
}
