using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvents : MonoBehaviour
{
    public bool CanInteractive = false;
    public Item currentItem;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        { 
            //靠近可交互物品
            CanInteractive = true;
            //显示按键提示
            currentItem = collision.gameObject.GetComponent<Item>();
            currentItem.ShowToolTip(true);

            Debug.Log("靠近可交互物品");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        { 
            // 离开可交互物品
            CanInteractive = false;
            //关闭按键提示
            Item item = collision.gameObject.GetComponent<Item>();
            item.ShowToolTip(false);
            Debug.Log("离开可交互物品");
        }
    }
}
