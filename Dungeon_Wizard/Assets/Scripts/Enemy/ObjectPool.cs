using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] monsterPool;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject entrance;

    bool isSpawn = false;
    
    void Update(){
        if(CheckMonsterLive()){
            exit.gameObject.SetActive(false);
            return;
        }
        if(isSpawn){
            exit.gameObject.SetActive(true);
            entrance.GetComponent<BoxCollider>().isTrigger = false;
        }
        
    }
    void SpawnMonster(){
        if(monsterPool == null) return;

        for(int i = 0; i < monsterPool.Length; i++){
            monsterPool[i].SetActive(true);
        }

    }

    bool CheckMonsterLive(){
        for(int i = 0; i < monsterPool.Length; i++){
            if(monsterPool[i].activeSelf){
                Debug.Log("살아있는 몬스터 존재");
                return false;
            }
        }
        Debug.Log("몬스터 다 죽음");
        return true;

    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("통과");
        if(isSpawn) return;
        
        if(other.gameObject.tag == "SpawnTrigger"){
            isSpawn = true;
            SpawnMonster();
        }
    }
    
}
