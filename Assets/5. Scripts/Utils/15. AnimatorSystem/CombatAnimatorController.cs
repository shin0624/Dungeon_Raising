
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

    [SerializeField] private GameObject[] skillParticles = new GameObject[3];//���� �� ��ų �ߵ� �� Ȱ��ȭ�Ǵ� ��ƼŬ ������Ʈ.
    private Animator anim;
    private Transform unit;
    private Rigidbody2D rb2D;
    private float coolTime = 0.0f;//��Ÿ�� ����
    private bool isMoving = false;//���� �̵� ���� �÷���. ���� �� �̵��� ���߾�� ��.
    private string animParameter = "";
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//boolŸ�� �Ķ���� ����Ʈ
    private List<string> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//TriggerŸ�� �Ķ���� ����Ʈ.  
    private float[] skillCoolTimeArray = new float[3] {2.0f, 2.5f, 3.0f};//���� �� ��ų 01, 02, 03�� ��Ÿ��. �׽�Ʈ ���� �� ���� ��ų �� ��Ÿ���� �ٸ��� �����ؾ� ��.
    private float[] remainingCooltime = new float[3];

    

    private void Start()//�� Ŭ������ �θ� ������Ʈ���� UnitRoot�� �̵�.
    {
        unit ??= transform.parent.transform;//�� Ŭ������ ������ ������ �ν��Ͻ��� unit���� ����.
        anim ??= gameObject.GetComponent<Animator>();// ������ ������Ʈ�� �ڽ� ��ü�� UnitRoot�� Animator�� �����ؾ� ��. 
        rb2D ??= unit.GetComponent<Rigidbody2D>();

        SkillParticleInit();
        SkillCooltimeInit();
    }
    
    private void SkillParticleInit()//�ʱ� ��ƼŬ ������Ʈ ��Ȱ��ȭ.
    {   
        foreach(GameObject skill in skillParticles)
        {
            if(skill) skill.SetActive(false);
        }
    }

    private void SkillCooltimeInit()//�ʱ� ��ų ��Ÿ���� 0�ʷ� �ʱ�ȭ.
    {
        for(int i=0; i < remainingCooltime.Length; i++)
        {
            remainingCooltime[i] = 0.0f;
        }
    }
    //----�ܺ� ����� �޼���----
    
    public void StopMove()//�̵� ���� �� �ִϸ��̼��� �����ϴ� �޼���.
    {
        anim.SetBool("1_Move", false);
        animParameter = "";
    }

    public void StartMove()
    {
        StartCoroutine(UnitMove());
        //Debug.Log("���� �̵� ����");
    }

    public void StartAutoBattle()//���� ���� �� ȣ��.
    {
        StartCoroutine(AutoBattleLoop());
    }

    public void SetSkillParticle(int skillIndex)//�ν������� Signal Receiver���� �Ҵ��� �޼���.
    {
        StartCoroutine(SetSkillParticleTiming(skillIndex));
    }

    private IEnumerator SetSkillParticleTiming(int skillIndex)//����, ��ų �� �� �ִϸ��̼� Ŭ������ ���� Ÿ�ֿ̹� ���߾� ��ƼŬ�� ��Ÿ������ �ϴ� �޼���. �ִϸ��̼� Ŭ������ ������ �����ӿ� �̺�Ʈ�� �߰��ϰ�, UnitRoot�� Signal Receiver���� �̺�Ʈ�� �����Ѵ�.
    {
        if(skillParticles[skillIndex] != null && !skillParticles[skillIndex].activeSelf)//��ų ��ƼŬ�� �����ϰ� ��Ȱ��ȭ ������ �� ȣ��.
        {
            skillParticles[skillIndex].SetActive(true);
        }
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);//��ų �ִϸ��̼� Ŭ�� ���̸�ŭ ���.
        //( ���� ��� �ð��� ���� ��ƼŬ �ý����� Duration���� �ٲپ�� ��. )

        skillParticles[skillIndex].SetActive(false);//��ų �ִϸ��̼� ���� �� ��ƼŬ ��Ȱ��ȭ.
    }

    //----���� ���� ��ȭ �޼���----

    private IEnumerator UnitAttack()//���� �Ϲ� ���� �ڷ�ƾ �޼���. ��ų ��Ÿ�� �� ����ȴ�.
    {
        SetState("2_Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        anim.ResetTrigger("2_Attack");
    }

    private IEnumerator UnitMove()//���� �̵� �޼���. (���� �÷��� ����������� �����ؾ���.)
    {
        isMoving = true;
        SetState("1_Move");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        anim.SetBool("1_Move", false);
        isMoving = false;
    }

    private IEnumerator UnitUseSkill(int skillIndex, string param)//���� ��ų ����� �����ϴ� �޼���.
    {
        if(skillParticles[skillIndex] == null) yield break;//��ų ��ƼŬ�� �Ҵ���� �ʾ��� ��� ����.
        SetState(param);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);//�ִϸ��̼� Ŭ�� ���̸�ŭ ���.
        anim.SetBool(param, false);//��ų ��� �� �ٷ� �Ķ���͸� false�� �ʱ�ȭ�Ͽ� �ߺ� ������ ����.
    }

    private IEnumerator AutoBattleLoop()//�ڵ� ���� �޼���. �⺻ ���� �ܿ��� ��� ��Ÿ���� �����ϰ�, �ش� ��ų�� �lŸ���� �����ִٸ� �⺻ ������ ���� �����Ѵ�.
    {
        while(true)
        {
            bool skillUsed = false;
            if(!skillUsed)
            {
                StartCoroutine(UnitAttack());//��ų ��� ���� �ƴ϶�� �⺻ ���� ����.
            }
            for(int i=0; i< skillCoolTimeArray.Length; i++)//��Ÿ�� �迭 ���̸�ŭ �ݺ��ϸ� ��ų01 ~ 03�� ����.
            {
                if(remainingCooltime[i] <=0)//��Ÿ���� 0�̶��(��Ÿ�� ��� ��) ����.
                {
                    StartCoroutine(UnitUseSkill(i, $"{7 + i}_Skill0{i + 1}"));//��ų �Ķ���ʹ� boolŸ��, 7,8,9 Skill01,02,03�̹Ƿ� �ش� �Ķ���͸��� �Ű������� ����.
                    remainingCooltime[i] = skillCoolTimeArray[i];//��ų ���� ��, �ܿ� ��Ÿ�� �迭(���� ��� 0)�� ������ ��Ÿ�� �迭(flaot�� ����)������ �ٽ� �ʱ�ȭ.
                    skillUsed = true;//��ų ��� �÷��� true.
                    break;//�� ���� �ϳ��� ��ų�� ����ϵ��� �Ѵ�.
                }
            }

            for(int i=0; i<remainingCooltime.Length; i++)//��ų ��Ÿ�� ���� �ݺ���. �ܿ� ��Ÿ�� �迭�� ���� 1�ʸ��� 1�� ���ҽ�Ų��.
            {
                if(remainingCooltime[i] > 0 )//����� ù for���� ����Ǿ��ٸ� ���� �ܿ���Ÿ�� �迭���� skillCoolTimeArray���� ������� ��.
                {
                    remainingCooltime[i] -= 1.0f;
                }
            }
            //Debug.Log($"{skillUsed}");
            yield return new WaitForSeconds(1.0f);//1�ʸ��� �ݺ�.
        }
    }
  

    private void SetState(string param)//���� ���� �޼���. ȣ�� �� ���� ���� �Ķ���͸� false�� �ʱ�ȭ�Ͽ� �Ķ���� �ߺ��� ���� �� �ش� ���� �Ķ���͸� true�� �Ѵ�.
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
        anim.SetBool("1_Move", false);
        //anim.SetBool("5_Debuff", false);
        anim.SetBool("7_Skill01", false);
        anim.SetBool("8_Skill02", false);
        anim.SetBool("9_Skill03", false);
        //anim.SetBool("isDeath", false);

        anim.SetBool(param, true);
    }

    private void TestSetTrigger(string param)
    {
       anim.ResetTrigger("2_Attack");
       //anim.ResetTrigger("3_Damage");
       //anim.ResetTrigger("4_Death");
       //anim.ResetTrigger("6_Ohter");

       anim.SetTrigger(param);
    }

}
