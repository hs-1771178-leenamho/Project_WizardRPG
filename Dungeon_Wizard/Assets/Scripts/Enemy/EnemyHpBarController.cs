using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBarController : MonoBehaviour
{
    [SerializeField] Canvas enemyHpCanvas;
    [SerializeField] Image enemyHpBar;

    [SerializeField] GameObject monsterBody;
    Monster monster;
    float hp;
    float maxHp;

    Transform target;
    Vector3 targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<HitManager>().gameObject.transform;
        monster = monsterBody.GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        targetTransform = new Vector3(target.position.x, target.position.y, target.position.z);
        enemyHpCanvas.transform.LookAt(targetTransform);
        hp = monster.GetMonsterHp();
        maxHp = monster.GetMonsterMaxHp();

        enemyHpBar.fillAmount = hp / maxHp;
        
    }
}
