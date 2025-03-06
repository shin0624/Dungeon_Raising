using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitDataSender : MonoBehaviour
{
    //__DataManager.cs를 참조하여, 체력, 레벨 등 스테이터스 증감 메서드 등을 작성한다. 여기서 작성된 메서드는 CombatAnimatorController.cs에서 사용된다.
    //작성할 메서드 : 체력, 레벨, 스킬 피해량, 공격력, 방어력, 공격 속도, 이동 속도 등 스테이터스 변화가 필요한 기능
    // 1. 공격 기능 : gameObject의 공격력, 공격 속도 <-> 상대 유닛의 체력, 방어력 필요
    // 2.

    public float CalculateDamage(float thisAttackPoint, float thisAttackSpeed, float otherDefensePoint)//초당 피해량(DPS)를 고려한 공격 시 데미지 계산 메서드.
    {
        /*스테이터스를 보면 [ 공격력 < 방어력 ] 이기 때문에, 공격력으로 발생하는 Damage가 방어력으로 인해 감쇠되고, 이것이 누적되어 체력이 소모되는 구조임. 
		여기에 공격 속도를 추가하여 초당 피해량(DPS)을 고려한 데미지 조정이 필요. 즉, 공격 속도가 빠를 수록 Damage 누적량이 많아짐.
        [  (공격력 * (100 / (100+방어력)) * 공격속도 ] 식을 세워 보면 분모에 방어력을 더하여 공격력에서 방어력만큼 Damage 감소 ->  이 값에 0격 속도를 곱하여 방어력으로 인해 감소된 값을 메꿀 수 있음.
        (ex. 공격력 20, 방어력 100이라면, Damage는 1/2감소된 10이지만, 공격 속도가 1.5이면 Damage = 15, 공격 속도가 3이면 30.
		공격 메서드는 공격 속도에 맞추어 실행되므로, 공격속도가 빠를 수록 데미지 누적량이 빠르게 늘어난다. 즉, 상대 유닛에게 빠르게 데미지를 줄 수 있다.*/
        
        float rawDamage = thisAttackPoint * (100 / (100 + otherDefensePoint));//gameObject의 공격력 * (100 / (100 + 상대 의 방어력))
        return rawDamage * thisAttackSpeed;//gameObject의 공격속도를 곱하여 리턴.
    }

    public T GetUnitType<T>(GameObject unit) where T : Component//UnitMoveController에서 targetUnit을 찾은 후 타입을 특정짓기 위한 제네릭 메서드. targetUnit이 Hero인지, Character인지, Soldier인지 알아야 각각의 유닛이 갖는 고유한 정보객체의 스테이터스 값을 연산에 사용할 수 있음.
    {
        if (unit == null)
        {
            Debug.LogError("unit is null");
            return null;
        }

        T dataManager = unit.GetComponent<T>();//제네릭 타입으로 dataManager 컴포넌트를 찾아야 한다. 
        if(dataManager!=null)
        {
            return dataManager;
        }

        Debug.LogError($"{typeof(T).Name} 컴포넌트가 {unit.name}에 존재하지 않음.");
        return null;
        //--> 타 스크립트에서 SoldierDataManager sol = GetUnitType<SoldierDataManager>(unit); 를 선언하고 null이 반환되었다면, 매개변수 unit은 soldier가 아니라는 뜻.
    }


    

}
