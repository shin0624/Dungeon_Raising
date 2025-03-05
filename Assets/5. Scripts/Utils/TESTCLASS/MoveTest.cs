using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{

    // Animator parent = unitroot

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb2D;
    private GameObject testPlayer;
    private string animParameter = "";
    private float moveSpeed = 5.0f;
    private Vector3 moveVector;


    void Start()
    {
        testPlayer = gameObject;
        anim ??= testPlayer.GetComponentInChildren<Animator>();
        rb2D ??= testPlayer.GetComponent<Rigidbody2D>();
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
    }
    void FixedUpdate()
    {
        if(moveVector.magnitude > 0.1f)
        {
            moveVector = moveVector.normalized;
            rb2D.velocity = moveVector * moveSpeed;
            StartCoroutine(UnitMove());
        }

    }

    private IEnumerator UnitMove()
    {
        TestSetBool("1_Move");
        yield return null;

    }

    private IEnumerator UnitAttack()
    {
        TestSetTrigger("2_Attack");
        yield return null;

    }

    // Bool : 1_Move, 5_Debuff, isDeath, 7_Skill01, 8_Skill02, 9_Skill03
    // Trigger : 2_Attack, 3_Damage, 4_Death, 6_Ohter

    private void TestSetBool(string param)
    {
        if(animParameter ==param)return;

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
        if(animParameter == param) return;

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
