using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithItemElementClicker : MonoBehaviour
{
    [SerializeField] private Image beforeLevelUpImage;//������ ������ �̹���
    [SerializeField] private Image afterLevelUpImage;//������ ������ �̹���
    [SerializeField] private Image beforeGradeImage;//�±� ������ �̹���
    [SerializeField] private Image afterGradeImage;//�±�  ������ �̹���
    [SerializeField] private TextMeshProUGUI beforeLevel;// [ ���緹��(itemLevel) / �ִ뷹��(10)]
    [SerializeField] private TextMeshProUGUI beforeStatus;// ������ ��� [���ݷ�(offensivePower) n], ���� ��� [����(defensivePower) n]
    [SerializeField] private TextMeshProUGUI afterLevel;// [ ���緹��(itemLevel) + 1 / �ִ뷹��(10)]
    [SerializeField] private TextMeshProUGUI afterStatus; //������ ��� [���ݷ�(offensivePower) + ���ġ n], ���� ��� [����(defensivePower) = ���ġ n]
    [SerializeField] private TextMeshProUGUI levelUpCost;
    [SerializeField] private TextMeshProUGUI gradeUpCost;
    [SerializeField] private GameObject[] reinforcementPanels = new GameObject[2];// ������ �гΰ� �±� �г��� �����ϱ� ���� �迭. [0] : ������ �г�, [1] : �±� �г�
    [SerializeField] private TextMeshProUGUI beforeGrade;
    [SerializeField] private TextMeshProUGUI afterGrade; 
    

    // ���� �� �������ͽ� ���ġ ���� ���� ��ġ ������ �Ϸ�Ǹ� �߰�. 

    public void AddListenerToElementButton(GameObject newItem, ArmorItem armorItem)//������ ����� ĸ��ȭ�Ͽ� BlackSmithItemList.cs�� �ڷ�ƾ�� �ִ´�.
    {
        Button itemElementbutton = newItem.GetComponent<Button>();
        itemElementbutton.onClick.AddListener(() => ElementLink(newItem, armorItem));
    }

    private void ElementLink(GameObject newItem, ArmorItem armorItem)//BlackSmithItemList.cs�� PopulateItemList()���� ����Ʈ�� ��µ� ������ ��ư Ŭ�� �� �ش� ������ ������ RightPivot�� ǥ���ϴ� �޼���
    {   
       //250506 ���� : RightPivot�� LevelUpPanel�� GradeUpPanel�� ������, ������ �гο� �´� ������ ǥ���ϵ��� ����.

        if(reinforcementPanels[0].activeSelf)//������ �г��� Ȱ��ȭ �Ǿ��ִٸ�
        {
            beforeLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            beforeLevel.text = $"Lv {armorItem.itemLevel} / 10";
            afterLevel.text = $"Lv {armorItem.itemLevel + 1} / 10";
            levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.levelUpCost}";
            switch(armorItem.itemParts)
            {
                case ItemPart.Weapon:
                    beforeStatus.text = $"���ݷ� {armorItem.offensivePower}";
                    afterStatus.text = $"���ݷ� {armorItem.offensivePower + 10}";//���ġ�� �ϴ� �ӽ÷� ����.
                    break;
                default:
                    beforeStatus.text = $"���� {armorItem.defensivePower}";
                    afterStatus.text = $"���� {armorItem.defensivePower + 10}";//���ġ�� �ϴ� �ӽ÷� ����.
                    break;
            }
        }
        else if(reinforcementPanels[1].activeSelf)//�±� �г��� Ȱ��ȭ �Ǿ��ִٸ�
        {
            beforeGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.gradeUpCost}";
            beforeGrade.text = armorItem.itemGrade.ToString();
            afterGrade.text = ((Grade)((int)armorItem.itemGrade + 1)).ToString();//enum�� ���������� int������ �����ϹǷ�, itemGrade + 1�� ���ȴ�. ������, ToString()���� text����� ���ؼ��� Ÿ�� ĳ��Ʈ�� �ʿ�.
            if(afterGrade.text == "5")//BlackStone ����� �±��� �Ұ���.
            {
                afterGrade.text = "BlackStone";
                gradeUpCost.text = "�±� �Ұ�";
            }
        }
    }


}
