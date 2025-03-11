
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatAnimatorController : MonoBehaviour
{
    // ���� �� ���ֵ��� ���� �ִϸ��̼��� �����ϴ� Ŭ����. 
    // �ִϸ��̼� Ŭ���� �Ķ���ʹ� bool, Trigger �� Ÿ���� ����.
    // Bool : 1_Move, 5_Debuff, isDeath, 7_Skill01, 8_Skill02, 9_Skill03
    // Trigger : 2_Attack, 3_Damage, 4_Death, 6_Ohter
    // isDeath���´� �ֿ켱���� ó���Ǿ�� ��. AnyState���� isDeath ==true���Ǹ� ��� ��ȯ�ǵ���.
    // ��ġ�� ���� ���� - SinglePlayScene ���� �� IDLE -> UnitMoveController.cs�� FindTargetAndMove()�� ȣ��� �� Move
    // -> �⺻ ���� 2_Attack -> ���� 7_Skill01 ~ 03�� ���ʷ� ���. -> ��ų���� ��Ÿ���� ����. ��Ÿ�� �̰�� �� �⺻ ���� �Ǵ� ��Ÿ���� ����� ��ų ���� ���

    private Animator anim;
    private Transform unit;
    private Rigidbody2D rb2D;
    
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//boolŸ�� �Ķ���� ����Ʈ
    private List<string> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//TriggerŸ�� �Ķ���� ����Ʈ.
    private string animParameter = "";
    

    private void Start()
    {
        unit ??= gameObject.transform;//�� Ŭ������ ������ ������ �ν��Ͻ��� unit���� ����.
        anim ??= unit.GetComponentInChildren<Animator>();// ������ ������Ʈ�� �ڽ� ��ü�� UnitRoot�� Animator�� �����ؾ� ��. 
        rb2D ??= unit.GetComponent<Rigidbody2D>();
    }
    
    public void StopMove()//�̵� ���� �� �ִϸ��̼��� �����ϴ� �޼���.
    {
        anim.SetBool("1_Move", false);
        animParameter = "";
    }

    public void StartMove()
    {
        StartCoroutine(UnitMove());
        Debug.Log("���� �̵� ����");
    }

    public void StartAttack()
    {
        StartCoroutine(UnitAttack());
        Debug.Log("���� ����");
    }

    private IEnumerator UnitAttack()//���� �ڷ�ƾ �޼���.
    {
        SetState("2_Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//�ִϸ��̼� ���̸� ������� ���.
        SetState("7_Skill01");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//�ִϸ��̼� ���̸� ������� ���.
        SetState("8_Skill02");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//�ִϸ��̼� ���̸� ������� ���.
        SetState("9_Skill03");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//�ִϸ��̼� ���̸� ������� ���.
    }

    private IEnumerator UnitMove()//�̵� �ڷ�ƾ �޼���.
    {
        SetState("1_Move");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
    }
  

    public void SetState(string param)//���� ���� �޼���. ȣ�� �� ���� ���� �Ķ���͸� false�� �ʱ�ȭ�Ͽ� �Ķ���� �ߺ��� ���� �� �ش� ���� �Ķ���͸� true�� �Ѵ�.
    {
        if(animParameter == param) return;//���� ���� ȣ�� �� �Լ� ����.
        
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
