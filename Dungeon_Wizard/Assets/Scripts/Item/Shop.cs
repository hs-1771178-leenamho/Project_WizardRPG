using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] protected Canvas purchaseWindow; // 구매 여부 물어보는 캔버스
    [SerializeField] protected Canvas AlertWindow; // 구매 성공/실패 여부 나타내는 캔버스
    [SerializeField] protected Text AlertText;
    [SerializeField] protected GameObject buttonClickSFXManager;
    //shoppingBT에 Item 스크립트를 넣어서 버튼이 아이템 정보를 가지고 있게 만듬

    //Item[] shopInven;
    protected PlayerStat player;
    protected GameObject itemToBuy;

    void Start()
    {
        player = FindObjectOfType<PlayerStat>();
    }
    public void ClickSoundPlay(){
        if(buttonClickSFXManager == null) return;
        buttonClickSFXManager.GetComponent<AudioSource>().Play();
    }

    public void ClickToPurchase() // 쇼핑창에서 아이템 클릭으로 구매 여부 묻기
    {
        if (purchaseWindow == null) return;
        ClickSoundPlay();
        purchaseWindow.gameObject.SetActive(true);
        itemToBuy = EventSystem.current.currentSelectedGameObject;
    }

    public void ClosePurchaseWindow()
    { // 구매 여부 묻는 창 닫기
        if (purchaseWindow == null) return;
        ClickSoundPlay();
        purchaseWindow.gameObject.SetActive(false);
    }

    public virtual void Purchase()
    {
        if(purchaseWindow.gameObject.activeSelf){
            purchaseWindow.gameObject.SetActive(false);
        }

        // 플레이어의 소지금과 아이템 가격 비교 후 구매하기
        // 구매 여부 묻는 창에서 확인을 눌렀을 때 이벤트

        int playerGold = player.GetGold(); // 소지금

        if (itemToBuy == null) return;
        Item itemToPurchase = itemToBuy.GetComponent<ItemManager>().GetItem();
        int shoppingCost = itemToPurchase.GetItemCost(); // 아이템 가격

        if (playerGold < shoppingCost)
        {
            ClickSoundPlay();
            // 돈 부족 스크린을 띄우기
            Debug.Log("돈 부족");
            if (AlertWindow == null || AlertText == null) return;
            AlertText.text = "돈이 부족합니다.";
            AlertWindow.gameObject.SetActive(true);
            //return;
        }
        else
        {
            Debug.Log("구매 성공");
            player.ReduceGold(shoppingCost);
            // 구매 성공 스크린을 띄우기
            // 구매한 아이템 버튼을 disable 시켜버리기
            //itemToBuy.gameObject.GetComponent<Button>().interactable = false;
            if (AlertWindow == null || AlertText == null) return;
            AlertText.text = "구매 했습니다.";
            AlertWindow.gameObject.SetActive(true);
            UpgradePlayerStat(itemToPurchase);
            player.BuyItemSFX();
            //return;
        }
    }

    public void CloseAlertTab()
    {
        if (AlertWindow == null) return;
        ClickSoundPlay();
        AlertWindow.gameObject.SetActive(false);
    }

    void UpgradePlayerStat(Item item) // 구매 성공 후 바로 스텟에 계산
    {
        if (item == null) return;
        
        string _type = item.GetItemType();
        float _att = item.GetItemAtt();
        float _def = item.GetItemDef();

        switch (_type)
        {
            case "Weapon":
                player.UpgradeAtt(_att);
                break;
            case "Armor":
                player.UpgradeDef(_def);
                break;
            default:
                break;
        }

    }

}
