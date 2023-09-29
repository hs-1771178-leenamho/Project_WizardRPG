using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] Canvas playerUI;
    [SerializeField] Canvas pauseUI;
    [SerializeField] GameObject saveAlertCanvas;

    PlayerRaycast playerRaycast;

    // Start is called before the first frame update
    void Start()
    {
        playerRaycast = FindObjectOfType<PlayerRaycast>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseGame();
        }
    }

    void PauseGame(){
        playerUI.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(true);
        if(saveAlertCanvas!= null) saveAlertCanvas.SetActive(false);

        Cursor.visible = true;
        playerRaycast.MovementDisable();
        Time.timeScale = 0f;

    }

    public void ResumeGame(){
        playerUI.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(false);
        if(saveAlertCanvas!= null) saveAlertCanvas.SetActive(false);

        Cursor.visible = false;
        playerRaycast.MovementAble();
        Time.timeScale = 1f;
    }

    public void SaveGame(){
        // 게임 데이터 세이브 코드 추가 해야함
        PlayerInfo.instance.SaveData();
        saveAlertCanvas.SetActive(true);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void GoBackToTown(){
        Cursor.visible = false;
        playerRaycast.MovementAble();
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
