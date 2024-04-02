using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvents : MonoBehaviour
{
    public bool CanInteractive = false;
    public Item currentItem;
    public GameObject currentObj;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        { 
            Debug.Log("靠近可交互物品");
            //靠近可交互物品
            CanInteractive = true;
            currentObj = collision.gameObject;
            switch(currentObj.transform.tag)
            {
                case "Item":
                currentItem = collision.gameObject.GetComponent<Item>();
                currentItem.ShowToolTip(true);
                break;
                case "NPC":
                Interactive interactive = collision.gameObject.GetComponent<Interactive>();
                interactive.ShowToolTip(true);
                break;
                case "ItemNoRequire":
                Interactive interactive1 = collision.gameObject.GetComponent<Interactive>();
                interactive1.ShowToolTip(true);
                break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        { 
            Debug.Log("离开可交互物品");
            // 离开可交互物品
            CanInteractive = false;
            currentObj = collision.gameObject;
            switch(collision.gameObject.transform.tag)
            {
                case "Item":
                currentItem = collision.gameObject.GetComponent<Item>();
                currentItem.ShowToolTip(false);
                break;
                case "NPC":
                Interactive interactive = collision.gameObject.GetComponent<Interactive>();
                interactive.ShowToolTip(false);
                break;
                case "ItemNoRequire":
                Interactive interactive1 = collision.gameObject.GetComponent<Interactive>();
                interactive1.ShowToolTip(false);
                break;
            }
            // 离开可交互物品时取消对话框
            EventHandler.CallShowDialogueEvent(string.Empty);
        }
    }
}
