using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevelUp : MonoBehaviour
{
    public void PerformItemLevelUp(ArmorItem armorItem)// 아이템 레벨업 메서드
    {
        if (armorItem.itemLevel >= 10) // 아이템 레벨이 10 이상이면 레벨업 불가
        {
            armorItem.itemGrade = Grade.BlackStone;
            Debug.Log("아이템이 최고레벨입니다. 레벨업이 불가능합니다.");
            //추후 ui로 알림 표시
            return;
        }
        else if (PlayerInfo.Instance.GetplayerGold() < armorItem.levelUpCost) //플레이어의 재화가 레벨업 요구 비용보다 적을 경우
        {
            Debug.Log("재화가 부족합니다. 레벨업이 불가능합니다.");
            //추후 ui로 알림 표시
            return;
        }
        else
        {
            switch (armorItem.itemParts)
            {
                case ItemPart.Weapon:
                    armorItem.offensivePower += 10; // 무기 공격력 상승치
                    break;
                default:
                    armorItem.defensivePower += 10; // 방어구 방어력 상승치
                    break;
            }
            armorItem.itemLevel++;
            armorItem.levelUpCost += 10; // 레벨업 비용 증가 
            PlayerInfo.Instance.SetPlayerGold(PlayerInfo.Instance.GetplayerGold() - armorItem.levelUpCost); // 플레이어의 재화 차감
            Debug.Log($"아이템 레벨업 성공: {armorItem.itemName} - 현재 레벨: {armorItem.itemLevel}, 현재 재화: {PlayerInfo.Instance.GetplayerGold()}");
        }
    }

    public void PerformItemAdvancement(ArmorItem armorItem)// 아이템 승급 메서드
    {
        if (armorItem.itemGrade == Grade.BlackStone)
        {
            Debug.Log("아이템이 최고등급입니다. 승급이 불가능합니다.");
            //추후 ui로 알림 표시
            return;
        }
        else if (PlayerInfo.Instance.GetplayerGold() < armorItem.gradeUpCost)//플레이어의 재화가 승급 요구 비용보다 적을 경우 
        {
            Debug.Log("재화가 부족합니다. 승급이 불가능합니다.");
            //추후 ui로 알림 표시
            return;
        }
        else
        {
            armorItem.itemGrade++;
            armorItem.gradeUpCost += 10; // 승급 비용 증가
            PlayerInfo.Instance.SetPlayerGold(PlayerInfo.Instance.GetplayerGold() - armorItem.gradeUpCost); // 플레이어의 재화 차감
            Debug.Log($"아이템 승급 성공: {armorItem.itemName} - 현재 등급: {armorItem.itemGrade}, 현재 재화: {PlayerInfo.Instance.GetplayerGold()}");
        }
    }

}
