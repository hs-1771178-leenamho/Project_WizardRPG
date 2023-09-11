using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] protected float MaxHp;
    [SerializeField] protected float Hp = 100f;
    [SerializeField] protected float Att = 10f;
    [SerializeField] protected float Def = 1f;
    [SerializeField] protected float Gold = 3f;
    [SerializeField] protected AudioClip attackSound;
    
    
    protected Vector3 targetTransform;
    protected AudioSource monsterAttackSoundSource;


    protected HitManager playerHit;

    // Start is called before the first frame update
    void Start()
    {
        playerHit = FindObjectOfType<HitManager>();
        monsterAttackSoundSource = this.GetComponent<AudioSource>();
        MaxHp = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    
    public void MonsterAttackPlayer()
    {
        playerHit.ProcessHitAnima(this.Att);
        if(attackSound != null){
            monsterAttackSoundSource.clip = attackSound;
            if(!monsterAttackSoundSource.isPlaying)
                 monsterAttackSoundSource.Play();
        }
    }

    public float GetMonsterHp()
    {
        Debug.Log(this.Hp);
        return this.Hp;
    }

    public void MonsterHitByPlayer(float _dmg)
    {

        this.Hp -= _dmg;

    }

    public float GetMonsterMaxHp(){
        return this.MaxHp;
    }
}
