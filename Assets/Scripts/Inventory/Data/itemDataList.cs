using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Inventory/ItemDataList")]
public class itemDataList : ScriptableObject
{
    public List<ItemDetails> itemDatailsList;
}

[System.Serializable]
public class ItemDetails
{
    public ItemName itemName;
    public Sprite itemSprite;
    
}