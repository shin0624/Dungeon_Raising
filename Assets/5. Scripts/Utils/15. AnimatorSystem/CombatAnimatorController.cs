
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

    private IEnumerator UnitAttack()
    {
        if(animParameter != "isAttacking")
        {
            Debug.Log("Parameter Error");
        }
        anim.SetBool("isAttacking", true);
        yield return new WaitForEndOfFrame();
        anim.SetBool("isAttacking", false);
    }

    

    public void SetState(string param)//상태 변경 메서드. 호출 시 이전 상태 파라미터를 false로 초기화하여 파라미터 중복을 제거 후 해당 상태 파라미터를 true로 한다.
    {
        if(animParameter ==param)return;// 현재 상태에서 동일 상태가 호출될 시 함수 종료.
        
        //"현재 상태" <-> "다음 상태" 간 자연스러운 전환을 위해, 상태 변경 시 현재 상태를 유지할 필요가 없을 경우(ex : MOVE상태에서 ATTACK상태로 변경 시 MOVE상태를 유지할 필요가 없음.) false로 설정.
        
        if(param == "isMoving")
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("useSkill01", false);
            anim.SetBool("useSkill02", false);
            anim.SetBool("useSkill03", false);
        }
        else if(param == "isAttacking")//공격과 이동은 동시에 이루어지지 않음. 
        {
            anim.SetBool("isMoving", false);
            StartCoroutine(UnitAttack());
        }
        else if(param.StartsWith("useSkill"))//useSkill로 시작되는 파라미터로 제어되는 상태는 일반 공격과 동시에 이루어지지 않음.
        {
            anim.SetBool("isAttacking", false);
        }
        else if(param == "isDead")
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isMoving", false);
            anim.SetBool("useSkill01", false);
            anim.SetBool("useSkill02", false);
            anim.SetBool("useSkill03", false);
        }
        
        anim.SetBool(param, true);
        animParameter = param;
        Debug.Log($"now State = {param + ", " + animParameter}");
        
    }
}
