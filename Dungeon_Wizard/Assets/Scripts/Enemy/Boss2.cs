using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Monster
{
    // 추후 수정 필요함 아직은 쓸데가 많이 없음
    
    public void AttackPlayerThrowStone(){
        playerHit.ProcessHitAnima(this.Att + 10f);
    }

}
