using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyShop : Shop
{
    [SerializeField] Button M2_button;
    [SerializeField] Button M3_button;
    MagicShoot magicShoot;

    PlayerInfo playerInfo;
    bool magic2Lock;
    bool magic3Lock;

    void OnEnable() {
        playerInfo = FindObjectOfType<PlayerInfo>();
        magic2Lock = playerInfo.GetInfoM2Lock();
        magic3Lock = playerInfo.GetInfoM3Lock();

        M2_button.interactable = magic2Lock;
        M3_button.interactable = magic3Lock;
    }

    public override void Purchase()
    {
        magicShoot = FindObjectOfType<MagicShoot>();

        if (purchaseWindow.gameObject.activeSelf)
        {
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
            player.BuyItemSFX();
            if (itemToBuy.gameObject.tag == "Magic2Scroll")
            {
                itemToBuy.gameObject.GetComponent<Button>().interactable = false;
                magicShoot.UnlockMagic2();
            }
            else if (itemToBuy.gameObject.tag == "Magic3Scroll")
            {
                itemToBuy.gameObject.GetComponent<Button>().interactable = false;
                magicShoot.UnlockMagic3();
            }
            else if (itemToBuy.gameObject.tag == "SmallPotion")
            {
                player.IncreaseSmallPotionAmount();
            }
            else if (itemToBuy.gameObject.tag == "MiddlePotion")
            {
                player.IncreaseMiddlePotionAmount();
            }
            else if (itemToBuy.gameObject.tag == "LargePotion")
            {
                player.IncreaseLargePotionAmount();
            }
            //UpgradePlayerStat(itemToPurchase);
            //return;
        }
    }


}
