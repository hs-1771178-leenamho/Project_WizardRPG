using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] GameObject stoneBreakVFX;
    Boss2 boss2;
    // Start is called before the first frame update
    void Start()
    {
        boss2 = FindObjectOfType<Boss2>();
    }

    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            boss2.AttackPlayerThrowStone();
        }
        
        PlayBreakVFX();
    }

    void PlayBreakVFX(){
        Instantiate(stoneBreakVFX, transform.localPosition, Quaternion.identity);       
        Destroy(this.gameObject);
    }


}
