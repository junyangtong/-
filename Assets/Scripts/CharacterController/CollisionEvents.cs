using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvents : MonoBehaviour
{
    public bool CanInteractive = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        { 
            //靠近可交互物品
            CanInteractive = true;
            Debug.Log("靠近可交互物品");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        { 
            // 离开可交互物品
            CanInteractive = false;
            Debug.Log("离开可交互物品");
        }
    }
}
