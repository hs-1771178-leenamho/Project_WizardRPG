using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{

    [SerializeField] GameObject cameraRoot;

    Transform cameraRootPos;
    Vector3 originCameraRootPos;

    Vector3 zoomInCameraRootPos;
    // Start is called before the first frame update
    void Start()
    {
        if(cameraRoot == null) return;
        cameraRootPos = cameraRootPos.transform;
        originCameraRootPos = cameraRootPos.position;
        zoomInCameraRootPos = new Vector3(cameraRootPos.position.x, cameraRootPos.position.y, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomIn(){
        if(cameraRoot == null) return;
        cameraRoot.transform.position = zoomInCameraRootPos;
    }

    public void ZoomOut(){
        if(cameraRoot == null) return;
        cameraRoot.transform.position = originCameraRootPos;
    }
}
