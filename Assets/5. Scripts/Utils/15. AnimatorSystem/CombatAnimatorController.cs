
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CombatAnimatorController : MonoBehaviour
{
    //���� �� ���ֵ��� ���� �ִϸ��̼��� �����ϴ� Ŭ����. 
    //Animator�� FSM�� �����ϴ� �ִϸ��̼� Ŭ���� [ IDLE - MOVE - ATTACK - SKILL01 - SKILL02 - SKILL 03 - DEATH ]�̸�
    //�� Ʈ�������� �Ķ���ʹ� boolŸ�� [ isMoving, isAttacking, useSkill01, useSkill02, useSkill03, isDead ]�̴�.
    //DEATH���´� �ֿ켱���� ó���Ǿ�� ��. AnyState���� isDead ==true���Ǹ� ��� ��ȯ�ǵ���.
    //��ġ�� ���� ���� - SinglePlayScene ���� �� IDLE -> UnitMoveController.cs�� FindTargetAndMove()�� ȣ��� �� Move
    // -> �⺻ ���� ATTACK -> ���� SKILL01 ~ 03�� ���ʷ� ���. -> ��ų���� ��Ÿ���� ����. ��Ÿ�� �̰�� �� �⺻ ���� �Ǵ� ��Ÿ���� ����� ��ų ���� ���

    private Animator anim;
    private Transform unit;
    private string animParameter = "";
    
    void Start()
    {
        unit ??= gameObject.transform;//�� Ŭ������ ������ ������ �ν��Ͻ��� unit���� ����.
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

    public void SetState(string param)//���� ���� �޼���. ȣ�� �� ���� ���� �Ķ���͸� false�� �ʱ�ȭ�Ͽ� �Ķ���� �ߺ��� ���� �� �ش� ���� �Ķ���͸� true�� �Ѵ�.
    {
        if(animParameter ==param)
        {
            return;// ���� ���¿��� ���� ���°� ȣ��� �� �Լ� ����.
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
