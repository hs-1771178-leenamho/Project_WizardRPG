using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] Text smallPotionAmount;
    [SerializeField] Text middlePotionAmount;
    [SerializeField] Text largePotionAmount;
    [SerializeField] Image hpBar;
    [SerializeField] Image magic_FireStream;
    [SerializeField] Image magic_EarthShatter;
    [SerializeField] Text playerGold;
    [SerializeField] Text playerAtt;
    [SerializeField] Text playerDef;

    PlayerStat playerStat;
    MagicShoot magicShoot;
    Color alpha_2;
    Color alpha_3;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        magicShoot = FindObjectOfType<MagicShoot>();
        alpha_2 = magic_FireStream.color;
        alpha_3 = magic_EarthShatter.color;
        alpha_2.a = 1f;
        alpha_3.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI(){
        this.smallPotionAmount.text = playerStat.GetSmallPotion().ToString();
        this.middlePotionAmount.text = playerStat.GetMiddlePotion().ToString();
        this.largePotionAmount.text = playerStat.GetLargePotion().ToString();
        this.hpBar.fillAmount = playerStat.GetHp() / 100f;
        this.playerGold.text = playerStat.GetGold().ToString();
        this.playerAtt.text = playerStat.GetAtt().ToString();
        this.playerDef.text = playerStat.GetDef().ToString();

        if(!magicShoot.magic2Lock){
            magic_FireStream.color = alpha_2;
        }

        if(!magicShoot.magic3Lock){
            magic_EarthShatter.color = alpha_3;
        }

    }
}
