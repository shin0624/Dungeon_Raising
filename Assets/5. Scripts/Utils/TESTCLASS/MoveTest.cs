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
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//boolŸ�� �Ķ���� ����Ʈ
    private List<string> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//TriggerŸ�� �Ķ���� ����Ʈ.
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

    private IEnumerator AutoBattleLoop()//�⺻ ���� �ܿ��� ��� ��Ÿ���� �����ϰ�, �ش� ��ų�� �lŸ���� �����ִٸ� �⺻ ������ ���� �����Ѵ�.
    {
        while(true)
        {
            bool skillUsed = false;
            if(!skillUsed)//��ų�� ������� �ʾҴٸ� �⺻ ���� ����
            {
                StartCoroutine(UnitAttack());
                Debug.Log("�⺻ ���� ����");
            }

            for(int i=0; i <skillCoolTimeArray.Length; i++)
            {
                if(remainingCooltime[i] <= 0)//��ų�� ��� �����ϸ� ����
                {
                    StartCoroutine(UnitUseSkill(i, $"{7+i}_Skill0{i+1}"));
                    Debug.Log($"Skill {i+1} ���� ��");
                    remainingCooltime[i] = skillCoolTimeArray[i];//��Ÿ�� �ʱ�ȭ. ������ �����صξ��� ��Ÿ�� �迭�� ���Ұ��� �ܿ� ��Ÿ�� �迭�� ���ҷ� �ִ´�.
                    skillUsed = true;
                    break;//�� ���� �ϳ��� ��ų�� ��� �����ϵ��� ��.
                }
            }



            for(int i=0; i < remainingCooltime.Length; i++)//��ų ��Ÿ�� ���� ���� 
            {
                if(remainingCooltime[i] > 0)
                {
                    remainingCooltime[i]-=1.0f;//1�ʸ��� ����
                }
            }
            Debug.Log($"{skillUsed}");
            yield return new WaitForSeconds(1.0f);//1�ʸ��� �ݺ�
        }

    }

    public IEnumerator SetSkillParticleTiming(int skillIndex)// ����, ��ų �� �� �ִϸ��̼� Ŭ������ ���� Ÿ�ֿ̹� ���߾� ��ƼŬ�� ��Ÿ������ �ϴ� �޼���. �ִϸ��̼� Ŭ������ ������ �����ӿ� �̺�Ʈ�� �߰��ϰ�, UnitRoot�� Signal Receiver���� �̺�Ʈ�� �����Ѵ�.
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
