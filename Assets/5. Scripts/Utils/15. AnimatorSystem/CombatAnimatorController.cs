using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimatorController : MonoBehaviour
{
    //전투 시 유닛들의 공통 애니메이션을 관리하는 클래스. 
    //Animator의 FSM을 구성하는 애니메이션 클립은 [ IDLE - MOVE - ATTACK - SKILL01 - SKILL02 - SKILL 03 - DEATH ]이며
    //각 트랜지션의 파라미터는 bool타입 [ isMoving, isAttacking, useSkill01, useSkill02, useSkill03, isDead ]이다.
    //DEATH상태는 최우선으로 처리되어야 함. AnyState에서 isDead ==true가되면 즉시 전환되도록.

    [SerializeField] private Animator anim;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
