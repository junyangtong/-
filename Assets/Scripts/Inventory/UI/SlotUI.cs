using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    public Image itemImage;
    public ItemDetails currentItem;
    public void SetItem(ItemDetails itemDetails)
    {
        currentItem = itemDetails;
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //呼叫方法 选择物品
        EventHandler.CallItemSelectedEvent(currentItem);
    }
    public void HighLight(bool isSelected)
    {
        //高亮显示
        if(isSelected)
            itemImage.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        else
            itemImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    
}
