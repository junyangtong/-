using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    private Animator anim;
    public float playerSpeed = 5f ;
    
    [Range(0, 10)] public float jumpSpeed = 5f;

    public bool isGrounded;//已经在地面上了

    public Transform groundCheck;//监测点

    public LayerMask ground;

    public float fallAddition = 3.5f;//下落加成

    public float jumpAddition = 1.5f;//跳跃加成

    public Collider2D m_CrouchDisableCollider;
   
    public Collider2D m_CrouchEnableCollider;
    
    private int jumpCount = 2;
    
    private float moveX;

    private bool facingRight = true;

    private bool moveJump;

    private bool jumpHold;//长按跳跃

    private bool isJump;

    private enum playerState
    {
        Idor,Run,Jump,Fall,Down,CroushIdor,CroushRun
    };
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        m_CrouchEnableCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetButtonDown("Jump");
        jumpHold = Input.GetButton("Jump");
        
        if (moveJump&&jumpCount>0)
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,0.1f,ground);
        
        Move();
        jump();
        PlayerAnim();
    }

    private void Move()
    {
        
        rb.velocity = new Vector2(moveX*playerSpeed,rb.velocity.y);
        
        if (!facingRight && moveX>0)
        {
            flip();
        }
        else if (facingRight && moveX<0)
        {
            flip();
        }
        
    }

    private void flip() //翻转
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
        
    }

    private void jump()
    {
        if (isGrounded)
        {
            jumpCount = 2;
        }
        if (isJump)
        {
            rb.AddForce(Vector2.up*jumpSpeed,ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }

        if (rb.velocity.y<0)
        {
            rb.gravityScale = fallAddition;
        }
        else if (rb.velocity.y>0 && !jumpHold)
        {
            rb.gravityScale = jumpAddition;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void PlayerAnim()
    {
        playerState states;
        if (Mathf.Abs(moveX)>0)
        {
            states = playerState.Run;
        }
        else
        {
            states = playerState.Idor;
        }

        if (rb.velocity.y>0.1f&& !isGrounded)
        {
            states = playerState.Jump;
        }
        else if (rb.velocity.y<-0.1f)
        {
            states = playerState.Fall;
            if (isGrounded&&rb.velocity.y<-0.1f)
            {
                states = playerState.Down;
            }
        }
        
        if (Input.GetKey(KeyCode.LeftControl)&& isGrounded && moveX == 0)
        {
            m_CrouchDisableCollider.enabled = false;
            m_CrouchEnableCollider.enabled = true;
            states = playerState.CroushIdor;
        }
        else if (Input.GetKey(KeyCode.LeftControl)&& isGrounded && moveX != 0)
        {
            m_CrouchDisableCollider.enabled = false;
            m_CrouchEnableCollider.enabled = true;
            states = playerState.CroushRun;
        }
        else
        {
            m_CrouchDisableCollider.enabled = true;
            m_CrouchEnableCollider.enabled = false;
        }

        
        anim.SetInteger("State",(int)states);
    }
}
