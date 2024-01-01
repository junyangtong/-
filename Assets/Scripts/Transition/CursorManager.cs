//实现点击切换场景
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{    Ray ray;
    RaycastHit hit;
    GameObject obj;
    //private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    //private bool canClick;
    /*private void Update()
    {
        canClick = ObjectAtMousePosition();

        if (canClick && Input.GetMouseButtonDown(0))
        {
            //检测周标互动情况
            ClickAction(ObjectAtMousePosition().gameObject);
        }
}*/


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("点击鼠标左键");
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                obj = hit.collider.gameObject;
                //通过标签
                if (obj.tag == "Teleport")
                {
                    var teleport = obj.GetComponent<Teleport>();
                    teleport?.TeleportToScene();
                    Debug.Log("点中" + obj.name);
                }
            }
        }
    }

/*private void ClickAction(GameObject clickObject)
{
    switch(clickObject.tag)
        {
            case "Teleport":
            var teleport = clickObject.GetComponent<Teleport>();
            teleport?.TeleportToScene();
            break;
            case "Item":
            var item = clickObject.GetComponent<Item>(); 
            item?.ItemClicked();
            break;
        }
}


/// <summary>
/// 测鼠标点击范围的碰撞体
/// </summary>
/// <returns></returns>
/*private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }*/
    }