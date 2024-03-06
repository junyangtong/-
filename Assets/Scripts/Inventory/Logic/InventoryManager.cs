using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InventoryManager : Singleton<InventoryManager>
{
   public itemDataList itemData;
   public GameObject slotPrefab;
   public GameObject slotGrid;
   public bool isSelected;
   public bool holdItem;
   public ItemName currentItem;
   private ItemDetails itemDetails;
   [SerializeField]private List<ItemName> itemList = new List<ItemName>();
   private void OnEnable() 
    {
         EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
    }
    void OnDisable()
    {
      EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails)
    {   
         holdItem = true;
         currentItem = itemDetails.itemName;
         // 高亮显示
         HighLightItem(currentItem);

         Debug.Log("当前选择"+currentItem); 
    }
    private void OnItemUsedEvent(ItemName itemName)
    {
        int children = slotGrid.transform.childCount;
        for (int i = 0; i < children; i++)
        {   
            SlotUI slotUI = slotGrid.transform.GetChild(i).GetComponent<SlotUI>();
            if(slotUI.currentItem.itemName == itemName)
            {
                //InitializeSelectionState();
                Destroy(slotUI.gameObject);
                Debug.Log("移除背包中的"+slotUI.currentItem.itemName);
                itemList.Remove(itemName);
            }
        }
        
    }
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
   // 使用物品时移除背包中的对应物品
    private void HighLightItem(ItemName itemName)
    {
        int children = slotGrid.transform.childCount;
        for (int i = 0; i < children; i++)
        {   
            SlotUI slotUI = slotGrid.transform.GetChild(i).GetComponent<SlotUI>();
            if(slotUI.currentItem.itemName == itemName)
            {
               slotUI.HighLight(true);
            }
            else
               slotUI.HighLight(false);
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
