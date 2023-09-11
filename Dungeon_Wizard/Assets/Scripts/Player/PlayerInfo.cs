using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData{
    public string name;
    public float Hp = 100f;
    public float Att = 10f;
    public float Def = 10f;
    public int Gold = 20;
    public int SmallPotionAmount = 0;
    public int MiddlePotionAmount = 0;
    public int LargePotionAmount = 0;
    public bool Magic2Lock = true;
    public bool Magic3Lock = true;

    // public PlayerData(float _hp, float _att, float _def, int _gold, int _spAmount, int _mpAmount, int _lpAmount, 
    // bool _m2Lock, bool _m3Lock){
    //     this.Hp = _hp;
    //     this.Att = _att;
    //     this.Def = _def;
    //     this.Gold = _gold;
    //     this.SmallPotionAmount = _spAmount;
    //     this.MiddlePotionAmount = _mpAmount;
    //     this.LargePotionAmount = _lpAmount;
    //     this.Magic2Lock = _m2Lock;
    //     this.Magic3Lock = _m3Lock;
    // }

}

public class PlayerInfo : MonoBehaviour
{
    // float Hp = 100f;
    // float Att = 10f;
    // float Def = 10f;
    // int Gold = 50;
    // int SmallPotionAmount = 0;
    // int MiddlePotionAmount = 0;
    // int LargePotionAmount = 0;
    // bool Magic2Lock = true;
    // bool Magic3Lock = true;

    public static PlayerInfo instance;
    public PlayerData playerData = new PlayerData();
    public string path;
    public int selectedSlotNumber;
    void Awake()
    {
       // var obj = FindObjectsOfType<PlayerInfo>();
        if (instance == null)
        {
           // DontDestroyOnLoad(gameObject);
           instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    // void Start(){
    //     SaveData();
    // }
#region Set
    public void SetInfoHp(float _hp)
    {
        playerData.Hp = _hp;
    }

    public void SetInfoAtt(float _att)
    {
        playerData.Att = _att;
    }

    public void SetInfoDef(float _def)
    {
        playerData.Def = _def;
    }

    public void SetInfoGold(int _gold)
    {
        playerData.Gold = _gold;
    }

    public void SetInfoSPAmount(int _spAmount)
    {
        playerData.SmallPotionAmount = _spAmount;
    }

    public void SetInfoMPAmount(int _mpAmount)
    {
        playerData.MiddlePotionAmount = _mpAmount;
    }

    public void SetInfoLPAmount(int _lpAmount)
    {
        playerData.LargePotionAmount = _lpAmount;
    }

    public void SetInfoM2Lock(bool _m2Lock)
    {
        playerData.Magic2Lock = _m2Lock;
    }

    public void SetInfoM3Lock(bool _m3Lock)
    {
        playerData.Magic3Lock = _m3Lock;
    }
#endregion

#region Get
    public float GetInfoHp()
    {
        return playerData.Hp;
    }

    public float GetInfoAtt()
    {
        return playerData.Att;
    }

    public float GetInfoDef()
    {
        return playerData.Def;
    }

    public int GetInfoGold()
    {
        return playerData.Gold;
    }

    public int GetInfoSPAmount()
    {
        return playerData.SmallPotionAmount;
    }

    public int GetInfoMPAmount()
    {
        return playerData.MiddlePotionAmount;
    }

    public int GetInfoLPAmount()
    {
        return playerData.LargePotionAmount;
    }

    public bool GetInfoM2Lock()
    {
        return playerData.Magic2Lock;
    }

    public bool GetInfoM3Lock()
    {
        return playerData.Magic3Lock;
    }
    #endregion

    
    public void SaveData(){
        string data = JsonUtility.ToJson(playerData);
        print(data);
        File.WriteAllText(path + selectedSlotNumber.ToString(), data);
    }

    public void LoadData(){
        string data = File.ReadAllText(path + selectedSlotNumber.ToString());
        playerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear(){
        selectedSlotNumber = -1;
        playerData = new PlayerData();
    }
}

