using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShatter : MonoBehaviour
{
   // MagicType magicType;

    void Start(){
        //this.magicType = MagicType.EarthShatter;
    }
    void OnEnable(){
        Destroy(this.gameObject, 3f);
    }

}
