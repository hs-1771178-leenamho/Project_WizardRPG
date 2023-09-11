using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 피격 담당 스크립트
public class HitManager : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] AudioClip hitSound;
    AudioSource playerAudioSource;
    PlayerStat playerStat;
    float playerDef;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        playerAudioSource = GetComponent<AudioSource>();
        playerAudioSource.clip = hitSound;
        playerDef = playerStat.GetDef();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessHitAnima(float _dmg){
        playerAnimator.SetTrigger("GetHit");
        if(playerStat == null) return;
        // 몬스터의 dmg를 인자로 넣어야함
        playerAudioSource.Play();
        playerStat.GetDamage(_dmg - (playerDef * 0.1f));
        //playerAnimator.ResetTrigger("GetHit");
        
    }
}
