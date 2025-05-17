using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmorLevelup : MonoBehaviour
{
    // �÷��̾��� ��� �������� ���� Ŭ����. ������ �� ���� ��� �������ͽ� ��ȭ�� �����ȴ�.
    // BlackSmithUIController�� PerformCharacterLevelUp()���� ȣ��ȴ�.
    // ������ �� ����� �������ͽ� ���� ������ N ���� ����.

    private int maxLevel = 10;//����� �ִ� ����.
    private int statIncrease = 10;//��� ������ �� �����ϴ� ������ ��ġ.
    private void ArmorLevelup(ArmorItem armorItem)//������ �޼���
    {
        int playerGold = PlayerInfo.Instance.GetplayerGold();//���� �÷��̾� ��ȭ���� �����´�.

        if (playerGold >= armorItem.levelUpCost)//���� �÷��̾� ��ȭ���� ������ ��뺸�� ���ٸ� ����.
        {
            if (armorItem.itemLevel >= maxLevel)//������ �ִ�ġ�� 10������ �����ߴٸ� ������ �Ұ�.
            {
                Debug.Log(armorItem.itemGrade == Grade.BlackStone ? "[DEBUG] ������ �Ұ� : BlackStone ����� ���� �������� �Ұ��մϴ�." : "[DEBUG] ������ �Ұ� : ����� ������ �ִ�ġ�Դϴ�. ���� ������� �±��� �����մϴ�.");
                return;
            }
            else
            {
                PlayerInfo.Instance.SetPlayerGold(PlayerInfo.Instance.GetplayerGold() - armorItem.levelUpCost);// �÷��̾� ��ȭ�� ����
                armorItem.itemLevel += 1;// ������

                switch (armorItem.itemParts)//������ ����(����, ��)�� ���� ���ġ�� �ٸ��Ƿ� switch������ ����.
                {
                    case ItemPart.Weapon:
                        armorItem.offensivePower += statIncrease; // ���ݷ� ���ġ
                        break;
                    default:
                        armorItem.defensivePower += statIncrease; // ���� ���ġ
                        break;
                }
                //TODO: ������ ���� ���� ó�� �ʿ�
            }
        }
        else
        {
            Debug.Log("[DEBUG] ������ �Ұ� : �÷��̾� ��ȭ���� �����մϴ�.");
        }
    }

    private void ArmorGradeUp(ArmorItem armorItem)// �±� �޼���
    {
        int playerGold = PlayerInfo.Instance.GetplayerGold();//���� �÷��̾� ��ȭ���� �����´�.

        if (playerGold < armorItem.gradeUpCost)
        {
            Debug.Log("[DEBUG] �±� �Ұ� : �÷��̾� ��ȭ���� �����մϴ�.");
            return;
        }

        if (armorItem.itemLevel < maxLevel)
        {
            Debug.Log("[DEBUG] �±� �Ұ� : ����� ������ �ִ�ġ�� �ƴմϴ�. ������ �� �±��� �����մϴ�.");
            return;
        }

        if (armorItem.itemGrade == Grade.BlackStone)
        {
            Debug.Log("[DEBUG] �±� �Ұ� : BlackStone ����� ���� �±��� �Ұ��մϴ�.");
            return;
        }

        // �±� ����
        PlayerInfo.Instance.SetPlayerGold(playerGold - armorItem.gradeUpCost);
        armorItem.itemGrade += 1;
            //�±� �� ������ ��������Ʈ ���� �� ���� �ʿ�.
    }


}
