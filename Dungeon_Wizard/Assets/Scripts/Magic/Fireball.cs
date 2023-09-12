using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float fireballSpeed = 100f;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject fire;
    
    //MagicType magicType;
    void Start(){
        //this.magicType = MagicType.FireBall;
    }

    void OnEnable() {
        Debug.Log("파이어볼 relative force 생성");
        explosion.SetActive(false);
        fire.SetActive(true);
        this.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * fireballSpeed);
       // Destroy(this.gameObject,2f);
    }

    void OnCollisionEnter(Collision other) {
        fire.SetActive(false);
        explosion.SetActive(true);
        Destroy(this.gameObject,2f);   
        
    }

    
}
