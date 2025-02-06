using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Image itemImageSprite;//ItemInfoPanel�� ǥ�õ� ������ �̹���.
    [SerializeField] private Image armorImageSprite;//ArmorInfoPanel�� ǥ�õ� ������ �̹���.
    [SerializeField] private ItemDatabase itemDatabase; // �߰�
    
    private List<InventorySlotUI> consumableSlots = new List<InventorySlotUI>();//�Һ� ������ �κ��丮 ���� ����Ʈ ����
    private List<InventorySlotUI> armorSlots = new List<InventorySlotUI>();// ��� �κ��丮 ���� ����Ʈ ����
    private void Awake()
    {
        InitThisInstance();
    }

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded-=OnSceneLoaded;//�̺�Ʈ ���� ���� �߰�
    }

    private void Start()
    {   
        InitMainSceneManagerInstance();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//���� ����� ��(�κ��丮�� ���� ������ MainScene������ ��ȯ) �ν��Ͻ��� �������Ѵ�.
    {
        if (scene.name == "MainScene")
        {
            Debug.Log("MainScene �ε�� - UI ���� �缳��");
             StartCoroutine(InitReferences());
        }
        else
        {
            Debug.Log("InventoryUIManager�� ���ʿ��� ������ �̵� - ������.");
        }
    }

    private void InitThisInstance()//�̱��� �������� ��ȭ�� �ν��Ͻ� �ʱ�ȭ �޼���.
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        Debug.Log("InventoryUIManager �ʱ�ȭ �Ϸ�");
    }
    private IEnumerator InitReferences()//�񵿱� �ε� �� ui ������Ʈ �ʱ�ȭ
    {    
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ItemListPanel") != null);
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ArmorListPanel") != null);
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ItemNameText") != null);
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ArmorNameText") != null);
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ItemInfoText") != null);
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ArmorInfoText") != null);
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ItemImageSprite") != null);
        yield return new WaitUntil(() => GameObject.FindWithTag("MainScene_ArmorImageSprite") != null);
        AssignUIReferences();
        InitSlots();
    }

    private void InitMainSceneManagerInstance()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryUpdated += UpdateInventoryUI;

        if (InventoryCategoryManager.Instance == null)
            Debug.LogError("InventoryCategoryManager.Instance is NULL!");

        if (InventoryUIManager.Instance == null)
            Debug.LogError("InventoryUIManager.Instance is NULL!");

        InitSlots();
    }

    public void InitSlots()// ���� ���� ���� �� �ʱ�ȭ
    {
        Debug.Log("Slot Init!");
        ClearAllSlot();
        var currentCategory = InventoryCategoryManager.Instance.currentCategory;// Ȱ��ȭ�� ī�װ� Ÿ�� Ȯ��

        int itemCount = currentCategory == ItemType.Consumable ? itemDatabase.consumableItems.Count : itemDatabase.armorItems.Count; // �ش� Ÿ���� ������ ���� ��������

        Transform parent = currentCategory == ItemType.Consumable ? itemListParent : armorListParent;// �ش� Ÿ���� �θ� Ʈ������ ����

        for(int i = 0; i < itemCount; i++)// ���� ������ ������ŭ ���� ����
        {
            var slot = Instantiate(slotPrefab, parent).GetComponent<InventorySlotUI>();                    
            IInventoryItem itemData = currentCategory == ItemType.Consumable ? (IInventoryItem)itemDatabase.consumableItems[i] : (IInventoryItem)itemDatabase.armorItems[i];// ������ ������ �Ҵ�
            slot.SetUp(itemData);
            
            if(currentCategory == ItemType.Consumable)// ����Ʈ�� �߰�
                consumableSlots.Add(slot);
            else
                armorSlots.Add(slot);
        }
       Debug.Log($"���� �ʱ�ȭ �Ϸ�! �Һ� ����: {consumableSlots.Count}, ��� ����: {armorSlots.Count}");
    }

    public void UpdateInventoryUI()//�κ��丮 UI�� ���� ���õ� ī�װ��� ��ġ�ϵ��� ������Ʈ�ϴ� �޼���.
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

    public void ShowItemInfo(IInventoryItem item)//���õ� ������ ī�װ��� ��ġ�ϴ� �������� ������ InformationPanel�� ����ϴ� �Լ�.
    {   
        if (item == null) //��üũ �߰�
        {
            Debug.LogError("������ �����Ͱ� null�Դϴ�!");
            return;
        }
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
        Debug.Log($"������ ���� ǥ��: {item.ItemName}, Ÿ��: {item.Type}");
    }

    private void ShowConsumableItem(IInventoryItem item)//�Һ� �������� ������ ����ȭ�ϴ� �޼���.
    {
        itemNameText.text = item.ItemName ?? "Unknown Item";
        itemImageSprite.sprite = item.ItemSprite;

        if (itemNameText == null || itemInfoText == null)//��üũ �߰�
        {
            Debug.LogError("ItemInfoPanel�� Text ������Ʈ�� ������� �ʾҽ��ϴ�!");
            return;
        }

        if(item is ConsumableItem consumable)
        {
            itemInfoText.text = $"���� : {consumable.itemAmount}";
        }
        else
        {
            Debug.LogError("ConsumableItem Ÿ���� �ƴմϴ�!");
        }
    }

    private void ShowArmorItem(IInventoryItem item)//��� �������� ������ ����ȭ�ϴ� �޼���.
    {
        armorNameText.text = item.ItemName ?? "Unknown Item";
        armorImageSprite.sprite = item.ItemSprite;
        
        if (armorNameText == null || armorInfoText == null)//��üũ �߰�
        {
            Debug.LogError("ArmorInfoPanel�� Text ������Ʈ�� ������� �ʾҽ��ϴ�!");
            return;
        }

        if(item is ArmorItem armor)
        {
            armorInfoText.text = $"��� : {armor.itemGrade.ToString()}\n" +
                                 $"���� : {armor.itemParts.ToString()}\n" +
                                 $"���� : {armor.itemLevel.ToString()}";
        }
        else
        {
            Debug.LogError("ArmorItem Ÿ���� �ƴմϴ�!");
        }
    }

    private void ClearAllSlot()//�κ��丮�� ������ ��� ������ �ʱ�ȭ�Ѵ�.
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

    private void AssignUIReferences()// �� ������ �� UI ��Ҹ� �������� ������
    {   
        itemListParent = GameObject.FindWithTag("MainScene_ItemListPanel").transform;
        armorListParent = GameObject.FindWithTag("MainScene_ArmorListPanel").transform;
        itemNameText = GameObject.FindWithTag("MainScene_ItemNameText").GetComponent<TextMeshProUGUI>();
        armorNameText = GameObject.FindWithTag("MainScene_ArmorNameText").GetComponent<TextMeshProUGUI>();
        itemInfoText = GameObject.FindWithTag("MainScene_ItemInfoText").GetComponent<TextMeshProUGUI>();
        armorInfoText = GameObject.FindWithTag("MainScene_ArmorInfoText").GetComponent<TextMeshProUGUI>();
        itemImageSprite = GameObject.FindWithTag("MainScene_ItemImageSprite").GetComponent<Image>();
        armorImageSprite = GameObject.FindWithTag("MainScene_ArmorImageSprite").GetComponent<Image>();

        // null üũ
        if (itemListParent == null || armorListParent == null || itemNameText == null || armorNameText == null ||
            itemInfoText == null || armorInfoText == null || itemImageSprite == null || armorImageSprite == null)
        {
            Debug.LogError("UI ���� �� �Ϻΰ� null�Դϴ�. �±װ� �ùٸ��� �����Ǿ����� Ȯ���ϼ���.");
        }
    }
}