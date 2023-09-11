using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalButton : MonoBehaviour
{
    [SerializeField] Canvas portalInfoCanvas;
    [SerializeField] Text dungeonInfoText;
    [SerializeField] string dungeonName;
    [SerializeField] int dungeonLevel;

    Vector3 mousePos;
    Vector3 infoPos;

    bool isInfoOpen = false;

    public void MouseEnterOnPortal(){
        Debug.Log("마우스 들어옴");
        mousePos = Input.mousePosition;
        infoPos = new Vector3(mousePos.x + 300, mousePos.y, 0);

        if (!isInfoOpen)
        {
            portalInfoCanvas.gameObject.SetActive(true);
            isInfoOpen = true;
            
        }

        portalInfoCanvas.transform.position = infoPos;
        PortalInfoSetting();
        
    }

    public void MouseExitOnPortal(){
        Debug.Log("마우스 나감");
        if (isInfoOpen)
        {
            portalInfoCanvas.gameObject.SetActive(false);
            isInfoOpen = false;
        }
    }

    void PortalInfoSetting(){
        dungeonInfoText.text = dungeonName + ", 난이도 : " + dungeonLevel;
    }
}
