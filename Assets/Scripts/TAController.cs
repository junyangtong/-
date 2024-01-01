using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAController : MonoBehaviour
{
    public float speed = 3f;
    int moveChangeAni;           //改变移动动画
    private Animator animator;   //动画
    
    private Rigidbody2D rigi;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        
    }

    private void move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");//控制水平方向
        float moveY = Input.GetAxisRaw("Vertical");//控制垂直方向
        //移动控制
         rigi.position += new Vector2( moveX * speed * Time.deltaTime,moveY * speed * Time.deltaTime);
      
        //方向控制
        if (moveX > 0)
        {
            transform.localScale = new Vector3(1.0f,1.0f, 1.0f);
        }
        else if(moveX<0)
        {
            transform.localScale = new Vector3(-1.0f,1.0f, 1.0f);
        }
        //动作控制
        if (moveX != 0 || moveY != 0)
        {
            moveChangeAni = 1;
        }
        else
        {
            moveChangeAni = 0;
        }
        
        animator.SetInteger("movement",moveChangeAni);

    }
    
    
}
