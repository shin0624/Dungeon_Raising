using System.Collections;
using System.Collections.Generic;
using BackEnd.Functions;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    // Animator parent = unitroot

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private GameObject[] skillParticles = new GameObject[3];
    private GameObject testPlayer;
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//bool타입 파라미터 리스트
    private List<string> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//Trigger타입 파라미터 리스트.
    private float[] skillCoolTimeArray = new float[3] {2.0f, 2.5f, 3.0f};
    private float[] remainingCooltime = new float[3];
    private float coolTime = 0.0f;
    private string animParameter = "";
    private float moveSpeed = 5.0f;
    private Vector3 moveVector;
    private bool isMoving = false;
    


    void Start()
    {
        testPlayer = gameObject;
        anim ??= testPlayer.GetComponentInChildren<Animator>();
        rb2D ??= testPlayer.GetComponent<Rigidbody2D>();
        foreach(GameObject skill in skillParticles)
        {
            if(skill) skill.SetActive(false);
        }

        for(int i=0; i < remainingCooltime.Length; i++)
        {
            remainingCooltime[i] = 0.0f;
        }
        StartCoroutine(AutoBattleLoop());

    }

    void Update()
    {
        TestInput();
    }

    private void TestInput()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
        
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartCoroutine(UnitAttack());
        // }
        // if(Input.GetKeyDown(KeyCode.Q))
        // {
        //     StartCoroutine(UnitUseSkill(0, "7_Skill01"));
        // }
        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     StartCoroutine(UnitUseSkill(1, "8_Skill02"));
        // }
        // if(Input.GetKeyDown(KeyCode.E))
        // {
        //     StartCoroutine(UnitUseSkill(2, "9_Skill03"));
        // }
        
    }
    void FixedUpdate()
    {
        if(!isMoving && moveVector.magnitude > 0.1f)
        {

            StartCoroutine(UnitMove());
        }

    }

    private IEnumerator AutoBattleLoop()//기본 공격 외에는 모두 쿨타임이 존재하고, 해당 스킬의 쿹타임이 남아있다면 기본 공격을 먼저 실행한다.
    {
        while(true)
        {
            bool skillUsed = false;
            if(!skillUsed)//스킬을 사용하지 않았다면 기본 공격 실행
            {
                StartCoroutine(UnitAttack());
                Debug.Log("기본 공격 실행");
            }

            for(int i=0; i <skillCoolTimeArray.Length; i++)
            {
                if(remainingCooltime[i] <= 0)//스킬이 사용 가능하면 실행
                {
                    StartCoroutine(UnitUseSkill(i, $"{7+i}_Skill0{i+1}"));
                    Debug.Log($"Skill {i+1} 실행 중");
                    remainingCooltime[i] = skillCoolTimeArray[i];//쿨타임 초기화. 기존에 설정해두었던 쿨타임 배열의 원소값을 잔여 쿨타임 배열의 원소로 넣는다.
                    skillUsed = true;
                    break;//한 번에 하나의 스킬만 사용 가능하도록 함.
                }
            }



            for(int i=0; i < remainingCooltime.Length; i++)//스킬 쿨타임 감소 로직 
            {
                if(remainingCooltime[i] > 0)
                {
                    remainingCooltime[i]-=1.0f;//1초마다 감소
                }
            }
            Debug.Log($"{skillUsed}");
            yield return new WaitForSeconds(1.0f);//1초마다 반복
        }

    }

    public IEnumerator SetSkillParticleTiming(int skillIndex)// 공격, 스킬 등 각 애니메이션 클립에서 공격 타이밍에 맞추어 파티클이 나타나도록 하는 메서드. 애니메이션 클립에서 적절한 프레임에 이벤트를 추가하고, UnitRoot의 Signal Receiver에서 이벤트를 설정한다.
    {
        if(skillParticles[skillIndex]!=null && !skillParticles[skillIndex].activeSelf)
        {
            skillParticles[skillIndex].SetActive(true);

        }     
        yield return new WaitForSeconds(0.5f);
        skillParticles[skillIndex].SetActive(false);    
    }

    private IEnumerator UnitUseSkill(int skillIndex, string param)
    {

        if(skillParticles[skillIndex] == null) yield break;

        SetState(param);

        //skillParticles[skillIndex].SetActive(true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        anim.SetBool(param, false);
        //skillParticles[skillIndex].SetActive(false);
    }

    private IEnumerator UnitMove()
    {
        isMoving = true;
        SetState("1_Move");
        moveVector = moveVector.normalized;
        rb2D.velocity = moveVector * moveSpeed;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        rb2D.velocity = Vector2.zero;
        anim.SetBool("1_Move", false);
        isMoving = false;

    }

    private IEnumerator UnitAttack()
    {
        SetState("2_Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        anim.ResetTrigger("2_Attack");
    }

    // Bool : 1_Move, 5_Debuff, isDeath, 7_Skill01, 8_Skill02, 9_Skill03
    // Trigger : 2_Attack, 3_Damage, 4_Death, 6_Ohter

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
       anim.SetBool("1_Move", false);
       anim.SetBool("5_Debuff", false);
       anim.SetBool("7_Skill01", false);
       anim.SetBool("8_Skill02", false);
       anim.SetBool("9_Skill03", false);
       anim.SetBool("isDeath", false);

       anim.SetBool(param, true);

    }

    private void TestSetTrigger(string param)
    {
       anim.ResetTrigger("2_Attack");
       anim.ResetTrigger("3_Damage");
       anim.ResetTrigger("4_Death");
       anim.ResetTrigger("6_Ohter");

       anim.SetTrigger(param);
    }

}
