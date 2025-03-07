
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatAnimatorController : MonoBehaviour
{
    //���� �� ���ֵ��� ���� �ִϸ��̼��� �����ϴ� Ŭ����. 
    // �ִϸ��̼� Ŭ���� �Ķ���ʹ� bool, Trigger �� Ÿ���� ����.
    // Bool : 1_Move, 5_Debuff, isDeath, 7_Skill01, 8_Skill02, 9_Skill03
    // Trigger : 2_Attack, 3_Damage, 4_Death, 6_Ohter
    //isDeath���´� �ֿ켱���� ó���Ǿ�� ��. AnyState���� isDeath ==true���Ǹ� ��� ��ȯ�ǵ���.
    //��ġ�� ���� ���� - SinglePlayScene ���� �� IDLE -> UnitMoveController.cs�� FindTargetAndMove()�� ȣ��� �� Move
    // -> �⺻ ���� 2_Attack -> ���� 7_Skill01 ~ 03�� ���ʷ� ���. -> ��ų���� ��Ÿ���� ����. ��Ÿ�� �̰�� �� �⺻ ���� �Ǵ� ��Ÿ���� ����� ��ų ���� ���

    private Animator anim;
    private Transform unit;
    private Rigidbody2D rb2D;
    
    private List<string> boolParamList = new List<string> {"1_Move", "5_Defbuff", "7_Skill01", "8_Skill02", "9_Skill03", "isDeath" };//boolŸ�� �Ķ���� ����Ʈ
    private List<String> triggerParamList = new List<string> {"2_Attack", "3_Damage", "4_Death", "6_Other"};//TriggerŸ�� �Ķ���� ����Ʈ.
    private string animParameter = "";

    private UnitDataSender unitDataSender;
    private EnemyDataManager targetEnemyDataManager;
    private BossDataManager targetBossDataManager;
    private HeroDataManager targetHeroDataManager;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name=="SinglePlayScene")
        {
             unitDataSender ??= GameObject.Find("SinglePlaySceneManager").GetComponent<UnitDataSender>();
             Debug.Log("unitDataSender is here!");
        }
        else
        {
            unitDataSender = null;
            Debug.Log("pass");
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded+=OnSceneLoaded;
        unit ??= gameObject.transform;//�� Ŭ������ ������ ������ �ν��Ͻ��� unit���� ����.
        anim ??= unit.GetComponentInChildren<Animator>();// ������ ������Ʈ�� �ڽ� ��ü�� UnitRoot�� Animator�� �����ؾ� ��. 
        rb2D ??= unit.GetComponent<Rigidbody2D>();
       //unitDataSender ??= GameObject.Find("SinglePlaySceneManager").GetComponent<UnitDataSender>();
        
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

    public void TestAutoAttack(Collider2D collision, GameObject targetUnit)
    {
        if(targetUnit==null)
        {
            Debug.LogError("TestAutoAttack : targetUnit�� null�Դϴ�.");
        }
        StartCoroutine(TestSustainableAutoAttack(collision, targetUnit));
    }

    private IEnumerator TestSustainableAutoAttack(Collider2D collision, GameObject targetUnit)
    {
        if(targetUnit==null)
        {
            Debug.LogError("TestSustainableAutoAttack : targetUnit�� null�Դϴ�.");
        }
        SetTargetUnitType(collision, targetUnit);
        yield return new WaitForSeconds(1.0f);
    }

    private float GetThisUnitDamage(float otherDefensePoint)
    {
        float damage = 0.0f;
        float thisAttackPoint = 0.0f;
        float thisAttackSpeed = 0.0f;
        EnemyDataManager enemyData = gameObject.GetComponent<EnemyDataManager>();
        BossDataManager bossData = gameObject.GetComponent<BossDataManager>();
        HeroDataManager heroData = gameObject.GetComponent<HeroDataManager>();
        if (enemyData != null)
        {
            thisAttackPoint = enemyData.enemyInformation.attackPoint;
            thisAttackSpeed = enemyData.enemyInformation.attackSpeed;
        }
        else if (bossData != null)
        {
            thisAttackPoint = bossData.bossInformation.attackPoint;
            thisAttackSpeed = bossData.bossInformation.attackSpeed;
        }
        else if(heroData!=null)
        {
            thisAttackPoint = heroData.heroInformation.attackPoint;
            thisAttackSpeed = heroData.heroInformation.attackSpeed;
        }
        else
        {
            thisAttackPoint = 20.0f;
            thisAttackSpeed = 1.5f;
        }
        Debug.Log($"thisAttackPoint = {thisAttackPoint}, thisAttackSpeed = {thisAttackSpeed}, otherdefensePoint = {otherDefensePoint}");
        
        try{
            damage = unitDataSender.CalculateDamage(thisAttackPoint, thisAttackSpeed, otherDefensePoint);         
            Debug.Log($"damage = {damage}");
        }
        catch(Exception e){      
            Debug.LogError($"CalculateDamage���� ���� �߻�: {e.Message}\n{e.StackTrace}");
        }
        
        
        return damage;
    }

    private void SetTargetUnitType(Collider2D collision, GameObject targetUnit)
    {   
        if (targetUnit == null)
        {
            Debug.LogError("SetTargetUnitType: targetUnit�� NULL�Դϴ�!");
            return;
        }

        float testDamage = 0.0f;
        EnemyDataManager enemyData = targetUnit.GetComponent<EnemyDataManager>();
        BossDataManager bossData = targetUnit.GetComponent<BossDataManager>();
        HeroDataManager heroData = targetUnit.GetComponent<HeroDataManager>();
        
        if(enemyData!=null)
        {
             //targetEnemyDataManager = unitDataSender.GetUnitType<EnemyDataManager>(targetUnit.gameObject);
             //targetEnemyDataManager = enemyData;
             testDamage = GetThisUnitDamage(enemyData.enemyInformation.defensePoint);
             SustainableAutoAttack(collision, enemyData.enemyInformation.healthPoint, testDamage);

        }
        else if(bossData!=null)
        {
            //targetBossDataManager = unitDataSender.GetUnitType<BossDataManager>(targetUnit.gameObject);
            //targetBossDataManager = bossData;
            testDamage = GetThisUnitDamage(bossData.bossInformation.defensePoint);
            SustainableAutoAttack(collision, bossData.bossInformation.healthPoint, testDamage);

        }
        else if(heroData!=null)
        {
            //targetHeroDataManager = unitDataSender.GetUnitType<HeroDataManager>(targetUnit.gameObject);
            //targetHeroDataManager = heroData;
            testDamage = GetThisUnitDamage(heroData.heroInformation.defensePoint);
            SustainableAutoAttack(collision, heroData.heroInformation.healthPoint, testDamage);

        }
        else//�÷��̾� ������ ��� : ���� �÷��̾� ������ �Ŵ����� �غ���� ����.
        {   
            testDamage = GetThisUnitDamage(150.0f);
            SustainableAutoAttack(collision, 100.0f, testDamage);
            Debug.Log("Target Unit is PlayerCharacter.");
        }
    }

    private IEnumerator SustainableAutoAttack(Collider2D collision, float hp, float damage)//�ڵ� ���� ���� �� ���� ���ó���� ���� �ӽ� �޼���.
    {
        while(hp > 0)
        {
            yield return new WaitForSeconds(1.0f);
            hp-=damage;
            Debug.Log($"{gameObject.name} HP : {hp}");
            if(hp <=0)
            {
                Destroy(gameObject);
                yield break;
            }
        }
    }

    private IEnumerator UnitAttack()//���� �ڷ�ƾ �޼���.
    {
        SetState("2_Attack");
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
