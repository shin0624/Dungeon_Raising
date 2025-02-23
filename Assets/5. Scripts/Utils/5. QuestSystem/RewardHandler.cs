using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler
{
    //보상 처리를 담당하는 클래스.

    public static void GiveReward(QuestReward reward)
    {
       // PlayerInfo.Instance.AddExperience(reward.experience); 추후 경험치 지급 메서드 생성 후 주석 해제
       //PlayerInfo.Instance.AddCurrency(reward.currency); 추후 재화 지급 메서드 생성 후 주석 해제

       foreach(var item in reward.items)//아이템 지급
       {
            //InventoryManager.Instance.AddItem(item.itemName, itme.amount); 추후 인벤토리 시스템 확립 시 주석 해제
       }

    }
}
