using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    //�κ��丮 UI�� ItemSlot �����۵��� ǥ���ϴ� ��Ʈ�ѷ� Ŭ����.

    public static InventoryUIManager Instance {get;private set;}

    [Header("References")]
    [SerializeField] private Transform itemListParent;//������ ����Ʈ�� ����� �г�
    [SerializeField] private Transform armorListParent;//��� ����Ʈ�� ����� �г�
    [SerializeField] private GameObject slotPrefab;//������ ���� ������
    [SerializeField] private TextMeshProUGUI itemNameText;//ItemInfoPanel�� ǥ�õ� ������ �̸�.
    [SerializeField] private TextMeshProUGUI itemInfoText;//ItemInfoPanel�� ǥ�õ� ������ ����.
    
    private List<InventorySlotUI> slots = new List<InventorySlotUI>();//�κ��丮 ���� ����Ʈ ����
    private void Awake() => Instance = this;

    private void Start()
    {
        InventoryManager.Instance.OnInventoryUpdated+=UpdateInventoryUI;//�ǽð� �κ��丮 ������Ʈ�� �κ��丮 UI ������Ʈ �Լ��� ��Ϸ�
        InitSlots();
    }

    private void InitSlots()//���� �ʱ�ȭ
    {
        Debug.Log("Slot Init!");
        int maxSlotSize = InventoryManager.Instance.maxSlots;
        if(InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable)
        {
            for(int i=0; i< maxSlotSize; i++)//�ִ� ���� �����ŭ ���� �������� �����۸���Ʈ�гο� �������� �ε�.
            {
                var slot = Instantiate(slotPrefab, itemListParent).GetComponent<InventorySlotUI>();
                slots.Add(slot);
                slot.Clear();
            }
        }
        else
        {
            for(int i=0; i< maxSlotSize; i++)//�ִ� ���� �����ŭ ���� �������� ��񸮽�Ʈ�гο� �������� �ε�.
            {
                var slot = Instantiate(slotPrefab, armorListParent).GetComponent<InventorySlotUI>();
                slots.Add(slot);
                slot.Clear();
            }
        }

    }

    public void UpdateInventoryUI()
    {
        var currentItems = InventoryManager.Instance.GetItemsByType(InventoryCategoryManager.Instance.currentCategory);
        for(int i=0; i<slots.Count;i++)
        {
            if(i < currentItems.Count)
            {
                slots[i].SetUp(currentItems[i]);//�κ��丮 ����Ʈ�� ��µ� �������� ���� �� ������ üũ�ϰ� ���� �̹��� ���� ����ȭ, ��ư Ŭ���̺�Ʈ�� �Ҵ�.
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    public void ShowItemInfo(IInventoryItem item)
    {
        itemNameText.text = item.ItemName;
        if(item is ConsumableItem consumable)
        {
            itemInfoText.text = $"���� : {consumable.itemAmount}";
        }
        else if(item is ArmorItem armor)
        {
            itemInfoText.text = $"��� : {armor.itemGrade}\n" +
                                $"���� : {armor.itemParts}\n" +
                                $"���� : {armor.itemLevel}";
        }
    }

}
