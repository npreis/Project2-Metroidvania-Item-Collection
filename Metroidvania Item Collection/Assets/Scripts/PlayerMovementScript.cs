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
    public LayerMask ground;
    public Transform groundCheck;

    MasterItemCheckScript itemCheck;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        itemCheck = gameObject.GetComponent<MasterItemCheckScript>();
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
        CheckGround();
    }

    void CheckInput()
    {
        xMove = Input.GetAxis("Horizontal") * xSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            shouldJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && itemCheck.canDoubleJump)
        {
            shouldDoubleJump = true;
        }
    }

    void Move()
    {
        myRigidbody.velocity = new Vector3(xMove, myRigidbody.velocity.y, 0);
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
        }
    }
}
