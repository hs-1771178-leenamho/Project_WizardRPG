using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2AI : MonoBehaviour
{
    [SerializeField] GameObject monsterBody;
    [SerializeField] GameObject throwingStone;
    [SerializeField] GameObject stoneSpawnPoint;
    [SerializeField] AudioClip deadSound;
    Transform targetTransform;
    NavMeshAgent navMeshAgent;
    Animator bossAnimator;
    Boss2 bossStat;
    PlayerStat playerStat;
    DungeonClear dungeonClear;
    GameObject fired_rock;
    AudioSource audioSource;
    float distanceToTarget = Mathf.Infinity;
    bool isDead = false;
    float initialAngle = 45f;

    void OnEnable()
    {
        targetTransform = FindObjectOfType<HitManager>().gameObject.transform;
        playerStat = FindObjectOfType<PlayerStat>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        bossAnimator = GetComponent<Animator>();
        bossStat = GetComponent<Boss2>();
        dungeonClear = FindObjectOfType<DungeonClear>();
        monsterBody.gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

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
            bossAnimator.SetBool("Attack", false);
            bossAnimator.SetBool("Victory", true);
            return;
        }
        distanceToTarget = Vector3.Distance(targetTransform.position, this.gameObject.transform.position);
        //ProcessChasing();
        ProcessChasing();

        if(fired_rock != null){
            fired_rock.transform.Rotate(new Vector3(1,1,1));
        }

    }

    private void ProcessChasing() // 플레이어 추적 함수
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            ProcessAttack();
        }
    }

    void ChaseTarget()
    {
        navMeshAgent.SetDestination(targetTransform.position);
        bossAnimator.SetBool("Move", true);
        bossAnimator.SetBool("Attack", false);
        
    }

    private void ProcessAttack() // 플레이어 공격 함수
    {
        //navMeshAgent.SetDestination(targetTransform.position);
        bossAnimator.SetBool("Attack", true);
    }

    public void ThrowStone(){
        Vector3 velocity = GetVelocity(stoneSpawnPoint.transform.position, targetTransform.transform.position, initialAngle);
        fired_rock = Instantiate(throwingStone, stoneSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        fired_rock.GetComponent<Rigidbody>().velocity = velocity;

    }

    Vector3 GetVelocity(Vector3 spawnPos, Vector3 target, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 rockTarget = new Vector3(target.x, 0, target.z);
        Vector3 spawnPosition = new Vector3(spawnPos.x, 0, spawnPos.z);

        float distance = Vector3.Distance(rockTarget, spawnPosition);
        float yOffset = spawnPos.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, rockTarget - spawnPosition) * (target.x > spawnPos.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "FireBall"){
            bossAnimator.SetTrigger("GetHit");
            Debug.Log("파이어볼 맞음");
            bossStat.MonsterHitByPlayer(5f);
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
                bossStat.MonsterHitByPlayer(0.08f);
                break;
            case "EarthShatter":
                Debug.Log("지진 맞음");
                bossStat.MonsterHitByPlayer(10f);
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
