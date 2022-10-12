using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    float xMove = 0f;
    public float xSpeed = 5f;
    Rigidbody myRigidbody;
    public float jumpForce = 350f;
    bool isGrounded = false;
    bool shouldJump = false;
    bool shouldDoubleJump = false;
    bool hasDoubleJumped = false;
    bool shouldDash = false;
    bool hasDashed = false;
    public LayerMask ground;
    public Transform groundCheck;

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
        CheckDash();
        CheckGround();
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
    }

    void Move()
    {
        myRigidbody.velocity = new Vector3(xMove, myRigidbody.velocity.y, 0);
        if(myRigidbody.velocity.x > 0.0)
        {
            lastKeyHit = KeyCode.D;
        }
        else if (myRigidbody.velocity.x < 0.0)
        {
            lastKeyHit = KeyCode.A;
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
        //myRigidbody.velocity = Vector3.zero;
        if(lastKeyHit == KeyCode.D)
        {
            myRigidbody.velocity = Vector3.right * dashSpeed;

        }
        else if(lastKeyHit == KeyCode.A)
        {
            myRigidbody.velocity = Vector3.left * dashSpeed;
        }
        shouldDash = false;
        hasDashed = true;

        while(dashTime >= 0)
        {
            dashTime -= Time.deltaTime;
            myRigidbody.velocity = myRigidbody.velocity;
        }
        dashTime = startDashTime;
        //myRigidbody.velocity = Vector3.zero;
    }

    void CheckGround()
    {
        Collider[] coll = Physics.OverlapSphere(groundCheck.position, 1f, ground);
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
}
