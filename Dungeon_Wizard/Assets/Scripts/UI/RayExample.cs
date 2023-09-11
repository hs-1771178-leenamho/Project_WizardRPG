using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayExample : MonoBehaviour
{
    float rayRange = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * rayRange, Color.blue, 1f);
    }
}
