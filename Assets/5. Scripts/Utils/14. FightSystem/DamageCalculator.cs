using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCalculater : MonoBehaviour
{
    // ���� �� Ʈ���� �浹 �� ���� ������ ����� �����ϴ� Ŭ����.
    // SetTargetUnitType, GetThisUnitDamage�� ������ �޼��带 �����, __CombatAnimatorController.cs���� �̵��� ȣ���ϰ� �����.

    //__DataManager.cs�� �����Ͽ�, ü��, ���� �� �������ͽ� ���� �޼��� ���� �ۼ��Ѵ�.
    //�ۼ��� �޼��� : ü��, ����, ��ų ���ط�, ���ݷ�, ����, ���� �ӵ�, �̵� �ӵ� �� �������ͽ� ��ȭ�� �ʿ��� ���
    // 1. ���� ��� : gameObject�� ���ݷ�, ���� �ӵ� <-> ��� ������ ü��, ���� �ʿ�

    private float attackPoint;
    private float attackSpeed;
    private float otherDefensePoint;
    private float otherHealthPoint;
    private float damage;
    private GameObject closestUnit;
    private HeroDataManager heroDataManager;
    private SoldierDataManager soldierDataManager;
    private EnemyDataManager enemyDataManager;
    private BossDataManager bossDataManager;
    private UnitMoveController unitMoveController;
    private EnemyMoveController enemyMoveController;

    private void Awake()
    {   
        if(SceneManager.GetActiveScene().name!= "SinglePlayScene")
        {
            enabled = false;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)// �� Ŭ������ ����� ��ü �� �ö��̴� �浹 �� ���� ����.
    {
        if(SceneManager.GetActiveScene().name == "SinglePlayScene")
        {
            StartAutoAttack();
            Debug.Log("AutoAttack Started!");
        }
    }

    private void StartAutoAttack()
    {
        StartCoroutine(RepeatAutoAttack());
    }

    private IEnumerator RepeatAutoAttack()//�ڵ� ���� ���� �� ���� ���ó���� ���� �޼���.
    {   
        FindDamageValue();
        yield return new WaitForSeconds(0.001f);//closestUnit�� �������� �ʾ��� ��츦 �����ϱ� ���� ���� ���� �ð����� �����̸� �ش�.

        FindOtherUnitValue();
        yield return new WaitForSeconds(0.001f);

        if (closestUnit == null)//���� closestUnit�� null�� ��� �ڷ�ƾ ����.
        {
            Debug.LogWarning("No target unit found!");
            yield break;
        }

        damage = CalculateDamage();
        while(otherHealthPoint > 0)
        {
            yield return new WaitForSeconds(attackSpeed);

            otherHealthPoint -= damage;  

            if(otherHealthPoint <=0)
            {
                Destroy(closestUnit);
                yield break;
            }
            Debug.Log($"[ATTACK] {gameObject.name} �� {closestUnit.name}, HP Left: {otherHealthPoint}");
        }
    }

    private void FindDamageValue()//gameObject�� �±׿� ���� �� ������ ���ݷ°� ���ݼӵ��� ã�´�.
    { 
        attackPoint = 10f;//null ���� ������ ���� �⺻���� �Ҵ��Ѵ�.
        attackSpeed = 1f;

        switch(gameObject.tag)
        {
            case "Unit_Player" :
                attackPoint = 20.0f;
                attackSpeed = 1.5f;
                break;

            case "Unit_Hero" :
                heroDataManager = gameObject.GetComponent<HeroDataManager>();
                if (heroDataManager)
                {
                    attackPoint = heroDataManager.heroInformation.attackPoint;
                    attackSpeed = heroDataManager.heroInformation.attackSpeed;
                }
                break;

            case "Unit_Soldier" :
                soldierDataManager = gameObject.GetComponent<SoldierDataManager>();
                if (soldierDataManager)
                {
                    attackPoint = soldierDataManager.soldierInformation.attackPoint;
                    attackSpeed = soldierDataManager.soldierInformation.attackSpeed;
                }
                break;

            case "Unit_Enemy" :
                enemyDataManager = gameObject.GetComponent<EnemyDataManager>();
                if (enemyDataManager)
                {
                    attackPoint = enemyDataManager.enemyInformation.attackPoint;
                    attackSpeed = enemyDataManager.enemyInformation.attackSpeed;
                }
                break;

            case "Unit_Boss" :
                bossDataManager = gameObject.GetComponent<BossDataManager>();
                if (bossDataManager)
                {
                    attackPoint = bossDataManager.bossInformation.attackPoint;
                    attackSpeed = bossDataManager.bossInformation.attackSpeed;
                }
                break;

            default : 
                Debug.LogError($"[ERROR] Unknown unit tag: {gameObject.tag}");
                break;
        }
    }

    private void FindOtherUnitValue()//unitMoveController.cs���� ã�� ���� ����� ������ "��� ����"���� �����ϰ�, ���� ü��, ������ �����´�. __DataManager�� FindDamageValue()���� �����Ǿ�� �ϹǷ�, FindDamageValue()�� ����� �Ŀ� �����Ѵ�.
    {
        closestUnit = FindClosestUnit();
        if (closestUnit == null)//���� ����� ���ֿ� null�� ��ȯ�Ǿ��� ��� �޼��带 �����Ѵ�.
        {
            Debug.LogWarning("No enemy found nearby!");
            return;
        }

        switch(closestUnit.tag)
        {
            case "Unit_Player" ://��� ������ �÷��̾��� ���.
                otherDefensePoint = 150.0f;
                otherHealthPoint = 300.0f;
                break;

            case "Unit_Hero" ://��� ������ ������ ���
                if (heroDataManager)
                {
                    otherDefensePoint = heroDataManager.heroInformation.defensePoint;
                    otherHealthPoint = heroDataManager.heroInformation.healthPoint;
                }
                break;

            case "Unit_Soldier" ://��� ������ ������ ���
               if (soldierDataManager)
                {
                    otherDefensePoint = soldierDataManager.soldierInformation.defensePoint;
                    otherHealthPoint = soldierDataManager.soldierInformation.healthPoint;
                }
                break;

            case "Unit_Enemy" ://��� ������ ���ʹ��� ���
                if (enemyDataManager)
                {
                    otherDefensePoint = enemyDataManager.enemyInformation.defensePoint;
                    otherHealthPoint = enemyDataManager.enemyInformation.healthPoint;
                }
                break;

            case "Unit_Boss" ://��� ������ ������ ���
                if (bossDataManager)
                {
                    otherDefensePoint = bossDataManager.bossInformation.defensePoint;
                    otherHealthPoint = bossDataManager.bossInformation.healthPoint;
                }
                break;

            default : 
                Debug.LogError($"[ERROR] Unknown enemy unit tag: {closestUnit.tag}");
                break;
        }
    }

    private GameObject FindClosestUnit()//gameObject�� �±׿� ���� closestUnit�� ��� ������Ʈ���� ������ �� �����ϴ� �޼���.
    {
        switch (gameObject.tag)
        {
            case "Unit_Player":
            case "Unit_Hero":
            case "Unit_Soldier":
                unitMoveController = gameObject.GetComponent<UnitMoveController>();
                return unitMoveController ? unitMoveController.FindClosestUnit() : null;//unitMoveController���� ã�� ���� ����� ������ ����.
            case "Unit_Boss":
            case "Unit_Enemy":
                enemyMoveController = gameObject.GetComponent<EnemyMoveController>();
                return enemyMoveController ? enemyMoveController.FindClosestUnit() : null;//unitMoveController���� ã�� ���� ����� ������ ����.
            default:
                return null;
        }
    }

    private float CalculateDamage()//�ʴ� ���ط�(DPS)�� ����� ���� �� ������ ��� �޼���.
    {
        /*�������ͽ��� ���� [ ���ݷ� < ���� ] �̱� ������, ���ݷ����� �߻��ϴ� Damage�� �������� ���� ����ǰ�, �̰��� �����Ǿ� ü���� �Ҹ�Ǵ� ������. 
		���⿡ ���� �ӵ��� �߰��Ͽ� �ʴ� ���ط�(DPS)�� ����� ������ ������ �ʿ�. ��, ���� �ӵ��� ���� ���� Damage �������� ������.
        [  (���ݷ� * (100 / (100+����)) * ���ݼӵ� ] ���� ���� ���� �и� ������ ���Ͽ� ���ݷ¿��� ���¸�ŭ Damage ���� ->  �� ���� 0�� �ӵ��� ���Ͽ� �������� ���� ���ҵ� ���� �޲� �� ����.
        (ex. ���ݷ� 20, ���� 100�̶��, Damage�� 1/2���ҵ� 10������, ���� �ӵ��� 1.5�̸� Damage = 15, ���� �ӵ��� 3�̸� 30.
		���� �޼���� ���� �ӵ��� ���߾� ����ǹǷ�, ���ݼӵ��� ���� ���� ������ �������� ������ �þ��. ��, ��� ���ֿ��� ������ �������� �� �� �ִ�.*/

        float rawDamage = attackPoint * (100 / (100 + otherDefensePoint));//gameObject�� ���ݷ� * (100 / (100 + ��� �� ����))
        return rawDamage * attackSpeed;//gameObject�� ���ݼӵ��� ���Ͽ� ����.
    }
}
