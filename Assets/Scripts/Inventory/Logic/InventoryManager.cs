using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InventoryManager : Singleton<InventoryManager>
{
   [SerializeField]private List<ItemName> itemList = new List<ItemName>();
   public void AddItem(ItemName itemName)
   {
      if (!itemList.Contains(itemName))
      {
         itemList.Add(itemName);
      }
   }
}
