using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] float rayRange = 5f;
    [SerializeField] Text actionText;

    public LayerMask rayLayer;
    Vector3 rayTransform;
    RaycastHit hitData;
    PortalCanvasControl portalCanvasControl;
    ShoppingCanvasController shopingCanvasController;
    AlchemyCanvasController alchemyCanvasController;
    MotelCanvasController motelCanvasController;
    
    // Start is called before the first frame update
    void Start()
    {
        portalCanvasControl = FindObjectOfType<PortalCanvasControl>();
        shopingCanvasController = FindObjectOfType<ShoppingCanvasController>();
        alchemyCanvasController = FindObjectOfType<AlchemyCanvasController>();
        motelCanvasController = FindObjectOfType<MotelCanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        rayTransform = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
        ShootRay();
    }

    void ShootRay()
    {
        Debug.DrawRay(rayTransform, this.transform.forward * rayRange, Color.blue, 1f);
        if (Physics.Raycast(rayTransform, this.transform.forward, out hitData, rayRange, rayLayer))
        {
            if (portalCanvasControl == null || shopingCanvasController == null)
            {
                Debug.Log("Canavs null");
                return;
            }
            if (hitData.transform.tag == "Portal")
            {
                actionText.gameObject.SetActive(true);
                actionText.text = "[ E ] 포탈 입장하기";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PortalOn();
                    MovementDisable();
                }
            }
            else if(hitData.transform.tag == "Treasure"){
                actionText.gameObject.SetActive(true);
                actionText.text = "[ E ] 열기";
                if(Input.GetKeyDown(KeyCode.E)){
                    Debug.Log("보물상자 열기");
                    MovementDisable();
                }

            }
            else if(hitData.transform.tag == "EquipStore"){
                actionText.gameObject.SetActive(true);
                actionText.text = "[ E ] 대장간 입장하기";
                if(Input.GetKeyDown(KeyCode.E)){
                    EquipStoreOn();
                    MovementDisable();
                }

            }
            else if(hitData.transform.tag == "AlchemyStore"){
                actionText.gameObject.SetActive(true);
                actionText.text = "[ E ] 연금술 상점 입장하기";
                if(Input.GetKeyDown(KeyCode.E)){
                    AlchemyStoreOn();
                    MovementDisable();
                }
            }
            else if(hitData.transform.tag == "Motel"){
                actionText.gameObject.SetActive(true);
                actionText.text = "[ E ] 숙소 입장하기";
                if(Input.GetKeyDown(KeyCode.E)){
                    MotelOn();
                    MovementDisable();
                }
            }
            else actionText.gameObject.SetActive(false);
        }
        else actionText.gameObject.SetActive(false);
    }

    private void PortalOn()
    {
        portalCanvasControl.CanvasOn();
        actionText.gameObject.SetActive(false);
    }

    private void EquipStoreOn(){
        actionText.gameObject.SetActive(false);
        shopingCanvasController.StartShopping();
    }

    private void AlchemyStoreOn(){
        actionText.gameObject.SetActive(false);
        alchemyCanvasController.StartShopping();
    }

    private void MotelOn(){
        actionText.gameObject.SetActive(false);
        motelCanvasController.StartMotel();
    }

    public void MovementDisable(){
        this.GetComponent<StarterAssetsInputs>().cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        this.GetComponent<ThirdPersonController>().enabled = false;   
    }

    public void MovementAble(){
        this.GetComponent<StarterAssetsInputs>().cursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        this.GetComponent<ThirdPersonController>().enabled = true;   
    }
}
