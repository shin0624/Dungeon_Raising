using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler
{
    //���� ó���� ����ϴ� Ŭ����.

    public static void GiveReward(QuestReward reward)
    {
       // PlayerInfo.Instance.AddExperience(reward.experience); ���� ����ġ ���� �޼��� ���� �� �ּ� ����
       //PlayerInfo.Instance.AddCurrency(reward.currency); ���� ��ȭ ���� �޼��� ���� �� �ּ� ����

       foreach(var item in reward.items)//������ ����
       {
            //InventoryManager.Instance.AddItem(item.itemName, itme.amount); ���� �κ��丮 �ý��� Ȯ�� �� �ּ� ����
       }

    }
}
