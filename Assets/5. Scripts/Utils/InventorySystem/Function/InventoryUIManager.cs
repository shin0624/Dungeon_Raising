using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
    [SerializeField] private TextMeshProUGUI armorNameText;//ArmorInfoPanel�� ǥ�õ� ������ �̸�.
    [SerializeField] private TextMeshProUGUI armorInfoText;//ArmorInfoPanel�� ǥ�õ� ������ ����.
    
    private List<InventorySlotUI> consumableSlots = new List<InventorySlotUI>();//�Һ� ������ �κ��丮 ���� ����Ʈ ����
    private List<InventorySlotUI> armorSlots = new List<InventorySlotUI>();// ��� �κ��丮 ���� ����Ʈ ����
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject); // �ߺ� ����
        } 

        Debug.Log("InventoryUIManager �ʱ�ȭ �Ϸ�");
    }

    private void Start()
    {
        InventoryManager.Instance.OnInventoryUpdated+=UpdateInventoryUI;//�ǽð� �κ��丮 ������Ʈ�� �κ��丮 UI ������Ʈ �Լ��� ���

        if (InventoryCategoryManager.Instance == null)
            Debug.LogError("InventoryCategoryManager.Instance is NULL!");

        if (InventoryUIManager.Instance == null)
            Debug.LogError("InventoryUIManager.Instance is NULL!");

        InitSlots();
        //StartCoroutine(DelayInit());
    }

    private IEnumerator DelayInit()
    {
        yield return null;
        InitSlots();
    }
    public void InitSlots()// ���� ���� ���� �� �ʱ�ȭ
    {
       Debug.Log("Slot Init!");

       foreach(var slot in armorSlots)
        {
            if(slot!=null && slot.gameObject!=null)
            {
                Destroy(slot.gameObject);
            } 
        }
       armorSlots.Clear();

       foreach(var slot in consumableSlots)
        {
            if(slot!=null && slot.gameObject !=null)
            {
                Destroy(slot.gameObject);
            }
        }
       consumableSlots.Clear();

       //�� ������ ����
       int maxSlotSize = 12;
       for(int i=0; i<maxSlotSize; i++)
        {
            var slot = Instantiate(slotPrefab, itemListParent).GetComponent<InventorySlotUI>();
            if(slot!=null) consumableSlots.Add(slot);//������ ������ ������ ����Ʈ�� �߰�
        }

       for(int i = 0; i<maxSlotSize; i++)
        {
            var slot02 = Instantiate(slotPrefab, armorListParent).GetComponent<InventorySlotUI>();
            if(slot02!=null) armorSlots.Add(slot02);//������ ������ ������ ����Ʈ�� �߰�
        }
       
       Debug.Log($"���� �ʱ�ȭ �Ϸ�! �Һ� ����: {consumableSlots.Count}, ��� ����: {armorSlots.Count}");
    }

    public void UpdateInventoryUI()
    {
        if(InventoryManager.Instance == null || InventoryCategoryManager.Instance == null)//�ν��Ͻ��� null�� ��� ���� ���.
        {
            Debug.LogError(" �Ŵ��� �ν��Ͻ��� �ʱ�ȭ���� �ʾҽ��ϴ�!");
            return;
        }
        List<IInventoryItem> currentItems = InventoryManager.Instance.GetItemsByType(InventoryCategoryManager.Instance.currentCategory);//���� ī�װ� Ÿ�Կ� �´� �������� ã�� IInventoryItemŸ�� ����Ʈ�� ����
        List<InventorySlotUI> activeSlots = InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable ? consumableSlots : armorSlots;//���� ī�װ��� ���� Ȱ��ȭ�� ������ ����.
        List<InventorySlotUI> inactiveSlots = InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable ? armorSlots : consumableSlots;//���� Ȱ��, ��Ȱ�� ��ȯ ������� �����ϱ� ����, ��Ȱ��ȭ ���� ������ �߰�.
        // for(int i=0; i<activeSlots.Count; i++)
        // {
        //     if(activeSlots[i] == null)//Ȱ��ȭ�� ���� ����Ʈ�� ����ִٸ�
        //     {
        //         Debug.LogWarning($"���� {i}���� ����ֽ��ϴ�. ����� �õ� ��...");
        //         activeSlots[i] = CreateNewSlot(i);
        //     }
        //     if(i < currentItems.Count)//���� ī�װ� Ÿ���� ������ ����Ʈ ��������
        //     {
        //         activeSlots[i].SetUp(currentItems[i]);//������ ����ȭ
        //     }
        //     else
        //     {
        //         activeSlots[i].Clear();
        //     }
        // }

            for (int i = 0; i < activeSlots.Count; i++)//Ȱ�� ���Ը� ������Ʈ.
            {
                if (i < currentItems.Count)
                {
                    activeSlots[i].SetUp(currentItems[i]);
                    activeSlots[i].gameObject.SetActive(true);
                }
                else
                {
                    activeSlots[i].Clear();
                    activeSlots[i].gameObject.SetActive(false);
                }
            }
            
            foreach (var slot in inactiveSlots) // ��Ȱ�� ���� �����
            {
                slot.gameObject.SetActive(false);
            }


    }

    private InventorySlotUI CreateNewSlot(int index)//������ ������� �� ���ο� ���԰�ü�� �����Ѵ�.
    {
        var newSlot = Instantiate(slotPrefab, InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable ? itemListParent : armorListParent).GetComponent<InventorySlotUI>();
        if(newSlot != null)
        {
            newSlot.transform.SetSiblingIndex(index);//������ newSlot��ü�� ���̾��Ű ������ index�� �ٲ۴�.
            return newSlot;
        }
        else
        {
            Debug.LogError("�� ���� ���� ����!");
            return null;
        }
    }

    public void ShowItemInfo(IInventoryItem item)
    {
        switch(InventoryCategoryManager.Instance.currentCategory)
        {
            case ItemType.Consumable:
            ShowConsumableItem(item);
            break;

            case ItemType.Armor:
            ShowArmorItem(item);
            break;

            default :
            Debug.Log("ShowItemInfo Method Error");
            break;
        }
    }

    private void ShowConsumableItem(IInventoryItem item)
    {
        itemNameText.text = item.ItemName;
        if(item is ConsumableItem consumable)
        {
            itemInfoText.text = $"���� : {consumable.itemAmount}";
        }
    }

    private void ShowArmorItem(IInventoryItem item)
    {
        armorNameText.text = item.ItemName;
        if(item is ArmorItem armor)
        {
            armorInfoText.text = $"��� : {armor.itemGrade}\n" +
                $"���� : {armor.itemParts}\n" +
                $"���� : {armor.itemLevel}";
        }
    }

}
