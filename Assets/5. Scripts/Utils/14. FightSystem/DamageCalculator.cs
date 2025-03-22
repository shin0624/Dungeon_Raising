using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCalculater : MonoBehaviour
{
    // 유닛 간 트리거 충돌 시 공격 데미지 계산을 수행하는 클래스.
    // SetTargetUnitType, GetThisUnitDamage를 분할한 메서드를 만들고, __CombatAnimatorController.cs에서 이들을 호출하게 만든다.

    //__DataManager.cs를 참조하여, 체력, 레벨 등 스테이터스 증감 메서드 등을 작성한다.
    //작성할 메서드 : 체력, 레벨, 스킬 피해량, 공격력, 방어력, 공격 속도, 이동 속도 등 스테이터스 변화가 필요한 기능
    // 1. 공격 기능 : gameObject의 공격력, 공격 속도 <-> 상대 유닛의 체력, 방어력 필요
    private float attackPoint = 30.0f;
    private float attackSpeed = 1.5f;
    private float healthPoint = 300.0f;
    private float otherDefensePoint;
    private float otherHealthPoint;
    private float damage;
    public float currentHP = 100.0f;
    private GameObject closestUnit;
    private HeroDataManager heroDataManager;//gameObject가 Hero일 경우 사용.
    private SoldierDataManager soldierDataManager;//gameObject가 Soldier일 경우 사용.
    private EnemyDataManager enemyDataManager;//gameObject가 Enemy일 경우 사용.
    private BossDataManager bossDataManager;//gameObject가 Boss일 경우 사용.
    private UnitMoveController unitMoveController;//gameObject가 플레이어 유닛일 경우 사용.
    private EnemyMoveController enemyMoveController;//gameObject가 에너미 유닛일 경우 사용.
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

    private void SceneCheck()//테스트 씬 또는 싱글플레이 씬 이외에 씬에서는 DamageCalulater를 비활성화한다.
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

    private void ApplyDamageToTarget()//공격받는 타겟의 currentHP를 감소시키는 메서드.
    {
        if(closestUnit == null) 
        {
             Debug.Log("No target unit found!");
             return;
        }

        DamageCalculater targetDamageCalculater = closestUnit.GetComponent<DamageCalculater>();
        if(targetDamageCalculater != null)
        {
            targetDamageCalculater.currentHP -= damage;//타겟의 hp 업데이트

            targetDamageCalculater.DecreaseHpBar(targetDamageCalculater.currentHP, otherHealthPoint);//타겟의 HPBar 업데이트

            if(targetDamageCalculater.currentHP <=0)//타겟의 hp가 0 이하가 되면 제거.
            {
                Destroy(closestUnit);
                Debug.Log($"{closestUnit.name} is destroyed!");
            }
        }
    }

    private void DecreaseHpBar(float targetCurrnetHP, float targetHP)// HP Bar와 현재 gameObject의 healthPoint, damage를 연동하는 메서드.
    {   
        if(healthBarController!=null)
        {     
            if(currentHP <=0) currentHP = 0;
            healthBarController.SetHealthPoint(targetCurrnetHP, targetHP);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)// 본 클래스가 적용될 객체 간 컬라이더 충돌 시 전투 시작.
    {
        if(SceneManager.GetActiveScene().name == "SinglePlayScene" || SceneManager.GetActiveScene().name == "PlayTest")//싱글플레이 씬에서만 작동하도록 설정.
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

    private IEnumerator RepeatAutoAttack()//자동 전투 진행 및 유닛 사망처리를 위한 메서드.
    {   
        FindDamageValue();
        yield return new WaitForSeconds(0.001f);//closestUnit이 설정되지 않았을 경우를 방지하기 위해 아주 작은 시간동안 딜레이를 준다.

        FindOtherUnitValue();
        yield return new WaitForSeconds(0.001f);

        //Debug.Log($"[ATTACK] {gameObject.name} → {closestUnit.name}, HP Left: {otherHealthPoint}");

        if (closestUnit == null)//만약 closestUnit이 null일 경우 코루틴 종료.
        {
            Debug.LogWarning("No target unit found!");
            yield break;
        }

        damage = CalculateDamage();;

        while(otherHealthPoint > 0)
        {
            yield return new WaitForSeconds(attackSpeed);

            ApplyDamageToTarget();//타겟의 체력 감소 적용.

            if(closestUnit == null) yield break;//타겟이 사망했을 경우 코루틴 종료.
        }
    }

    private void FindDamageValue()//gameObject의 태그에 따라 이 유닛의 공격력과 공격속도를 찾는다. + 체력도 찾는다.
    { 
        attackPoint = 30.0f;//null 오류 방지를 위해 기본값을 할당한다.
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
                if (gameObject.GetComponent<HeroDataManager>())//gameObject가 HeroDataManager를 가지고 있을 경우
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

    private void FindOtherUnitValue()//unitMoveController.cs에서 찾은 가장 가까운 유닛을 "상대 유닛"으로 지정하고, 그의 체력, 방어력을 가져온다. __DataManager가 FindDamageValue()에서 결정되어야 하므로, FindDamageValue()가 실행된 후에 실행한다.
    {
        closestUnit = FindClosestUnit();
        
        if (closestUnit == null)//가장 가까운 유닛에 null이 반환되었을 경우 메서드를 종료한다.
        {
            Debug.LogWarning("No enemy found nearby!");
            return;
        }

        switch(closestUnit.tag)
        {
            case "Unit_Player" ://상대 유닛이 플레이어인 경우.
                otherDefensePoint = 150.0f;
                otherHealthPoint = 300.0f;
                break;

            case "Unit_Hero" ://상대 유닛이 영웅인 경우
                if (closestUnit.GetComponent<HeroDataManager>())
                {
                    otherDefensePoint = closestUnit.GetComponent<HeroDataManager>().heroInformation.defensePoint;
                    otherHealthPoint = closestUnit.GetComponent<HeroDataManager>().heroInformation.healthPoint;
                    
                }
                break;

            case "Unit_Soldier" ://상대 유닛이 병사일 경우
               if (closestUnit.GetComponent<SoldierDataManager>())
                {
                    otherDefensePoint = closestUnit.GetComponent<SoldierDataManager>().soldierInformation.defensePoint;
                    otherHealthPoint = closestUnit.GetComponent<SoldierDataManager>().soldierInformation.healthPoint;
                }
                break;

            case "Unit_Enemy" ://상대 유닛이 에너미일 경우
                if (closestUnit.GetComponent<EnemyDataManager>())
                {
                    otherDefensePoint = closestUnit.GetComponent<EnemyDataManager>().enemyInformation.defensePoint;
                    otherHealthPoint = closestUnit.GetComponent<EnemyDataManager>().enemyInformation.healthPoint;
                }
                break;

            case "Unit_Boss" ://상대 유닛이 보스일 경우
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

    private GameObject FindClosestUnit()//gameObject의 태그에 따라 closestUnit을 어느 컴포넌트에서 가져올 지 결정하는 메서드.
    {
        switch (gameObject.tag)
        {
            case "Unit_Player":
            case "Unit_Hero":
            case "Unit_Soldier":
                unitMoveController = gameObject.GetComponent<UnitMoveController>();
                return unitMoveController ? unitMoveController.FindClosestUnit() : null;//unitMoveController에서 찾은 가장 가까운 유닛을 저장.
                //즉, gameObject가 플레이어블 유닛이면 closestUnit은 enemy 또는 boss를 리턴할 것.

            case "Unit_Boss":
            case "Unit_Enemy":
                enemyMoveController = gameObject.GetComponent<EnemyMoveController>();
                return enemyMoveController ? enemyMoveController.FindClosestUnit() : null;//unitMoveController에서 찾은 가장 가까운 유닛을 저장.
                //즉, gameObject가 에너미 유닛이면 closestUnit은 player 또는 hero 또는 solider를 리턴할 것.

            default:
                return null;
        }
    }

    private float CalculateDamage()//초당 피해량(DPS)를 고려한 공격 시 데미지 계산 메서드.
    {
        /*스테이터스를 보면 [ 공격력 < 방어력 ] 이기 때문에, 공격력으로 발생하는 Damage가 방어력으로 인해 감쇠되고, 이것이 누적되어 체력이 소모되는 구조임. 
		여기에 공격 속도를 추가하여 초당 피해량(DPS)을 고려한 데미지 조정이 필요. 즉, 공격 속도가 빠를 수록 Damage 누적량이 많아짐.
        [  (공격력 * (100 / (100+방어력)) * 공격속도 ] 식을 세워 보면 분모에 방어력을 더하여 공격력에서 방어력만큼 Damage 감소 ->  이 값에 0격 속도를 곱하여 방어력으로 인해 감소된 값을 메꿀 수 있음.
        (ex. 공격력 20, 방어력 100이라면, Damage는 1/2감소된 10이지만, 공격 속도가 1.5이면 Damage = 15, 공격 속도가 3이면 30.
		공격 메서드는 공격 속도에 맞추어 실행되므로, 공격속도가 빠를 수록 데미지 누적량이 빠르게 늘어난다. 즉, 상대 유닛에게 빠르게 데미지를 줄 수 있다.*/

        float rawDamage = attackPoint * (100.0f / (100.0f + otherDefensePoint));//gameObject의 공격력 * (100 / (100 + 상대 의 방어력))
        rawDamage = Mathf.Max(rawDamage, 10.0f); //방어력이 너무 높을 경우를 대비하여, 최소 10의 데미지를 보장하도록 한다.
          
        return rawDamage * attackSpeed;//gameObject의 공격속도를 곱하여 리턴.
    }
}
