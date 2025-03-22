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
    private float attackPoint = 30.0f;
    private float attackSpeed = 1.5f;
    private float healthPoint = 300.0f;
    private float otherDefensePoint;
    private float otherHealthPoint;
    private float damage;
    public float currentHP = 100.0f;
    private GameObject closestUnit;
    private HeroDataManager heroDataManager;//gameObject�� Hero�� ��� ���.
    private SoldierDataManager soldierDataManager;//gameObject�� Soldier�� ��� ���.
    private EnemyDataManager enemyDataManager;//gameObject�� Enemy�� ��� ���.
    private BossDataManager bossDataManager;//gameObject�� Boss�� ��� ���.
    private UnitMoveController unitMoveController;//gameObject�� �÷��̾� ������ ��� ���.
    private EnemyMoveController enemyMoveController;//gameObject�� ���ʹ� ������ ��� ���.
    private HealthBarController healthBarController;
    

    private void Awake()
    {   
        SceneCheck();
    }

    private void Start()
    {
        healthBarController = GetComponentInChildren<HealthBarController>();
        FindDamageValue();
    }

    private void SceneCheck()//�׽�Ʈ �� �Ǵ� �̱��÷��� �� �̿ܿ� �������� DamageCalulater�� ��Ȱ��ȭ�Ѵ�.
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch(sceneName)
        {
            case "SinglePlayScene":
            case "PlayTest":
                enabled = true;
                break;

            default:
                enabled = false;
                break;
        }
    }

    private void ApplyDamageToTarget()//���ݹ޴� Ÿ���� currentHP�� ���ҽ�Ű�� �޼���.
    {
        if(closestUnit == null) 
        {
             Debug.Log("No target unit found!");
             return;
        }

        DamageCalculater targetDamageCalculater = closestUnit.GetComponent<DamageCalculater>();
        if(targetDamageCalculater != null)
        {
            targetDamageCalculater.currentHP -= damage;//Ÿ���� hp ������Ʈ

            targetDamageCalculater.DecreaseHpBar(targetDamageCalculater.currentHP, otherHealthPoint);//Ÿ���� HPBar ������Ʈ

            if(targetDamageCalculater.currentHP <=0)//Ÿ���� hp�� 0 ���ϰ� �Ǹ� ����.
            {
                Destroy(closestUnit);
                Debug.Log($"{closestUnit.name} is destroyed!");
            }
        }
    }

    private void DecreaseHpBar(float targetCurrnetHP, float targetHP)// HP Bar�� ���� gameObject�� healthPoint, damage�� �����ϴ� �޼���.
    {   
        if(healthBarController!=null)
        {     
            if(currentHP <=0) currentHP = 0;
            healthBarController.SetHealthPoint(targetCurrnetHP, targetHP);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)// �� Ŭ������ ����� ��ü �� �ö��̴� �浹 �� ���� ����.
    {
        if(SceneManager.GetActiveScene().name == "SinglePlayScene" || SceneManager.GetActiveScene().name == "PlayTest")//�̱��÷��� �������� �۵��ϵ��� ����.
        {
            switch(gameObject.tag)
            {
                case "Unit_Player":
                case "Unit_Hero":
                case "Unit_Soldier":
                    if(collision.gameObject.tag == "Unit_Enemy" || collision.gameObject.tag == "Unit_Boss")
                    {
                        Debug.Log($"collision : {collision.gameObject.name}");
                        StartAutoAttack();
                    }
                    break;

                case "Unit_Enemy":
                case "Unit_Boss":
                    if(collision.gameObject.tag == "Unit_Player" || collision.gameObject.tag == "Unit_Hero" || collision.gameObject.tag == "Unit_Soldier")
                    {
                        Debug.Log($"collision : {collision.gameObject.name}");
                        StartAutoAttack();   
                    }
                    break;

                default:
                    break;
            }
            //StartAutoAttack();
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

        //Debug.Log($"[ATTACK] {gameObject.name} �� {closestUnit.name}, HP Left: {otherHealthPoint}");

        if (closestUnit == null)//���� closestUnit�� null�� ��� �ڷ�ƾ ����.
        {
            Debug.LogWarning("No target unit found!");
            yield break;
        }

        damage = CalculateDamage();;

        while(otherHealthPoint > 0)
        {
            yield return new WaitForSeconds(attackSpeed);

            ApplyDamageToTarget();//Ÿ���� ü�� ���� ����.

            if(closestUnit == null) yield break;//Ÿ���� ������� ��� �ڷ�ƾ ����.
        }
    }

    private void FindDamageValue()//gameObject�� �±׿� ���� �� ������ ���ݷ°� ���ݼӵ��� ã�´�. + ü�µ� ã�´�.
    { 
        attackPoint = 30.0f;//null ���� ������ ���� �⺻���� �Ҵ��Ѵ�.
        attackSpeed = 1.5f;
        healthPoint = 300.0f;

        switch(gameObject.tag)
        {
            case "Unit_Player" :
                attackPoint = 30.0f;
                attackSpeed = 1.5f;
                healthPoint = 300.0f;
                break;

            case "Unit_Hero" :
                //heroDataManager = gameObject.GetComponent<HeroDataManager>();
                if (gameObject.GetComponent<HeroDataManager>())//gameObject�� HeroDataManager�� ������ ���� ���
                {
                    attackPoint = gameObject.GetComponent<HeroDataManager>().heroInformation.attackPoint;
                    attackSpeed = gameObject.GetComponent<HeroDataManager>().heroInformation.attackSpeed;
                    healthPoint = gameObject.GetComponent<HeroDataManager>().heroInformation.healthPoint;
                }
                break;

            case "Unit_Soldier" :
                //soldierDataManager = gameObject.GetComponent<SoldierDataManager>();
                if (gameObject.GetComponent<SoldierDataManager>())
                {
                    attackPoint = gameObject.GetComponent<SoldierDataManager>().soldierInformation.attackPoint;
                    attackSpeed = gameObject.GetComponent<SoldierDataManager>().soldierInformation.attackSpeed;
                    healthPoint = gameObject.GetComponent<SoldierDataManager>().soldierInformation.healthPoint;
                }
                break;

            case "Unit_Enemy" :
                //enemyDataManager = gameObject.GetComponent<EnemyDataManager>();
                if (gameObject.GetComponent<EnemyDataManager>())
                {
                    attackPoint = gameObject.GetComponent<EnemyDataManager>().enemyInformation.attackPoint;
                    attackSpeed = gameObject.GetComponent<EnemyDataManager>().enemyInformation.attackSpeed;
                    healthPoint = gameObject.GetComponent<EnemyDataManager>().enemyInformation.healthPoint;
                }
                break;

            case "Unit_Boss" :
                //bossDataManager = gameObject.GetComponent<BossDataManager>();
                if (gameObject.GetComponent<BossDataManager>())
                {
                    attackPoint = gameObject.GetComponent<BossDataManager>().bossInformation.attackPoint;
                    attackSpeed = gameObject.GetComponent<BossDataManager>().bossInformation.attackSpeed;
                    healthPoint = gameObject.GetComponent<BossDataManager>().bossInformation.healthPoint;
                }
                break;

            default : 
                Debug.LogError($"[ERROR] Unknown unit tag: {gameObject.tag}");
                break;
        }
        currentHP = healthPoint;
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
                if (closestUnit.GetComponent<HeroDataManager>())
                {
                    otherDefensePoint = closestUnit.GetComponent<HeroDataManager>().heroInformation.defensePoint;
                    otherHealthPoint = closestUnit.GetComponent<HeroDataManager>().heroInformation.healthPoint;
                    
                }
                break;

            case "Unit_Soldier" ://��� ������ ������ ���
               if (closestUnit.GetComponent<SoldierDataManager>())
                {
                    otherDefensePoint = closestUnit.GetComponent<SoldierDataManager>().soldierInformation.defensePoint;
                    otherHealthPoint = closestUnit.GetComponent<SoldierDataManager>().soldierInformation.healthPoint;
                }
                break;

            case "Unit_Enemy" ://��� ������ ���ʹ��� ���
                if (closestUnit.GetComponent<EnemyDataManager>())
                {
                    otherDefensePoint = closestUnit.GetComponent<EnemyDataManager>().enemyInformation.defensePoint;
                    otherHealthPoint = closestUnit.GetComponent<EnemyDataManager>().enemyInformation.healthPoint;
                }
                break;

            case "Unit_Boss" ://��� ������ ������ ���
                if (closestUnit.GetComponent<BossDataManager>())
                {
                    otherDefensePoint = closestUnit.GetComponent<BossDataManager>().bossInformation.defensePoint;
                    otherHealthPoint = closestUnit.GetComponent<BossDataManager>().bossInformation.healthPoint;
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
                //��, gameObject�� �÷��̾�� �����̸� closestUnit�� enemy �Ǵ� boss�� ������ ��.

            case "Unit_Boss":
            case "Unit_Enemy":
                enemyMoveController = gameObject.GetComponent<EnemyMoveController>();
                return enemyMoveController ? enemyMoveController.FindClosestUnit() : null;//unitMoveController���� ã�� ���� ����� ������ ����.
                //��, gameObject�� ���ʹ� �����̸� closestUnit�� player �Ǵ� hero �Ǵ� solider�� ������ ��.

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

        float rawDamage = attackPoint * (100.0f / (100.0f + otherDefensePoint));//gameObject�� ���ݷ� * (100 / (100 + ��� �� ����))
        rawDamage = Mathf.Max(rawDamage, 10.0f); //������ �ʹ� ���� ��츦 ����Ͽ�, �ּ� 10�� �������� �����ϵ��� �Ѵ�.
          
        return rawDamage * attackSpeed;//gameObject�� ���ݼӵ��� ���Ͽ� ����.
    }
}
