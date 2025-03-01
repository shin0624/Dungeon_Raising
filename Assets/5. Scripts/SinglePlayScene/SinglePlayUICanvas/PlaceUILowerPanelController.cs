using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUILowerPanelController : MonoBehaviour
{
    // UI���� ���� ��ư, ���� ��ư Ŭ�� �� �÷��̾ �������� ����, ���� db�� ��ҵ��� �����ְ�, Ŭ���ϸ� ���� ������ ���� ��ü�� ��Ͽ��� Ŭ���� �������� �ٲ�� ����� �����ϴ� Ŭ����.
    // UnitManager�� GetHeroUnitList, GetSoldierList�� ����Ͽ� DB�� �����ϴ� ���� �� �÷��̾� ���ǰ� �´� ���ֵ��� Ž��.
    // ���ǿ� �´� ���� ������ HorizontalLayOutGroup�� ����.(�κ��丮�� ������ ����)

    // �� ��ũ��Ʈ�� LowerPanel�� ������ ���� ������ ��� ����ϸ�, ���԰� db���� �� ����ȭ(����, ���� ��)�� �ٸ� ��ũ��Ʈ���� ����.

    [Header("References")]
    [SerializeField] private Button heroButton;// ���� ����Ʈ ��� ��ư
    [SerializeField] private Button soliderButton;//���� ����Ʈ ��� ��ư
    [SerializeField] private GameObject slotPrefab;//���� ������
    [SerializeField] private Transform slotPivot;//������ �θ� Ʈ������ 
    private List<GameObject> heroSlots = new List<GameObject>();//���� ���� ����Ʈ.
    private List<GameObject> soldierSlots = new List<GameObject>();//���� ���� ����Ʈ.
    private UnitManager unitManager;

    private void Start()
    {
        unitManager = gameObject.GetComponent<UnitManager>();
        heroButton.onClick.AddListener(OnHeroButtonClicked);
        soliderButton.onClick.AddListener(OnSoldierButtonClicked);
    }

    private void OnHeroButtonClicked()//���� ��ư Ŭ�� ��
    {
       if(soldierSlots.Count!=0)
        {
            ClearSlot(2);
        }
        InitSlots(1);
    }

    private void OnSoldierButtonClicked()//���� ��ư Ŭ�� ��
    {
        if(heroSlots.Count!=0)
        {
            ClearSlot(1);
        }
        InitSlots(2);
    }

    private void InitSlots(int flag)//Ŭ���� ��ư�� ���� (���� / ����) �� ����� ��µȴ�.
    {   
        ClearSlot(flag);

        int slotAmount = flag == 1 ? unitManager.GetHeroUnitList().Count : unitManager.GetSoliderList().Count;//�̹� DB���� ���� ������ �̾Ƴ��� ����Ʈ�� ���ϵǵ��� �� �޼��尡 ������, �� ����Ʈ�� Count ��ŭ LowerPanel�� ������ ���� ������ ����.
        //��ư Ŭ�� �� flag���� ���޵ǰ�, 1�̸� ����, 2�̸� ���� ����Ʈ�� ��µ� ��.
        
        for(int i=0; i< slotAmount; i++)
        {
            GameObject slotButton = Instantiate(slotPrefab, slotPivot);//���� �������� �ν��Ͻ�ȭ�� ������, UnitSlot.cs�� �¾� �޼��带 ȣ���Ͽ� __Information ��ü�� Sprite, Level <-> ������ Sprite, Level�� ����ȭ �Ѵ�.
            UnitSlot unitSlot = slotButton.GetComponent<UnitSlot>();//�ν��Ͻ����� UnitSlot ������Ʈ�� �����´�.
            if(unitSlot!=null)
            {
                switch(flag)
                {
                    case 1://���� ��ư Ŭ�� ��
                        unitSlot.SetUpHero(slotButton, i);
                        heroSlots.Add(slotButton);
                        break;

                    case 2://���� ��ư Ŭ�� ��
                        unitSlot.SetUpSoldier(slotButton, i);
                        soldierSlots.Add(slotButton);
                        break;

                    default:
                        Debug.LogWarning("Unknown Button Clicked !");
                        break;
                }
            }
            else//���� UnitSlot�� null�̶��
            {
                Debug.LogError("slotPrefab not connected to UnitSlot Component.");
            }
        }
    }

    private void ClearSlot(int flag)//���� ���� ���� �� ����Ʈ�� ���� �޼���.
    {
        List<GameObject> slotToClear = (flag==1) ? heroSlots : soldierSlots;
        foreach(GameObject slot in slotToClear)
        {
            Destroy(slot);
        }
        slotToClear.Clear();
    }






}
