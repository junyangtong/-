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
                collisionEvents.currentItem.ItemPicked();
            }
        }
    }
}
