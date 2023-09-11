using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalMonsterAI : MonoBehaviour
{
    [SerializeField] protected GameObject monsterBody;
    [SerializeField] protected AudioClip deadSound;
    protected Transform targetTransform;
    protected NavMeshAgent navMeshAgent;
    protected Animator monsterAnimator;
    Monster monsterStat;
    protected PlayerStat playerStat;
    protected float distanceToTarget = Mathf.Infinity;
    protected bool isDead = false;
    AudioSource monsterSoundSource;
    float playerAtt;


    // Start is called before the first frame update
    void OnEnable()
    {
        targetTransform = FindObjectOfType<HitManager>().gameObject.transform;
        playerStat = FindObjectOfType<PlayerStat>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();
        monsterStat = GetComponent<Monster>();
        monsterBody.gameObject.SetActive(true);
        monsterSoundSource = GetComponent<AudioSource>();
        playerAtt = playerStat.GetAtt();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            StartCoroutine(Dead());
        }
        if (isDead)
        {
            navMeshAgent.enabled = false;
            return;
        }
        if(playerStat.IsPlayerDead()){
            Debug.Log("플레이어 죽음 확인");
            monsterAnimator.SetBool("Move", false);
            monsterAnimator.SetBool("Attack", false);
            return;
        }
        distanceToTarget = Vector3.Distance(targetTransform.position, this.gameObject.transform.position);
        ProcessChasing();

    }

    private void ProcessChasing() // 플레이어 추적 함수
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(targetTransform.position);
        monsterAnimator.SetBool("Move", true);
        monsterAnimator.SetBool("Attack", false);
    }

    void AttackTarget()
    {
        monsterAnimator.SetBool("Move", false);
        monsterAnimator.SetBool("Attack", true);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "FireBall"){
            monsterAnimator.SetTrigger("GetHit");
            Debug.Log("파이어볼 맞음");
            monsterStat.MonsterHitByPlayer(5f * (playerAtt * 0.2f));
        }

        if (monsterStat.GetMonsterHp() <= 0f)
        {
            StartCoroutine(Dead());
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("마법 맞음");
        monsterAnimator.SetTrigger("GetHit");

        // 플레이어가 데미지를 주는 코드
        switch (other.gameObject.tag)
        { // 마법의 태그에 따라 주는 데미지가 다르게
            // case "FireBall":
            //     Debug.Log("파이어볼 맞음");
            //     monsterStat.MonsterHitByPlayer(5f);
            //     break;
            case "FireStream":
                Debug.Log("불기둥 맞음");
                monsterStat.MonsterHitByPlayer(0.08f * (playerAtt * 0.01f));
                break;
            case "EarthShatter":
                Debug.Log("지진 맞음");
                monsterStat.MonsterHitByPlayer(10f * (playerAtt * 0.5f));
                break;
            default:
                break;
        }
        //monsterStat.MonsterHitByPlayer(other.gameObject.)
        // 만약 몬스터 체력이 0보다 작거나 같다면 
        if (monsterStat.GetMonsterHp() <= 0f)
        {
            StartCoroutine(Dead());
        }

    }

    IEnumerator Dead()
    {
        monsterAnimator.SetTrigger("Dead");

        if(deadSound != null) monsterSoundSource.clip = deadSound;
        monsterSoundSource.Play();
        //플레이어가 골드를 획득하는 코드
        isDead = true;

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);

    }
}
