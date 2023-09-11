using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonFail : MonoBehaviour
{
    [SerializeField] Canvas failCanvas;

    PlayerRaycast playerRaycast;
    PlayerInfo player;
    // Start is called before the first frame update
    void Start()
    {
        playerRaycast = FindObjectOfType<PlayerRaycast>();
        player = FindObjectOfType<PlayerInfo>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fail(){
        StartCoroutine(FailCanvasOn());
    }
    
    IEnumerator FailCanvasOn()
    {
        playerRaycast.MovementDisable();
        yield return new WaitForSeconds(3f);
        Cursor.visible = true;
        failCanvas.gameObject.SetActive(true);
    }

    public void MoveToTown(){
        player.SetInfoHp(1f);
        SceneManager.LoadScene(1);
    }
}
