using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : MonoBehaviour
{
    [SerializeField] GameObject bossBody;
    [SerializeField] AudioClip deadSound;
    Transform targetTransform;
    NavMeshAgent navMeshAgent;
    Animator bossAnimator;
    Boss1 bossStat;
    PlayerStat playerStat;
    DungeonClear dungeonClear;
    AudioSource audioSource;
    float distanceToTarget = Mathf.Infinity;
    bool isDead = false;
    float playerAtt;


    // Start is called before the first frame update
    void OnEnable()
    {
        targetTransform = FindObjectOfType<HitManager>().gameObject.transform;
        playerStat = FindObjectOfType<PlayerStat>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        bossAnimator = GetComponent<Animator>();
        bossStat = GetComponent<Boss1>();
        dungeonClear = FindObjectOfType<DungeonClear>();
        bossBody.gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
        playerAtt = playerStat.GetAtt();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Dead());
        }
        if (isDead)
        {
            navMeshAgent.enabled = false;
            return;
        }
        if (playerStat.IsPlayerDead())
        {
            Debug.Log("플레이어 죽음 확인");
            bossAnimator.SetBool("Run", false);
            bossAnimator.SetBool("Attack_Normal", false);
            bossAnimator.SetBool("Attack_Special", false);
            bossAnimator.SetBool("Victory", true);
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
        bossAnimator.SetBool("Run", true);
        bossAnimator.SetBool("Attack_Normal", false);
        bossAnimator.SetBool("Attack_Special", false);
    }

    void AttackTarget()
    {
        bossAnimator.SetBool("Run", false);
        // 난수를 만들어 공격 패턴 둘중 하나가 나가게 만든다.
        int attPattern = Random.Range(0, 10);
        if (attPattern <= 6)
        {
            AttackNormal();
        }
        else
        {
            AttackSpecial();
        }
    }

    void AttackNormal()
    {
        bossAnimator.SetBool("Attack_Special", false);
        bossAnimator.SetBool("Attack_Normal", true);
    }

    void AttackSpecial()
    {
        bossAnimator.SetBool("Attack_Normal", false);
        bossAnimator.SetBool("Attack_Special", true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "FireBall")
        {
            bossAnimator.SetTrigger("GetHit");
            Debug.Log("파이어볼 맞음");
            bossStat.MonsterHitByPlayer(5f * (playerAtt * 0.2f));
        }

        if (bossStat.GetMonsterHp() <= 0f)
        {
            StartCoroutine(Dead());
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("마법 맞음");
        bossAnimator.SetTrigger("GetHit");

        // 플레이어가 데미지를 주는 코드
        switch (other.gameObject.tag)
        { // 마법의 태그에 따라 주는 데미지가 다르게
            // case "FireBall":
            //     Debug.Log("파이어볼 맞음");
            //     bossStat.MonsterHitByPlayer(5f);
            //     break;
            case "FireStream":
                Debug.Log("불기둥 맞음");
                bossStat.MonsterHitByPlayer(0.08f * (playerAtt * 0.01f));
                break;
            case "EarthShatter":
                Debug.Log("지진 맞음");
                bossStat.MonsterHitByPlayer(10f * (playerAtt * 0.5f));
                break;
            default:
                break;
        }
        //monsterStat.MonsterHitByPlayer(other.gameObject.)
        // 만약 몬스터 체력이 0보다 작거나 같다면 
        if (bossStat.GetMonsterHp() <= 0f)
        {
            StartCoroutine(Dead());
        }

    }

    IEnumerator Dead()
    {
        bossAnimator.SetTrigger("Dead");
        //플레이어가 골드를 획득하는 코드
        isDead = true;
        dungeonClear.IncreaseClearCount();
        audioSource.clip = deadSound;
        audioSource.Play();

        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);

    }
}
