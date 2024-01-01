using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SkillSortEnum
{

}

public class PlayController2 : MonoBehaviour
{
    public Vector3 flippedScale = new Vector3(-1,1,1);
    private Rigidbody2D rigi;
    private Animator animator;
    

    int moveChangeAni;

    public float MoveSpeed =  10f;
    private float moveX;
    public float jumpHeight = 5f;

    private int jumpCount;

    private bool isOnGround;
    private bool jumpis = false;
    private bool jumpSwitch = false;
    
   //冲刺

   public float dashSpeed;
   public float dashTime;
   private float startDashTimer;
   private bool isDashing;
   public GameObject dashObj;

    public Collider2D m_CrouchDisableCollider;
    private bool crouch = false;

    //技能
    public static PlayController2 Instance;
    private GameObject _skill1, _skill2, _skill3;
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Instance = this;
        _skill1 = Resources.Load<GameObject>("fire");
        _skill2 = Resources.Load<GameObject>("");
        _skill3 = Resources.Load<GameObject>("");
    }

    // Update is called once per frame
    void Update()
    {
        Crush();
        Movement();
        Direction();
        Jump();
        dash();

    }
   

    //移动
    private void Movement()
    {
        moveX = Input.GetAxis("Horizontal");
        
        rigi.velocity = new Vector2(moveX*MoveSpeed,rigi.velocity.y);

        if (moveX != 0)
        {
            moveChangeAni = 1;
        }
        else
        {
            moveChangeAni = 0;
        }
        
        animator.SetInteger("movement",moveChangeAni);

    }
    
    private void Direction()
    {
        if (moveX > 0)
        {
            transform.localScale = new Vector3(0.43f,0.43f, 0.43f);
        }
        else if(moveX<0)
        {
            transform.localScale = flippedScale;
        }
    }
    //跳跃
    private void Jump()
    {
        
        if (jumpis)
        {
            jumpSwitch = true;
        }
        
        
        if (Input.GetButtonDown("Jump")&& jumpis)
        {
          
            rigi.velocity = new Vector2(rigi.velocity.x,jumpHeight);
            animator.SetTrigger("jump");
            
        }
        if( Input.GetButtonDown("Jump") &&!jumpis &&jumpSwitch)
        {
            rigi.velocity = new Vector2(rigi.velocity.x,jumpHeight);
           
            jumpSwitch = false;
        }
       

        
    }
    //地面检测
    private void OnCollisionEnter2D(Collision2D col)
    {
        Grounding(col,false);
        jumpis = false;

        if (col.gameObject.tag == "Trap")
        {
            Death();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Grounding(collision,false);
        jumpis = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Grounding(other,true);
        jumpis = false;
    }

    private void Grounding(Collision2D col,bool exitState)
    {
        if (exitState)                                      //离开为真
        {
            if (col.gameObject.layer ==LayerMask.NameToLayer("Terrain"))
            {
                isOnGround = false;
            }
        }
        else
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain")&&!isOnGround&&col.contacts[0].normal==Vector2.up)
            {
                isOnGround = true;
                JumpCancle();
            }
            else if(col.gameObject.layer == LayerMask.NameToLayer("Terrain")&&!isOnGround&&col.contacts[0].normal==Vector2.down)
            {  
                JumpCancle();
            }
        }
        animator.SetBool("isOnGround",isOnGround);
    }

    private void JumpCancle()
    {
        animator.ResetTrigger("jump");
    }
    //冲刺
    private void dash()
    {
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                dashObj.SetActive(true);
                isDashing = true;
                startDashTimer = dashTime;
            }
        }
        else
        {
            startDashTimer -= Time.deltaTime;
            if (startDashTimer <=0)
            {
                isDashing = false;
                dashObj.SetActive(false);
            }
            else
            {
                rigi.velocity = transform.right * (dashSpeed * transform.localScale.x);  
                
            }
        }   
    }
    //死亡
    private void Death()
    {
        
        animator.SetTrigger("Death");
        rigi.bodyType = RigidbodyType2D.Static;
    }
    
    //重新加载场景
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Crush()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && jumpis)
        {
            m_CrouchDisableCollider.enabled = false;
            crouch = true;
            animator.SetBool("crouch", crouch);
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_CrouchDisableCollider.enabled = true;
            crouch = false;
            animator.SetBool("crouch", crouch);
        }
    }
    
    //释放技能
    public void UseSkill(SkillSortEnum skillSort)
    {
        switch (skillSort)
        {
            case SkillSortEnum.skill01:
            {
                GameObject clone = Instantiate<GameObject>(_skill1);
                clone.transform.position = transform.position;
            }
                break;
            case SkillSortEnum.skill02:
            {
                GameObject clone = Instantiate<GameObject>(_skill2);
                clone.transform.position = transform.position;
            }
                break;
            case SkillSortEnum.skill03:
            {
                GameObject clone = Instantiate<GameObject>(_skill3);
                clone.transform.position = transform.position;
            }
                break;
           
        }
    }
}
