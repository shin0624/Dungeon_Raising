using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLevelUp : MonoBehaviour
{
    [SerializeField] private AdvancementAlert advancementAlert; // �±� ���� �� �˸��� ǥ���ϱ� ���� AdvancementAlert ��ũ��Ʈ ����
    public void PerformItemLevelUp(ArmorItem armorItem)// ������ ������ �޼���
    {
        if (armorItem.itemLevel >= 10) // ������ ������ 10 �̻��̸� ������ �Ұ�
        {
            armorItem.itemGrade = Grade.BlackStone;
            Debug.Log("�������� �ְ����Դϴ�. �������� �Ұ����մϴ�.");
            advancementAlert.AdvancementFailed("�������� �ְ����Դϴ�. �������� �Ұ����մϴ�.");
            return;
        }
        else if (PlayerInfo.Instance.GetplayerGold() < armorItem.levelUpCost) //�÷��̾��� ��ȭ�� ������ �䱸 ��뺸�� ���� ���
        {
            Debug.Log("��ȭ�� �����մϴ�. �������� �Ұ����մϴ�.");
            advancementAlert.AdvancementFailed("��ȭ�� �����մϴ�. �������� �Ұ����մϴ�.");
            return;
        }
        else
        {
            switch (armorItem.itemParts)
            {
                case ItemPart.Weapon:
                    armorItem.offensivePower += 10; // ���� ���ݷ� ���ġ
                    break;
                default:
                    armorItem.defensivePower += 10; // �� ���� ���ġ
                    break;
            }
            armorItem.itemLevel++;
            armorItem.levelUpCost += 10; // ������ ��� ���� 
            PlayerInfo.Instance.SetPlayerGold(PlayerInfo.Instance.GetplayerGold() - armorItem.levelUpCost); // �÷��̾��� ��ȭ ����
            Debug.Log($"������ ������ ����: {armorItem.itemName} - ���� ����: {armorItem.itemLevel}, ���� ��ȭ: {PlayerInfo.Instance.GetplayerGold()}");
        }
    }

    public bool PerformItemAdvancement(ArmorItem armorItem)// ������ �±� �޼���
    {
        if (armorItem.itemGrade == Grade.BlackStone)
        {
            Debug.Log("�������� �ְ����Դϴ�. �±��� �Ұ����մϴ�.");
            advancementAlert.AdvancementFailed("�������� �ְ����Դϴ�. �±��� �Ұ����մϴ�.");
            return false;
        }
        else if (PlayerInfo.Instance.GetplayerGold() < armorItem.gradeUpCost)//�÷��̾��� ��ȭ�� �±� �䱸 ��뺸�� ���� ��� 
        {
            Debug.Log("��ȭ�� �����մϴ�. �±��� �Ұ����մϴ�.");
            advancementAlert.AdvancementFailed("��ȭ�� �����մϴ�. �±��� �Ұ����մϴ�.");
            return false;
        }
        else
        {
            armorItem.itemGrade++;
            armorItem.gradeUpCost += 10; // �±� ��� ����
            PlayerInfo.Instance.SetPlayerGold(PlayerInfo.Instance.GetplayerGold() - armorItem.gradeUpCost); // �÷��̾��� ��ȭ ����
            armorItem.itemLevel = 1; // �±� �� ���� �ʱ�ȭ. �±��ϸ� �ٽ� 1������ ���ư��� ���� �±��� ���� �������� ����.
            Debug.Log($"������ �±� ����: {armorItem.itemName} - ���� ���: {armorItem.itemGrade}, ���� ��ȭ: {PlayerInfo.Instance.GetplayerGold()}");
            return true;
        }
    }

}
