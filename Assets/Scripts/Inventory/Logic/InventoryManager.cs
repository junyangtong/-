using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InventoryManager : Singleton<InventoryManager>
{
   public itemDataList itemData;
   public GameObject slotPrefab;
   public GameObject slotGrid;
   private ItemDetails itemDetails;
   [SerializeField]private List<ItemName> itemList = new List<ItemName>();
   public void AddItem(ItemName itemName)
   {
      if (!itemList.Contains(itemName))
      {
         itemList.Add(itemName);
         // UI对应显示
            itemDetails = itemData.GetItemDetails(itemName);
            CreateNewItem(itemDetails);
      }
      else
      {
         Debug.Log("请勿重复添加");
      }
   }
   public void CreateNewItem(ItemDetails itemDetails)
   {
      GameObject newItem = Instantiate(slotPrefab,slotGrid.transform.position,Quaternion.identity);
      newItem.transform.SetParent(slotGrid.transform);
      // 设置实例化物体的参数
      SlotUI slotUI = newItem.GetComponent<SlotUI>();
      slotUI.SetItem(itemDetails);
      Debug.Log("拾取"+itemDetails.itemName);
   }
}
