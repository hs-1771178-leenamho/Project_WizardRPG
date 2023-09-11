using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingButton : MonoBehaviour
{
    [SerializeField] Canvas itemInfoWindow;
    [SerializeField] Text itemSnapName;
    [SerializeField] Text itemSnapAtt;
    [SerializeField] Text itemSnapDef;
    [SerializeField] Text itemSnapCost;
    [SerializeField] Text itemSnapType;

    Vector3 mousePos;
    Vector3 infoPos;
    ItemManager itemManager;
    bool isInfoOpen = false;

    void Start()
    {
        itemManager = this.gameObject.GetComponent<ItemManager>();
    }

    public void MouseEnterOnImg()
    {
        Debug.Log("마우스 들어옴");
        mousePos = Input.mousePosition;
        infoPos = new Vector3(mousePos.x + 120, mousePos.y, 0);
        if (!isInfoOpen)
        {
            itemInfoWindow.gameObject.SetActive(true);
            isInfoOpen = true;
        }

        itemInfoWindow.transform.position = infoPos;
        ItemInfoSetting();
    }

    public void MouseExitOnImg()
    {
        Debug.Log("마우스 나감");
        if (isInfoOpen)
        {
            itemInfoWindow.gameObject.SetActive(false);
            isInfoOpen = false;
        }
    }


    void ItemInfoSetting()
    {
        itemSnapName.text = "이름 : " + itemManager.GetItem().GetItemName();
        itemSnapAtt.text = "공격력 : " + itemManager.GetItem().GetItemAtt();
        itemSnapDef.text = "방어력 : " + itemManager.GetItem().GetItemDef();
        itemSnapCost.text = "가격 : " + itemManager.GetItem().GetItemCost() + "G";

        switch (itemManager.GetItem().GetItemType())
        {
            case "Weapon":
                itemSnapType.text = "무기";
                break;
            case "Armor":
                itemSnapType.text = "방어구";
                break;
            default:
                break;
        }


    }


}
