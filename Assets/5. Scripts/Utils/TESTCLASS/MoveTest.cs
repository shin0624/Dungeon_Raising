using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    // Animator parent = unitroot

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb2D;
    private GameObject testPlayer;
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//bool타입 파라미터 리스트
    private List<string> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//Trigger타입 파라미터 리스트.
    private string animParameter = "";
    private float moveSpeed = 5.0f;
    private Vector3 moveVector;
    [SerializeField] private GameObject[] skillParticles = new GameObject[3];


    void Start()
    {
        testPlayer = gameObject;
        anim ??= testPlayer.GetComponentInChildren<Animator>();
        rb2D ??= testPlayer.GetComponent<Rigidbody2D>();
        skillParticles[0].SetActive(false);
        skillParticles[1].SetActive(false);
        skillParticles[2].SetActive(false);
    }

    void Update()
    {
        TestInput();
    }

    private void TestInput()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(UnitAttack());
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(UnitUseSkill01());
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(UnitUseSkill02());
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(UnitUseSkill03());
        }
    }
    void FixedUpdate()
    {
        if(moveVector.magnitude > 0.1f)
        {

            StartCoroutine(UnitMove());
        }

    }
    private IEnumerator UnitUseSkill01()
    {
        SetState("7_Skill01");
        skillParticles[0].SetActive(true);
       
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("7_Skill01", false);
        skillParticles[0].SetActive(false);
    }
    private IEnumerator UnitUseSkill02()
    {
        SetState("8_Skill02");
        skillParticles[1].SetActive(true);
        
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        skillParticles[1].SetActive(false);
        anim.SetBool("8_Skill02", false);
    }
    private IEnumerator UnitUseSkill03()
    {
        SetState("9_Skill03");
        skillParticles[2].SetActive(true);        
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        skillParticles[2].SetActive(false);      
        anim.SetBool("9_Skill03", false);
    }

    private IEnumerator UnitMove()
    {
        SetState("1_Move");
        moveVector = moveVector.normalized;
        rb2D.velocity = moveVector * moveSpeed;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        moveVector = Vector3.zero;
        rb2D.velocity = Vector2.zero;
        anim.SetBool("1_Move", false);

    }

    private IEnumerator UnitAttack()
    {
        SetState("2_Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

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
