
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CombatAnimatorController : MonoBehaviour
{
    //전투 시 유닛들의 공통 애니메이션을 관리하는 클래스. 
    //Animator의 FSM을 구성하는 애니메이션 클립은 [ IDLE - MOVE - ATTACK - SKILL01 - SKILL02 - SKILL 03 - DEATH ]이며
    //각 트랜지션의 파라미터는 bool타입 [ isMoving, isAttacking, useSkill01, useSkill02, useSkill03, isDead ]이다.
    //DEATH상태는 최우선으로 처리되어야 함. AnyState에서 isDead ==true가되면 즉시 전환되도록.
    //방치형 전투 구현 - SinglePlayScene 진입 시 IDLE -> UnitMoveController.cs의 FindTargetAndMove()가 호출될 때 Move
    // -> 기본 공격 ATTACK -> 이후 SKILL01 ~ 03을 차례로 사용. -> 스킬에는 쿨타임이 존재. 쿨타임 미경과 시 기본 공격 또는 쿨타임이 경과된 스킬 먼저 사용

    private Animator anim;
    private Transform unit;
    private string animParameter = "";
    
    void Start()
    {
        unit ??= gameObject.transform;//본 클래스를 보유한 프리팹 인스턴스를 unit으로 설정.
        anim ??= unit.GetComponent<Animator>();
    }

    private void Update()
    {
        
    }


    private void FSM()
    {
        switch(animParameter)
        {
            case "isMoving" :
                break;

            case "isAttacking" :
                break;

            case "useSkill01" :
                break;

            case "useSkill02" :
                break;

            case "useSkill03" :
                break;

            case "isDead" :
                break;
        }
    }

    private void UnitAttack()
    {

    }

    public void SetState(string param)//상태 변경 메서드. 호출 시 이전 상태 파라미터를 false로 초기화하여 파라미터 중복을 제거 후 해당 상태 파라미터를 true로 한다.
    {
        if(animParameter ==param)
        {
            return;// 현재 상태에서 동일 상태가 호출될 시 함수 종료.
        }
        anim.SetBool("isAttacking", false);
        anim.SetBool("isMoving", false);
        anim.SetBool("useSkill01", false);
        anim.SetBool("useSkill02", false);
        anim.SetBool("useSkill03", false);
        anim.SetBool("isDead", false);

        switch(param)
        {
            case "isMoving" :
                anim.SetBool("isMoving", true);
                break;

            case "isAttacking" :
                anim.SetBool("isAttacking", true);
                break;

            case "useSkill01" :
                anim.SetBool("useSkill01", true);
                break;

            case "useSkill02" :
                anim.SetBool("useSkill02", true);
                break;

            case "useSkill03" :
                anim.SetBool("useSkill03", true);
                break;

            case "isDead" :
                anim.SetBool("isDead", true);
                break;
        }
        
        animParameter = param;
        Debug.Log($"now State = {param + ", " + animParameter}");
        
    }
}
