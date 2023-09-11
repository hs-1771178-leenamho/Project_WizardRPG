using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalCanvasControl : MonoBehaviour
{
    [SerializeField] GameObject portalCanvas;
    PlayerRaycast playerRaycast;
    int sceneIdx;

    void Start(){
        playerRaycast = FindObjectOfType<PlayerRaycast>();
        sceneIdx = SceneManager.GetActiveScene().buildIndex;
    }
    public void CanvasOn(){
        Cursor.visible = true;
        portalCanvas.SetActive(true);
    }

    public void CanvasOff(){
        Cursor.visible = false;
        
        playerRaycast.MovementAble();
        portalCanvas.SetActive(false);
    }

    public void JumpToDungeon1(){
        Cursor.visible = false;
        SceneManager.LoadScene(sceneIdx+1);
    }

    public void JumpToDungeon2(){
        Cursor.visible = false;
        SceneManager.LoadScene(sceneIdx+2);
    }

    public void JumpToDungeon3(){
        Cursor.visible = false;
        SceneManager.LoadScene(sceneIdx+3);
    }

    
}
