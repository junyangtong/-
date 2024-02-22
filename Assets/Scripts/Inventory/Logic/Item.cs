using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public ItemName itemName;
   private GameObject ToolTip;
   public void ItemPicked()
   {
         //添加到背包并隐藏物体
         InventoryManager.Instance.AddItem(itemName);
         this.gameObject.SetActive(false);
      
   }
   public void ShowToolTip(bool isShow)
   {
      //获取按键提示图
      ToolTip = transform.GetChild(0).gameObject;
      ToolTip.SetActive(isShow);
   }

}

