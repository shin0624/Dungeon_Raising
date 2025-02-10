using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithPlayerItemInfo : MonoBehaviour
{
    /* ���� ��� ������ ui��Ҹ� �����ϴ� Ŭ����.
    1. armorItem�� �� ������ �ش��ϴ� (2*3)�� �׸��� ���� ��ư�� �����ϰ�, �� ��ư Ŭ�� �� ��ư ���� ��µǴ� �� �гο� �÷��̾� �κ��丮(������DB)�� ����� armorItem ����� Ȯ���� �� �ִ�.
    2. ��ư�� �⺻ ��� �̹����� �����ϴٰ�, �гο��� ��ȭ ��� ������ Ŭ�� �� �� �������� �̹����� �ٲ��.
    3. ��ư Ŭ�� �� UpperCenterCorner��ġ�� ��Ȱ�����̴� ������ ����Ʈ �г��� Ȱ��ȭ. -> �гο��� �� �ٿ� �ϳ��� ������ �������� ǥ��.
        -> �ش� �г��� �θ� ��ư�� �ѹ� �� Ŭ���ϸ� ������ ����Ʈ �г��� ������.
    4. �� ���� ��ư ������Ʈ�� �����ϸ�, �������� ������ �̹���, �������� ������ �̸��� ǥ��.
    5. ���õ� ������ ������ ������ �����Ͽ� �ٸ� ���尣 ���� Ŭ�������� ������ �� �ְ� �Ѵ�.
    */

    [SerializeField] private Button[] armorItemButtons = new Button[6];//��� ������ ��ư��. ������� weapon, arm, head, waist, chest, leg
    [SerializeField] private GameObject[] armorItemInfoPanels = new GameObject[6];//�� ��ư�� ��Ȱ��ȭ�� ���·� �����ϴ� �����۸���Ʈ ��� �гε�
    [SerializeField] private ItemDatabase itemDatabase;//������ �����ͺ��̽� ��ũ���ͺ� ������Ʈ

    [SerializeField] private BlackSmithItemList[] blackSmithItemList = new BlackSmithItemList[6];

    private void OnEnable() 
    {
        AttachAllListeners();//������ ���
    }

    private void OnDisable() 
    {
        DettachAllListeners();//������ ����
    }

    private void AttachAllListeners()//�� ��� ������ ������ ��ư�� �̺�Ʈ �����ʸ� ����Ѵ�.
    {
        DettachAllListeners();//Ȥ�� �����ִ� �����ʰ� ���� ��츦 �����ϱ� ����, �ϴ� ��� ���� �� �����ʸ� ����Ѵ�.
        armorItemButtons[0].onClick.AddListener(() => armorItemButtonClicked(0, ItemPart.Weapon));
        armorItemButtons[1].onClick.AddListener(() => armorItemButtonClicked(1, ItemPart.Arm));
        armorItemButtons[2].onClick.AddListener(() => armorItemButtonClicked(2, ItemPart.Head));
        armorItemButtons[3].onClick.AddListener(() => armorItemButtonClicked(3, ItemPart.Waist));
        armorItemButtons[4].onClick.AddListener(() => armorItemButtonClicked(4, ItemPart.Chest));
        armorItemButtons[5].onClick.AddListener(() => armorItemButtonClicked(5, ItemPart.Leg));
        Debug.Log("���尣 ������ ��ư ����");
    }

    private void DettachAllListeners()//â�� ��Ȱ��ȭ �Ǹ� ��� �����ʸ� ����.
    {
        foreach(Button armorItemButton in armorItemButtons)
        {
            armorItemButton.onClick.RemoveAllListeners();
        }
        Debug.Log("���尣 ������ ��ư ����");
    }

    private void armorItemButtonClicked(int num, ItemPart part)//�Ű������� ������ ������ �־, ��ư�� Ŭ���ϸ� �ش��ϴ� ���� ������ ����Ʈ Ȱ��ȭ.
    {
        Debug.Log($"������ ��ư Ŭ���� : {part}");
        bool isExist = itemDatabase.armorItems.Exists(i => i.itemParts == part);//�ش� ��Ʈ�� �������� �ִٸ� true, ������ false.
        if(isExist)
        {
            if(armorItemInfoPanels[num].activeSelf)//�̹� �ش� ����Ʈ�� ���������� �ݴ´�.
            {
                armorItemInfoPanels[num].SetActive(false);
            }
            else//�ش� ����Ʈ�� �������� ������ ����Ʈ�� Ȱ��ȭ.
            {
                for(int i=0; i<armorItemInfoPanels.Length; i++)
                {
                    armorItemInfoPanels[i].SetActive(false);
                }
                armorItemInfoPanels[num].SetActive(true);
                blackSmithItemList[num].DelayPopulateItemList(part);

                Debug.Log($"������ ��ư Ŭ�� : {part}");
            }
        }
        else
        {
            Debug.Log($"{part}�� �ش��ϴ� �������� �����ϰ� ���� �ʽ��ϴ�.");
        }
        
    }
}
