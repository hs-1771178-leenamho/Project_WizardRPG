using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] float itemAtt;
    [SerializeField] float itemDef;
    [SerializeField] int itemCost;
    [SerializeField] string itemType;

    Item item;
    // Start is called before the first frame update
    void Start()
    {
        item = new Item(itemName, itemAtt, itemDef, itemCost, itemType);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public Item GetItem(){
        return this.item;
    }

    
}
