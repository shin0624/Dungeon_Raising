using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithItemElementClicker : MonoBehaviour
{
    [SerializeField] private Button itemListElementButton;//������ ����Ʈ ������Ʈ�� ��ư ������Ʈ
    [SerializeField] private Image beforeImage;//������ ��ư �Ʒ��� ǥ�õ� ������ �̹���
    [SerializeField] private Image afterImage;//�±� ��ư �Ʒ��� ǥ�õ� ������ �̹���
    [SerializeField] private TextMeshProUGUI beforeLevel;// [ ���緹��(itemLevel) / �ִ뷹��(10)]
    [SerializeField] private TextMeshProUGUI beforeStatus;// ������ ��� [���ݷ�(offensivePower) n], ���� ��� [����(defensivePower) n]
    [SerializeField] private TextMeshProUGUI afterLevel;// [ ���緹��(itemLevel) + 1 / �ִ뷹��(10)]
    [SerializeField] private TextMeshProUGUI afterStatus; //������ ��� [���ݷ�(offensivePower) + ���ġ n], ���� ��� [����(defensivePower) = ���ġ n]
    [SerializeField] private TextMeshProUGUI levelUpCost;
    [SerializeField] private TextMeshProUGUI gradeUpCost;

    // ���� �� �������ͽ� ���ġ ���� ���� ��ġ ������ �Ϸ�Ǹ� �߰�. 

    public void AddListenerToElementButton(GameObject newItem, ArmorItem armorItem)//������ ����� ĸ��ȭ�Ͽ� BlackSmithItemList.cs�� �ڷ�ƾ�� �ִ´�.
    {
        itemListElementButton.onClick.AddListener(() => ElementLink(newItem, armorItem));

        Debug.Log("����Ʈ �� ��ư�� ������ ��ϵ�.");
    }

    private void ElementLink(GameObject newItem, ArmorItem armorItem)//BlackSmithItemList.cs�� PopulateItemList()���� ����Ʈ�� ��µ� ������ ��ư Ŭ�� �� �ش� ������ ������ RightPivot�� ǥ���ϴ� �޼���
    {   
       Debug.Log($"{armorItem.itemParts} ���� ������ ����Ʈ���� {armorItem.itemName}Ŭ����. ");

       beforeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
       afterImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;

       beforeLevel.text = $"Lv {armorItem.itemLevel} / 10";
       afterLevel.text = $"Lv {armorItem.itemLevel + 1} / 10";

       levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.levelUpCost}";
       gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.gradeUpCost}";

        if(armorItem.itemParts == ItemPart.Weapon) 
        {
            beforeStatus.text = $"���ݷ� {armorItem.offensivePower}";
            afterStatus.text = $"���ݷ� {armorItem.offensivePower + 10}";//���ġ�� �ϴ� �ӽ÷� ����.
        }
        else
        {
            beforeStatus.text = $"���� {armorItem.defensivePower}";
            afterStatus.text = $"���� {armorItem.defensivePower + 10}";//���ġ�� �ϴ� �ӽ÷� ����.
        }
    }


}
