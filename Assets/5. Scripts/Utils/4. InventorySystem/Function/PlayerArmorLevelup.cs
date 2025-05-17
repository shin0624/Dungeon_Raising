using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmorLevelup : MonoBehaviour
{
    // 플레이어의 장비 레벨업을 위한 클래스. 레벨업 시 실제 장비 스테이터스 변화가 연동된다.
    // BlackSmithUIController의 PerformCharacterLevelUp()에서 호출된다.
    // 레벨업 시 장비의 스테이터스 증가 정도는 N 으로 고정.

    private int maxLevel = 10;//장비의 최대 레벨.
    private int statIncrease = 10;//장비 레벨업 시 증가하는 스탯의 수치.
    private void ArmorLevelup(ArmorItem armorItem)//레벨업 메서드
    {
        int playerGold = PlayerInfo.Instance.GetplayerGold();//현재 플레이어 재화량을 가져온다.

        if (playerGold >= armorItem.levelUpCost)//현재 플레이어 재화량이 레벨업 비용보다 많다면 수행.
        {
            if (armorItem.itemLevel >= maxLevel)//레벨업 최대치인 10레벨에 도달했다면 레벨업 불가.
            {
                Debug.Log(armorItem.itemGrade == Grade.BlackStone ? "[DEBUG] 레벨업 불가 : BlackStone 등급의 장비는 레벨업이 불가합니다." : "[DEBUG] 레벨업 불가 : 장비의 레벨이 최대치입니다. 다음 등급으로 승급이 가능합니다.");
                return;
            }
            else
            {
                PlayerInfo.Instance.SetPlayerGold(PlayerInfo.Instance.GetplayerGold() - armorItem.levelUpCost);// 플레이어 재화량 차감
                armorItem.itemLevel += 1;// 레벨업

                switch (armorItem.itemParts)//아이템 부위(무기, 방어구)에 따라 상승치가 다르므로 switch문으로 구분.
                {
                    case ItemPart.Weapon:
                        armorItem.offensivePower += statIncrease; // 공격력 상승치
                        break;
                    default:
                        armorItem.defensivePower += statIncrease; // 방어력 상승치
                        break;
                }
                //TODO: 레벨업 정보 저장 처리 필요
            }
        }
        else
        {
            Debug.Log("[DEBUG] 레벨업 불가 : 플레이어 재화량이 부족합니다.");
        }
    }

    private void ArmorGradeUp(ArmorItem armorItem)// 승급 메서드
    {
        int playerGold = PlayerInfo.Instance.GetplayerGold();//현재 플레이어 재화량을 가져온다.

        if (playerGold < armorItem.gradeUpCost)
        {
            Debug.Log("[DEBUG] 승급 불가 : 플레이어 재화량이 부족합니다.");
            return;
        }

        if (armorItem.itemLevel < maxLevel)
        {
            Debug.Log("[DEBUG] 승급 불가 : 장비의 레벨이 최대치가 아닙니다. 레벨업 후 승급이 가능합니다.");
            return;
        }

        if (armorItem.itemGrade == Grade.BlackStone)
        {
            Debug.Log("[DEBUG] 승급 불가 : BlackStone 등급의 장비는 승급이 불가합니다.");
            return;
        }

        // 승급 수행
        PlayerInfo.Instance.SetPlayerGold(playerGold - armorItem.gradeUpCost);
        armorItem.itemGrade += 1;
            //승급 시 아이템 스프라이트 변경 및 저장 필요.
    }


}
