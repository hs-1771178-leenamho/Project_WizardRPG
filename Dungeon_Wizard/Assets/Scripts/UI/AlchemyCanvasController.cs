using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyCanvasController : MonoBehaviour
{
    [SerializeField] Canvas shoppingWindow; // 쇼핑 캔버스
    [SerializeField] AudioClip openShoppingSound;
    [SerializeField] AudioClip closeShoppingSound;
    PlayerRaycast playerRaycast;
    AudioSource shoppingAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        playerRaycast = FindObjectOfType<PlayerRaycast>();   
        shoppingAudioSource = GetComponent<AudioSource>();
    }

    public void StartShopping() // 최초에 E키를 눌러 쇼핑창을 킴
    {
        if (shoppingWindow == null) return;
        shoppingAudioSource.clip = openShoppingSound;
        shoppingAudioSource.Play();
        Cursor.visible = true;
        shoppingWindow.gameObject.SetActive(true);
        
    }


    public void ClickToExit() // 쇼핑창 닫기
    {
        if (shoppingWindow == null) return;
        shoppingAudioSource.clip = closeShoppingSound;
        shoppingAudioSource.Play();
        Cursor.visible = false;
        playerRaycast.MovementAble();
        shoppingWindow.gameObject.SetActive(false);
    }
}
