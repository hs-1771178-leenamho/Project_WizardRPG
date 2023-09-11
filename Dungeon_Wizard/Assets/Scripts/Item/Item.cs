using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct ItemInfo{
    public string item_name;
    public float item_Att;
    public float item_Def;
    public int item_Cost;
    public string item_Type;
}

[System.Serializable]
public class Item
{
    ItemInfo itemInfo;

    public Item(string itemName, float itemAtt, float itemDef, int itemCost, string itemType){
        itemInfo.item_name = itemName;
        itemInfo.item_Att = itemAtt;
        itemInfo.item_Def = itemDef;
        itemInfo.item_Cost = itemCost;
        itemInfo.item_Type = itemType;
    }

    public string GetItemName(){
        return itemInfo.item_name;
    }

    public float GetItemAtt(){
        return itemInfo.item_Att;
    }

    public float GetItemDef(){
        return itemInfo.item_Def;
    }

    public int GetItemCost(){
        return itemInfo.item_Cost;
    }

    public string GetItemType(){
        return itemInfo.item_Type;
    }
}
