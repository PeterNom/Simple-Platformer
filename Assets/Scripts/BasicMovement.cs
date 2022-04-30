using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private bool onGround = true;
    private Transform playerImage;
    private Animator playerAnim;

    public Transform onGroundCheck;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float speed     = 5;
    public float jumpForce = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool facingRight = true;
    public float horizontalValue;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerImage = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        BetterJump();
        properFlip();
        CheckIfGrounded();
    }


    void Move()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal") * speed;

        if( ( horizontalValue<0 || horizontalValue > 0 ) && ( onGround ) )
        {
            playerAnim.SetBool("Moving", true);
        }
        else
        {
            playerAnim.SetBool("Moving", false);
        }

        playerRigidBody.velocity = new Vector2(horizontalValue, playerRigidBody.velocity.y);
    }

    void Jump()
    {
        //Input.GetKeyDown(KeyCode.Space)
        if (Input.GetAxis("Jump") != 0 && onGround)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);
        }
    }

    void CheckIfGrounded()
    {
        Collider2D groundCollider = Physics2D.OverlapCircle(onGroundCheck.position, checkGroundRadius, groundLayer);

        if (groundCollider == null)
        {
            onGround = false;
        }
        else
        {
            onGround = true;
        }

        playerAnim.SetBool("OnGround", onGround);
    }

    void BetterJump()
    {
        if(playerRigidBody.velocity.y < 0)
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            playerRigidBody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void properFlip()
    {
        if ((horizontalValue < 0 && facingRight) || (horizontalValue > 0 && !facingRight))
        {
            facingRight = !facingRight;
            playerImage.Rotate(new Vector3(0, 180, 0));
        }
    }
}
