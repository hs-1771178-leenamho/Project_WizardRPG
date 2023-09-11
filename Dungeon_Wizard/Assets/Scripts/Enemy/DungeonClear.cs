using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonClear : MonoBehaviour
{
    [SerializeField] int clearCondition;
    [SerializeField] Canvas clearCanvas;
    [SerializeField] Text rewardGoldText;
    [SerializeField] int rewardGold;
    int clearCount;
    PlayerInfo player;
    PlayerRaycast playerRaycast;
    // Start is called before the first frame update
    void Start()
    {
        clearCount = 0;
        player = FindObjectOfType<PlayerInfo>();
        playerRaycast = FindObjectOfType<PlayerRaycast>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clearCondition == clearCount){
            StartCoroutine(ClearDungeon());
        }
    }

    IEnumerator ClearDungeon(){
        playerRaycast.MovementDisable();
        yield return new WaitForSeconds(3f);
        SetCanvas();
        clearCanvas.gameObject.SetActive(true);
        
    }

    public void IncreaseGold(){
        int gold = player.GetInfoGold();

        player.SetInfoGold(gold + rewardGold);
    }

    public void IncreaseClearCount(){
        this.clearCount++;
    }

    public void MoveToTown(){
        playerRaycast.MovementAble();
        IncreaseGold();
        Cursor.visible = false;
        SceneManager.LoadScene(1);

    }

    void SetCanvas(){
        rewardGoldText.text = "+ " + rewardGold.ToString();
        Cursor.visible = true;
    }
}
