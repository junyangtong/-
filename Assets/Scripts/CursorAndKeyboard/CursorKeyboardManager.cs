using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorKeyboardManager : MonoBehaviour
{
    public CollisionEvents collisionEvents;

    
    private void Update() 
    {
        if(collisionEvents.CanInteractive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {   
                switch(collisionEvents.currentObj.transform.tag)
                {
                    case "Item":
                        collisionEvents.currentItem.ItemPicked();
                        break;
                    case "NPC":
                        var interactive = collisionEvents.currentObj.gameObject.GetComponent<Interactive>();
                                if(InventoryManager.Instance.holdItem)
                                    {
                                        interactive?.CheckItem(InventoryManager.Instance.currentItem);
                                        //if(interactive.isDone)
                                            InventoryManager.Instance.holdItem =false;//如果物品成功使用了 则取消选择状态
                                    }
                                else
                                    interactive?.EmptyClicked();
                        break;
                    case "ItemNoRequire":
                        var interactive1 = collisionEvents.currentObj.gameObject.GetComponent<Interactive>();
                        interactive1?.OnClickedActionNoRequire();
                        break;
                }
            }
        }
    }
}
