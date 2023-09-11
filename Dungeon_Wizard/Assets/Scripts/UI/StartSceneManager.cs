using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] GameObject selectGameCanvas;

    public void StartGameButton(){
        selectGameCanvas.SetActive(true);
    }


    public void Exit(){
        // 게임 종료
        Application.Quit();
    }
}
