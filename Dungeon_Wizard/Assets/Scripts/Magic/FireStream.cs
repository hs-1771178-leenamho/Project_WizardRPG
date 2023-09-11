using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStream : MonoBehaviour
{
    MagicType magicType;
    // Start is called before the first frame update
    void Start()
    {
        this.magicType = MagicType.FireStream;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        Destroy(this.gameObject, 3f);
    }
}
