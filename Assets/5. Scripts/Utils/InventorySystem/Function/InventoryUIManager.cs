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
    [SerializeField] private ItemDatabase itemDatabase; // �߰�
    
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

    public void InitSlots()// ���� ���� ���� �� �ʱ�ȭ
    {
       Debug.Log("Slot Init!");
        ClearAllSlot();

               // Ȱ��ȭ�� ī�װ� Ÿ�� Ȯ��
            var currentCategory = InventoryCategoryManager.Instance.currentCategory;

            // �ش� Ÿ���� ������ ���� ��������
            int itemCount = currentCategory == ItemType.Consumable ? 
                itemDatabase.consumableItems.Count : 
                itemDatabase.armorItems.Count;

            // �ش� Ÿ���� �θ� Ʈ������ ����
            Transform parent = currentCategory == ItemType.Consumable ? 
                itemListParent : 
                armorListParent;

            // ���� ������ ������ŭ ���� ����
            for(int i = 0; i < itemCount; i++)
            {
                var slot = Instantiate(slotPrefab, parent).GetComponent<InventorySlotUI>();
                
                // ������ ������ �Ҵ�
                IInventoryItem itemData = currentCategory == ItemType.Consumable ?
                    (IInventoryItem)itemDatabase.consumableItems[i] :
                    (IInventoryItem)itemDatabase.armorItems[i];
                
                slot.SetUp(itemData);

                // ����Ʈ�� �߰�
                if(currentCategory == ItemType.Consumable)
                    consumableSlots.Add(slot);
                else
                    armorSlots.Add(slot);
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

    private void ClearAllSlot()
    {
       foreach (var slot in consumableSlots)
        {
            if (slot != null && slot.gameObject != null) Destroy(slot.gameObject);
        }
        consumableSlots.Clear();

        foreach (var slot in armorSlots)
        {
            if (slot != null && slot.gameObject != null) Destroy(slot.gameObject);
        }
        armorSlots.Clear();
    }
}