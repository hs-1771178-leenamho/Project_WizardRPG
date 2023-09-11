using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotelCanvasController : MonoBehaviour
{
    [SerializeField] GameObject motelCanvas;
    [SerializeField] AudioClip openMotelSound;
    [SerializeField] AudioClip closeMotelSound;
    PlayerStat playerStat;
    PlayerRaycast playerRaycast;
    AudioSource motelAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        playerRaycast = FindObjectOfType<PlayerRaycast>();   
        motelAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMotel(){
        if(motelCanvas == null) return;
        motelAudioSource.clip = openMotelSound;
        motelAudioSource.Play();
        Cursor.visible = true;
        motelCanvas.gameObject.SetActive(true);
    }

    public void HealAndExitMotel(){
        if(motelCanvas == null) return;
        motelAudioSource.clip = closeMotelSound;
        motelAudioSource.Play();
        playerStat.Heal(100f);
        Cursor.visible = false;
        playerRaycast.MovementAble();
        motelCanvas.gameObject.SetActive(false);
    }
}
