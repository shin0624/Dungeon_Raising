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
    [SerializeField] private AdvancementAlert advancementAlert;//�±� ���� �� �˸��� ǥ���ϱ� ���� AdvancementAlert ��ũ��Ʈ ����. �±� ���� �� UI�� �˸��� ǥ���ϱ� ���� �ʿ�.

    //250629 : ���� ������ �������� �±��� �����ϴ� ��� �߰��� ���� ItemLevelUp.cs�� armorItem �Ű������� �����ϴ� ���� �߰�
    [SerializeField] private ItemLevelUp itemLevelUp;//������ ������ ��ũ��Ʈ. ������ ������ �� �±��� �����ϴ� �޼��带 ȣ���ϱ� ���� �ʿ�.
    private IBlackSmithManager blackSmithManager;//���尣 �Ŵ��� �������̽� ����(��ȯ ���� ����)
    private ArmorItem currentSelectedItem;//���� ���õ� �������� �����ϱ� ���� ����. ������ �� �±��� ������ �� ���.

    private void Start()
    {
        // �������̽��� ������ �Ŵ��� ã��
        blackSmithManager = FindObjectOfType<BlackSmithUIController>();

        if (blackSmithManager == null)
        {
            Debug.LogError("BlackSmithManager�� ã�� �� �����ϴ�!");
        }
    }
    public void AddListenerToElementButton(GameObject newItem, ArmorItem armorItem)//������ ����� ĸ��ȭ�Ͽ� BlackSmithItemList.cs�� �ڷ�ƾ�� �ִ´�.
    {
        Button itemElementbutton = newItem.GetComponent<Button>();
        itemElementbutton.onClick.AddListener(() => ElementLink(newItem, armorItem));
    }

    private void ElementLink(GameObject newItem, ArmorItem armorItem)//BlackSmithItemList.cs�� PopulateItemList()���� ����Ʈ�� ��µ� ������ ��ư Ŭ�� �� �ش� ������ ������ RightPivot�� ǥ���ϴ� �޼���
    {
        //250506 ���� : RightPivot�� LevelUpPanel�� GradeUpPanel�� ������, ������ �гο� �´� ������ ǥ���ϵ��� ����.
        //250629 ���� : ������ �� �±��� �����ϴ� ��� �߰��� ���� ItemLevelUp.cs�� armorItem �Ű������� �����ϴ� ���� �߰�

        currentSelectedItem = armorItem;//���� ���õ� �������� ����. 
        blackSmithManager?.SetActiveClicker(this);// �Ŵ����� ���� Ȱ�� Ŭ��Ŀ�� ���

        if (reinforcementPanels[0].activeSelf)//������ �г��� Ȱ��ȭ �Ǿ��ִٸ�
        {
            beforeLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            beforeLevel.text = $"Lv {armorItem.itemLevel} / 10";
            afterLevel.text = $"Lv {armorItem.itemLevel + 1} / 10";
            levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.levelUpCost}";
            switch (armorItem.itemParts)
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
        else if (reinforcementPanels[1].activeSelf)//�±� �г��� Ȱ��ȭ �Ǿ��ִٸ�
        {
            beforeGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.gradeUpCost}";
            beforeGrade.text = armorItem.itemGrade.ToString();
            afterGrade.text = ((Grade)((int)armorItem.itemGrade + 1)).ToString();//enum�� ���������� int������ �����ϹǷ�, itemGrade + 1�� ���ȴ�. ������, ToString()���� text����� ���ؼ��� Ÿ�� ĳ��Ʈ�� �ʿ�.
            if (afterGrade.text == "5")//BlackStone ����� �±��� �Ұ���.
            {
                afterGrade.text = "BlackStone";
                gradeUpCost.text = "�±� �Ұ�";
            }
        }
    }

    public void OnItemLevelUp()// ������ ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    {
        if (currentSelectedItem != null)
        {
            if (currentSelectedItem == null)
            {
                Debug.LogWarning("���õ� �������� �����ϴ�.");
                return;
            }

            if (itemLevelUp == null)
            {
                Debug.LogError("ItemLevelUp ������ �������� �ʾҽ��ϴ�!");
                return;
            }
            itemLevelUp.PerformItemLevelUp(currentSelectedItem);// ItemLevelUp�� ������ �޼��� ȣ��
            UpdateLevelUpUI();
        }
    }

    public void OnItemAdvancement()// �±� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    {
        if (currentSelectedItem == null)
        {
            Debug.LogWarning("���õ� �������� �����ϴ�.");
            return;
        }
        if (currentSelectedItem.itemLevel < 10)
        {
            ShowAdvancementFailedUI("������ ������ 10�� �Ǿ�� �±��� �����մϴ�.");
            return;
        }
        if (itemLevelUp == null)
        {
            Debug.LogError("ItemLevelUp ������ �������� �ʾҽ��ϴ�!");
            return;
        }
        bool advancementSuccess = itemLevelUp.PerformItemAdvancement(currentSelectedItem);// ItemLevelUp�� �±� �޼��� ȣ��
        if (advancementSuccess)
        {
            currentSelectedItem.itemLevel = 1; // �±� �� ���� �ʱ�ȭ. �±��ϸ� �ٽ� 1������ ���ư��� ���� �±��� ���� �������� ����.
            Debug.Log($"������ �±� ����: {currentSelectedItem.itemName} - ���� ���: {currentSelectedItem.itemGrade}, ���� ��ȭ: {PlayerInfo.Instance.GetplayerGold()}");
            UpdateAdvancementUI();
            UpdateLevelUpUI();
        }

    }


    // ������ UI ������Ʈ
    private void UpdateLevelUpUI()
    {
        if (reinforcementPanels[0].activeSelf && currentSelectedItem != null)
        {
            beforeLevel.text = $"Lv {currentSelectedItem.itemLevel} / 10";
            afterLevel.text = $"Lv {Mathf.Min(currentSelectedItem.itemLevel + 1, 10)} / 10";
            levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {currentSelectedItem.levelUpCost}";

            switch (currentSelectedItem.itemParts)
            {
                case ItemPart.Weapon:
                    beforeStatus.text = $"���ݷ� {currentSelectedItem.offensivePower}";
                    afterStatus.text = $"���ݷ� {currentSelectedItem.offensivePower + 10}";
                    break;
                default:
                    beforeStatus.text = $"���� {currentSelectedItem.defensivePower}";
                    afterStatus.text = $"���� {currentSelectedItem.defensivePower + 10}";
                    break;
            }
        }
    }

    // �±� UI ������Ʈ
    private void UpdateAdvancementUI()
    {
        if (reinforcementPanels[1].activeSelf && currentSelectedItem != null)
        {
            gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {currentSelectedItem.gradeUpCost}";
            beforeGrade.text = currentSelectedItem.itemGrade.ToString();

            int nextGradeInt = (int)currentSelectedItem.itemGrade + 1;
            if (nextGradeInt >= 4) // BlackStone
            {
                afterGrade.text = "BlackStone";
                gradeUpCost.text = "�±� �Ұ�";
            }
            else
            {
                afterGrade.text = ((Grade)nextGradeInt).ToString();
            }
        }
    }

    private void ShowAdvancementFailedUI(string message)// �±� ���� �޽����� UI�� ǥ���ϴ� �޼���
    {
        advancementAlert.AdvancementFailed(message);// AdvancementAlert ��ũ��Ʈ�� AdvancementFailed �޼��带 ȣ���Ͽ� ���� �޽����� ǥ��
    }
}
