using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    public Image itemImage;
    public ItemDetails currentItem;
    private bool isSelected;
    private bool isSelectedtemp;
    public void SetItem(ItemDetails itemDetails)
    {
        currentItem = itemDetails;
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
    
    }
    
}
