using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MagicCoolTime : MonoBehaviour
{
    [SerializeField] Image coolTimeImg; // 쿨타임 연출용 이미지
    [SerializeField] float time_CoolTime; // 각 마법들의 쿨타임(삭제 시간)
    float curTime; // 쿨타임 확인용 시간
    bool isEnable = true; // 종료 여부 확인 변수


    // Update is called once per frame
    void Update()
    {
        if(isEnable) return;
        CheckCoolTime();
    }

    void CheckCoolTime(){
        curTime += Time.deltaTime; // 시간 누적
        if(curTime < time_CoolTime){ // 내가 정한 쿨타임에 다다르지 않으면
            SetCoolTimeImgFill(curTime); // 이미지 fill 갱신
        }
        else if(!isEnable){ // 내가 정한 쿨타임에 다다랐는데 안끝났으면
            EndCoolTime(); //  끝냄
        }
    }

    void EndCoolTime(){
        SetCoolTimeImgFill(time_CoolTime); // 이미지 갱신
        isEnable = true; // 끝내기
    }

    void TriggerSkill(){
        if(!isEnable) return; // 아직 쿨타임이면 안함
        ResetCoolTime(); // 쿨타임 초기화
    }

    void ResetCoolTime(){
        curTime = 0;
        SetCoolTimeImgFill(0);
        isEnable = false;
    }

    void SetCoolTimeImgFill(float _coolTime){
        coolTimeImg.fillAmount = _coolTime / time_CoolTime;

    }

    public void UseSkill(){
        TriggerSkill();
    }
}
