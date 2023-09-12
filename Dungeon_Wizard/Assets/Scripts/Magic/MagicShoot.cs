using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MagicShoot : MonoBehaviour
{

    [SerializeField] GameObject magicPos;
    [SerializeField] GameObject[] magicArr;
    [SerializeField] Image magic1ButtonImg;
    [SerializeField] Image magic2ButtonImg;
    [SerializeField] Image magic3ButtonImg;
    [SerializeField] MagicCoolTime[] magicCoolTime;

    GameObject fireMagic;
    int magicNum;
    public bool magic2Lock = false;
    public bool magic3Lock = false;
    PlayerInfo playerInfo;
    
    Color originColor = new Color(255f,255f,255f);
    Color selectedColor = new Color(255f,255f,0);
    

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
        //magicCoolTime = FindObjectsOfType<MagicCoolTime>();
        if(playerInfo == null) {
            Debug.Log("으이잉");
            return;
        }
        if (magicArr[0] != null)
        {
            fireMagic = magicArr[0];
            magicNum = 0;
            magic1ButtonImg.color = selectedColor;
        }
        magic2Lock = playerInfo.GetInfoM2Lock(); // DonotDestroyOnLoad에서 가져와야함 
        magic3Lock = playerInfo.GetInfoM3Lock(); // 이것도
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            Debug.Log("파이어볼 장전");
            SwitchMagic(0);
            magicNum = 0;
            magic1ButtonImg.color = selectedColor;
            magic2ButtonImg.color = originColor;
            magic3ButtonImg.color = originColor;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(magic2Lock) return;
            Debug.Log("화염방사 장전");
            SwitchMagic(1);
            magicNum = 1;
            magic1ButtonImg.color = originColor;
            magic2ButtonImg.color = selectedColor;
            magic3ButtonImg.color = originColor;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(magic3Lock) return;
            Debug.Log("지진 장전");
            SwitchMagic(2);
            magicNum = 2;
            magic1ButtonImg.color = originColor;
            magic2ButtonImg.color = originColor;
            magic3ButtonImg.color = selectedColor;
        }
    }

    public void ShootFireBall()
    {
        if (magicPos != null && fireMagic != null )
        {
            if(magicNum == 0)
            { 
                Instantiate(fireMagic, magicPos.transform.position, magicPos.transform.rotation);
                magicCoolTime[0].UseSkill();
            }
            else if(magicNum == 1)
            {
                Instantiate(fireMagic, magicPos.transform.position, magicPos.transform.rotation);
                magicCoolTime[1].UseSkill();
            }
            else
            {
                Instantiate(fireMagic, new Vector3(magicPos.transform.position.x, 0f, magicPos.transform.position.z), magicPos.transform.rotation);
                magicCoolTime[2].UseSkill();
            }
            
        }
    }

    void SwitchMagic(int num)
    {
        if (num < magicArr.Length && magicArr[num] != null)
        {
            fireMagic = magicArr[num];
        }
    }

    public void SetMagic(GameObject magic){
        if(magic.gameObject.tag == "FireBall"){
            magicArr[0] = magic;
        }

        else if(magic.gameObject.tag == "FireStream"){
            magicArr[1] = magic;
        }

        else if(magic.gameObject.tag == "EarthShatter"){
            magicArr[2] = magic;
        }
        else{
            return;
        }
    }

    public void UnlockMagic2(){
        this.magic2Lock = false;
        playerInfo.SetInfoM2Lock(this.magic2Lock);
    }

    public void UnlockMagic3(){
        this.magic3Lock = false;
        playerInfo.SetInfoM3Lock(this.magic3Lock);
    }
}
