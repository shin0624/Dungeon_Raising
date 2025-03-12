
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

    [SerializeField] private GameObject[] skillParticles = new GameObject[3];//유닛 별 스킬 발동 시 활성화되는 파티클 오브젝트.
    private Animator anim;
    private Transform unit;
    private Rigidbody2D rb2D;
    private float coolTime = 0.0f;//쿨타임 변수
    private bool isMoving = false;//유닛 이동 여부 플래그. 공격 시 이동을 멈추어야 함.
    private string animParameter = "";
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//bool타입 파라미터 리스트
    private List<string> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//Trigger타입 파라미터 리스트.  
    private float[] skillCoolTimeArray = new float[3] {2.0f, 2.5f, 3.0f};//유닛 별 스킬 01, 02, 03의 쿨타임. 테스트 성공 시 유닛 스킬 별 쿨타임을 다르게 설정해야 함.
    private float[] remainingCooltime = new float[3];

    

    private void Start()//본 클래스를 부모 오브젝트에서 UnitRoot로 이동.
    {
        unit ??= transform.parent.transform;//본 클래스를 보유한 프리팹 인스턴스를 unit으로 설정.
        anim ??= gameObject.GetComponent<Animator>();// 프리팹 오브젝트의 자식 객체인 UnitRoot의 Animator를 참조해야 함. 
        rb2D ??= unit.GetComponent<Rigidbody2D>();

        SkillParticleInit();
        SkillCooltimeInit();
    }
    
    private void SkillParticleInit()//초기 파티클 오브젝트 비활성화.
    {   
        foreach(GameObject skill in skillParticles)
        {
            if(skill) skill.SetActive(false);
        }
    }

    private void SkillCooltimeInit()//초기 스킬 쿨타임을 0초로 초기화.
    {
        for(int i=0; i < remainingCooltime.Length; i++)
        {
            remainingCooltime[i] = 0.0f;
        }
    }
    //----외부 실행용 메서드----
    
    public void StopMove()//이동 종료 후 애니메이션을 중지하는 메서드.
    {
        anim.SetBool("1_Move", false);
        animParameter = "";
    }

    public void StartMove()
    {
        StartCoroutine(UnitMove());
        //Debug.Log("유닛 이동 시작");
    }

    public void StartAutoBattle()//게임 시작 시 호출.
    {
        StartCoroutine(AutoBattleLoop());
    }

    public void SetSkillParticle(int skillIndex)//인스펙터의 Signal Receiver에서 할당할 메서드.
    {
        StartCoroutine(SetSkillParticleTiming(skillIndex));
    }

    private IEnumerator SetSkillParticleTiming(int skillIndex)//공격, 스킬 등 각 애니메이션 클립에서 공격 타이밍에 맞추어 파티클이 나타나도록 하는 메서드. 애니메이션 클립에서 적절한 프레임에 이벤트를 추가하고, UnitRoot의 Signal Receiver에서 이벤트를 설정한다.
    {
        if(skillParticles[skillIndex] != null && !skillParticles[skillIndex].activeSelf)//스킬 파티클이 존재하고 비활성화 상태일 때 호출.
        {
            skillParticles[skillIndex].SetActive(true);
        }
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);//스킬 애니메이션 클립 길이만큼 대기.
        //( 위의 대기 시간은 추후 파티클 시스템의 Duration으로 바꾸어야 함. )

        skillParticles[skillIndex].SetActive(false);//스킬 애니메이션 종료 시 파티클 비활성화.
    }

    //----유닛 상태 변화 메서드----

    private IEnumerator UnitAttack()//유닛 일반 공격 코루틴 메서드. 스킬 쿨타임 시 실행된다.
    {
        SetState("2_Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        anim.ResetTrigger("2_Attack");
    }

    private IEnumerator UnitMove()//유닛 이동 메서드. (추후 플래그 적용버전으로 수정해야함.)
    {
        isMoving = true;
        SetState("1_Move");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        anim.SetBool("1_Move", false);
        isMoving = false;
    }

    private IEnumerator UnitUseSkill(int skillIndex, string param)//유닛 스킬 사용을 제어하는 메서드.
    {
        if(skillParticles[skillIndex] == null) yield break;//스킬 파티클이 할당되지 않았을 경우 종료.
        SetState(param);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);//애니메이션 클립 길이만큼 대기.
        anim.SetBool(param, false);//스킬 사용 후 바로 파라미터를 false로 초기화하여 중복 실행을 방지.
    }

    private IEnumerator AutoBattleLoop()//자동 전투 메서드. 기본 공격 외에는 모두 쿨타임이 존재하고, 해당 스킬의 쿹타임이 남아있다면 기본 공격을 먼저 실행한다.
    {
        while(true)
        {
            bool skillUsed = false;
            if(!skillUsed)
            {
                StartCoroutine(UnitAttack());//스킬 사용 중이 아니라면 기본 공격 실행.
            }
            for(int i=0; i< skillCoolTimeArray.Length; i++)//쿨타임 배열 길이만큼 반복하며 스킬01 ~ 03을 실행.
            {
                if(remainingCooltime[i] <=0)//쿨타임이 0이라면(쿨타임 경과 시) 실행.
                {
                    StartCoroutine(UnitUseSkill(i, $"{7 + i}_Skill0{i + 1}"));//스킬 파라미터는 bool타입, 7,8,9 Skill01,02,03이므로 해당 파라미터명을 매개변수로 전달.
                    remainingCooltime[i] = skillCoolTimeArray[i];//스킬 실행 후, 잔여 쿨타임 배열(원소 모두 0)을 지정한 쿨타임 배열(flaot형 원소)값으로 다시 초기화.
                    skillUsed = true;//스킬 사용 플래그 true.
                    break;//한 번에 하나의 스킬만 사용하도록 한다.
                }
            }

            for(int i=0; i<remainingCooltime.Length; i++)//스킬 쿨타임 감소 반복문. 잔여 쿨타임 배열의 값을 1초마다 1씩 감소시킨다.
            {
                if(remainingCooltime[i] > 0 )//상단의 첫 for문이 수행되었다면 현재 잔여쿨타임 배열에는 skillCoolTimeArray값이 들어있을 것.
                {
                    remainingCooltime[i] -= 1.0f;
                }
            }
            //Debug.Log($"{skillUsed}");
            yield return new WaitForSeconds(1.0f);//1초마다 반복.
        }
    }
  

    private void SetState(string param)//상태 변경 메서드. 호출 시 이전 상태 파라미터를 false로 초기화하여 파라미터 중복을 제거 후 해당 상태 파라미터를 true로 한다.
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
