using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyButton : MonoBehaviour
{
    [SerializeField] Canvas itemInfoWindow;
    [SerializeField] Text itemSnapName;
    [SerializeField] Text itemSnapCost;
    [SerializeField] Text itemSnapType;
    [SerializeField] Text itemSnapDescript;

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
        infoPos = new Vector3(mousePos.x + 240, mousePos.y, 0);
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
        itemSnapCost.text = "가격 : " + itemManager.GetItem().GetItemCost() + "G";

        switch (itemManager.GetItem().GetItemType())
        {
            case "Magic2Scroll":
                itemSnapType.text = "마법 스크롤";
                itemSnapDescript.text = "마법 '화염 방사'를 해금 시켜주는 스크롤";
                break;
            case "Magic3Scroll":
                itemSnapType.text = "마법 스크롤";
                itemSnapDescript.text = "마법 '지진'을 해금 시켜주는 스크롤";
                break;
            case "SmallPotion":
                itemSnapType.text = "포션";
                itemSnapDescript.text = "체력을 10 회복 시켜주는 하급 포션";
                break;
            case "MiddlePotion":
                itemSnapType.text = "포션";
                itemSnapDescript.text = "체력을 25 회복 시켜주는 중급 포션";
                break;
            case "LargePotion":
                itemSnapType.text = "포션";
                itemSnapDescript.text = "체력을 40 회복 시켜주는 고급 포션";
                break;
            default:
                break;
        }


    }
}
