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
    
    public static UnitDataSender Instance {get; private set;}// UnitDataSender는 SinglePlayScene에서 항상 하나만 존재하므로, 여러 개의 CombatAnimatorController가 동일한 unitDataSender를 공유하도록 싱글톤 인
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
