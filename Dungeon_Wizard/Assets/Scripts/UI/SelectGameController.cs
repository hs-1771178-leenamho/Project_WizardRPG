using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SelectGameController : MonoBehaviour
{
    public GameObject creatGameCanvas;
    public Text[] slotText;
    public Text newPlayerName;
    bool[] saveFiles = new bool[3];
    
    // Start is called before the first frame update
    void Awake()
    {
        // 슬롯 별로 저장된 데이터가 있는지 확인
        for(int i = 0; i < 3; i++){
            if(File.Exists(PlayerInfo.instance.path + $"{i}")){
                saveFiles[i] = true;
                PlayerInfo.instance.selectedSlotNumber = i;
                PlayerInfo.instance.LoadData();
                slotText[i].text = PlayerInfo.instance.playerData.name;
                Debug.Log("존재");
            }
            else{
                slotText[i].text = "empty";
                slotText[i].color = Color.gray;
            }

            
        }
        PlayerInfo.instance.DataClear();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlotSetting(int _num){
        PlayerInfo.instance.selectedSlotNumber = _num;

        // 저장된 데이터가 있을 때 >> 저장된 데이터를 불러와 게임씬으로 이동
        if(saveFiles[_num]){
            PlayerInfo.instance.LoadData();
            
            StartGame();
        }
        else{// 저장된 데이터가 없을 때 
            CreatGameCanvasOn();
        }
        
        
    }

    public void CreatGameCanvasOn(){
        creatGameCanvas.SetActive(true);
    }

    public void StartGame(){
        if(!saveFiles[PlayerInfo.instance.selectedSlotNumber]){
            PlayerInfo.instance.playerData.name = newPlayerName.text;
            PlayerInfo.instance.SaveData();
        }
        SceneManager.LoadScene(1);
    }
}
