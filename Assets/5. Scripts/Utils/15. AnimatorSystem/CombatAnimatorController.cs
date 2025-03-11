
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatAnimatorController : MonoBehaviour
{
    // 전투 시 유닛들의 공통 애니메이션을 관리하는 클래스. 
    // 애니메이션 클립의 파라미터는 bool, Trigger 두 타입이 존재.
    // Bool : 1_Move, 5_Debuff, isDeath, 7_Skill01, 8_Skill02, 9_Skill03
    // Trigger : 2_Attack, 3_Damage, 4_Death, 6_Ohter
    // isDeath상태는 최우선으로 처리되어야 함. AnyState에서 isDeath ==true가되면 즉시 전환되도록.
    // 방치형 전투 구현 - SinglePlayScene 진입 시 IDLE -> UnitMoveController.cs의 FindTargetAndMove()가 호출될 때 Move
    // -> 기본 공격 2_Attack -> 이후 7_Skill01 ~ 03을 차례로 사용. -> 스킬에는 쿨타임이 존재. 쿨타임 미경과 시 기본 공격 또는 쿨타임이 경과된 스킬 먼저 사용

    private Animator anim;
    private Transform unit;
    private Rigidbody2D rb2D;
    
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//bool타입 파라미터 리스트
    private List<string> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//Trigger타입 파라미터 리스트.
    private string animParameter = "";
    

    private void Start()
    {
        unit ??= gameObject.transform;//본 클래스를 보유한 프리팹 인스턴스를 unit으로 설정.
        anim ??= unit.GetComponentInChildren<Animator>();// 프리팹 오브젝트의 자식 객체인 UnitRoot의 Animator를 참조해야 함. 
        rb2D ??= unit.GetComponent<Rigidbody2D>();
    }
    
    public void StopMove()//이동 종료 후 애니메이션을 중지하는 메서드.
    {
        anim.SetBool("1_Move", false);
        animParameter = "";
    }

    public void StartMove()
    {
        StartCoroutine(UnitMove());
        Debug.Log("유닛 이동 시작");
    }

    public void StartAttack()
    {
        StartCoroutine(UnitAttack());
        Debug.Log("유닛 공격");
    }

    private IEnumerator UnitAttack()//공격 코루틴 메서드.
    {
        SetState("2_Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//애니메이션 길이를 기반으로 대기.
        SetState("7_Skill01");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//애니메이션 길이를 기반으로 대기.
        SetState("8_Skill02");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//애니메이션 길이를 기반으로 대기.
        SetState("9_Skill03");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//애니메이션 길이를 기반으로 대기.
    }

    private IEnumerator UnitMove()//이동 코루틴 메서드.
    {
        SetState("1_Move");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
    }
  

    public void SetState(string param)//상태 변경 메서드. 호출 시 이전 상태 파라미터를 false로 초기화하여 파라미터 중복을 제거 후 해당 상태 파라미터를 true로 한다.
    {
        if(animParameter == param) return;//동일 상태 호출 시 함수 종료.
        
        if(boolParamList.Contains(param))
        {
            TestSetBool(param);
        }
        else if(triggerParamList.Contains(param))
        {
            TestSetTrigger(param);
        }
        else
        {
            Debug.LogWarning($"{param} is Invalid Parameter.");
            return;
        }
    }

    private void TestSetBool(string param)
    {
        switch(param)
        {
            case "1_Move" :
            anim.SetBool("7_Skill01", false);
            anim.SetBool("8_Skill02", false);
            anim.SetBool("9_Skill03", false);

            anim.SetBool("1_Move", true);
            break;

            case "7_Skill01" : 
            anim.SetBool("1_Move", false);
            anim.SetBool("8_Skill02", false);
            anim.SetBool("9_Skill03", false);

            anim.SetBool("7_Skill01", true);
            break;

            case "8_Skill02" : 
            anim.SetBool("1_Move", false);
            anim.SetBool("7_Skill01", false);
            anim.SetBool("9_Skill03", false);

            anim.SetBool("8_Skill02", true);
            break;

            case "9_Skill03" : 
            anim.SetBool("1_Move", false);
            anim.SetBool("7_Skill01", false);
            anim.SetBool("8_Skill02", false);

            anim.SetBool("9_Skill03", true);
            break;
        }
        animParameter = param;
    }

    private void TestSetTrigger(string param)
    {
        switch(param)
        {
            case "2_Attack" :
            anim.ResetTrigger("3_Damage");
            anim.ResetTrigger("4_Death");

            anim.SetTrigger("2_Attack");
            break;

            case "3_Damage" :
            anim.ResetTrigger("2_Attack");
            anim.ResetTrigger("4_Death");

            anim.SetTrigger("3_Damage");
            break;

            case "4_Death" :
            anim.ResetTrigger("2_Attack");
            anim.ResetTrigger("3_Damage");
            anim.SetTrigger("4_Death");
            break;
        }

        animParameter = param;
    }

}
