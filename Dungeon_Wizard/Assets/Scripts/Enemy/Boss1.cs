using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1 : Monster
{
    [SerializeField] AudioClip specialAttackSound;


    public void MonsterAttackSpecialPlayer(){
        playerHit.ProcessHitAnima(this.Att + 5f);
        if(specialAttackSound != null){
            monsterAttackSoundSource.clip = specialAttackSound;
            if(!monsterAttackSoundSource.isPlaying) monsterAttackSoundSource.Play();
        }
    }
}
