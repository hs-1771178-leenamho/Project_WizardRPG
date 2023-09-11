using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class PlayerStat : MonoBehaviour
{
    [SerializeField] float Hp = 100f;
    [SerializeField] float Att = 10f;
    [SerializeField] float Def = 10f;
    [SerializeField] int Gold = 50;
    [SerializeField] Animator _animator;
    [SerializeField] int SmallPotion_Amount;
    [SerializeField] int MiddlePotion_Amount;
    [SerializeField] int LargePotion_Amount;
    [SerializeField] AudioClip deadSound;
    [SerializeField] AudioClip potionUseSound;
    [SerializeField] AudioClip buySound;
    [SerializeField] AudioClip healSound;
    
    

    public bool isPlayerDead = false;
    PlayerInfo playerInfo;
    DungeonFail dungeonFail;

    AudioSource playerAudioSource;

    void Start()
    {
        _animator.ResetTrigger("Dead");
        playerInfo = FindObjectOfType<PlayerInfo>();
        dungeonFail = FindObjectOfType<DungeonFail>();
        playerAudioSource = GetComponent<AudioSource>();
        if (playerInfo == null) return;
        SettingStat();
        Cursor.visible = false;

    }

    private void SettingStat()
    {
        this.Hp = playerInfo.GetInfoHp();
        this.Att = playerInfo.GetInfoAtt();
        this.Def = playerInfo.GetInfoDef();
        this.Gold = playerInfo.GetInfoGold();
        this.SmallPotion_Amount = playerInfo.GetInfoSPAmount();
        this.MiddlePotion_Amount = playerInfo.GetInfoMPAmount();
        this.LargePotion_Amount = playerInfo.GetInfoLPAmount();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            UseSmallPotion();
        }

        if(Input.GetKeyDown(KeyCode.X)){
            UseMiddlePotion();
        }

        if(Input.GetKeyDown(KeyCode.C)){
            UseLargePotion();
        }
    }

    public bool IsPlayerDead(){
        return isPlayerDead;
    }
    public void GetDamage(float _dmg)
    {
        if(_dmg <= 0f) _dmg = 1f;
        this.Hp -= _dmg;
        Debug.Log(this.Hp);
        playerInfo.SetInfoHp(this.Hp);

        if (this.Hp <= 0f)
        {
            isPlayerDead = true;
            Debug.Log("플레이어 죽음");
            ProcessDead();
        }
    }

    public float GetHp()
    {
        return Hp;
    }

    public float GetAtt()
    {
        return Att;
    }

    public float GetDef()
    {
        return Def;
    }
    
    public int GetGold(){
        return Gold;
    }

    public void Heal(float _hp)
    {
        playerAudioSource.clip = healSound;
        playerAudioSource.Play();
        this.Hp += _hp;
        if(this.Hp >= 100f) this.Hp = 100f;
        playerInfo.SetInfoHp(this.Hp);
    }

    public void UpgradeAtt(float _att)
    {
        this.Att += _att;
        playerInfo.SetInfoAtt(this.Att);
    }

    public void UpgradeDef(float _def)
    {
        this.Def += _def;
        playerInfo.SetInfoDef(this.Def);
    }

    void ProcessDead() // 실패 캔버스 띄워야함
    {
        FindObjectOfType<ThirdPersonController>().enabled = false;
        if (_animator == null) return;
        if (playerAudioSource == null) return;
        playerAudioSource.clip = deadSound;
        playerAudioSource.Play();
        _animator.SetTrigger("Dead");
        
        dungeonFail.Fail();
    }

    

    public void ReduceGold(int _price){
        this.Gold -= _price;
        playerInfo.SetInfoGold(this.Gold);
    }
    public void BuyItemSFX()
    {
        playerAudioSource.clip = buySound;
        playerAudioSource.Play();
    }

    public void IncreaseSmallPotionAmount()
    {
        BuyItemSFX();
        this.SmallPotion_Amount++;
        Debug.Log("하급 포션 구매 : " + this.SmallPotion_Amount);
        playerInfo.SetInfoSPAmount(this.SmallPotion_Amount);
    }

    

    public void IncreaseMiddlePotionAmount(){
        BuyItemSFX();
        this.MiddlePotion_Amount++;
        Debug.Log("중급 포션 구매 : " + this.MiddlePotion_Amount);
        playerInfo.SetInfoMPAmount(this.MiddlePotion_Amount);
    }

    public void IncreaseLargePotionAmount(){
        BuyItemSFX();
        this.LargePotion_Amount++;
        Debug.Log("고급 포션 구매 : " + this.LargePotion_Amount);
        playerInfo.SetInfoLPAmount(this.LargePotion_Amount);
    }

    private void UsePotionSFX()
    {
        playerAudioSource.clip = potionUseSound;
        playerAudioSource.Play();
    }

    public void UseSmallPotion()
    {
        if (this.SmallPotion_Amount <= 0) return;
        this.Hp += 10;
        if (this.Hp >= 100f) this.Hp = 100f;
        this.SmallPotion_Amount--;
        Debug.Log("초급 포션 사용");
        UsePotionSFX();
        playerInfo.SetInfoSPAmount(this.SmallPotion_Amount);
    }

    

    public void UseMiddlePotion(){
        if(this.MiddlePotion_Amount <= 0) return;
        this.Hp += 25;
        if(this.Hp >= 100f) this.Hp = 100f;
        this.MiddlePotion_Amount--;
        Debug.Log("중급 포션 사용");
        UsePotionSFX();
        playerInfo.SetInfoMPAmount(this.MiddlePotion_Amount);
    }

    public void UseLargePotion(){
        if(this.LargePotion_Amount <= 0) return;
        this.Hp += 40;
        if(this.Hp >= 100f) this.Hp = 100f;
        this.LargePotion_Amount--;
        Debug.Log("고급 포션 사용");
        UsePotionSFX();
        playerInfo.SetInfoLPAmount(this.LargePotion_Amount);
    }

    public int GetSmallPotion(){
        return this.SmallPotion_Amount;
    }


    public int GetMiddlePotion(){
        return this.MiddlePotion_Amount;
    }

    
    public int GetLargePotion(){
        return this.LargePotion_Amount;
    }

    

}
