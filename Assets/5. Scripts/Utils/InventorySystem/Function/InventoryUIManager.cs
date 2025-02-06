using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class InventoryUIManager : MonoBehaviour
{
    //인벤토리 UI에 ItemSlot 아이템들을 표시하는 컨트롤러 클래스.

    public static InventoryUIManager Instance {get;private set;}

    [Header("References")]
    [SerializeField] private Transform itemListParent;//아이템 리스트를 출력할 패널
    [SerializeField] private Transform armorListParent;//장비 리스트를 출력할 패널
    [SerializeField] private GameObject slotPrefab;//아이템 슬롯 프리팹
    [SerializeField] private TextMeshProUGUI itemNameText;//ItemInfoPanel에 표시될 아이템 이름.
    [SerializeField] private TextMeshProUGUI itemInfoText;//ItemInfoPanel에 표시될 아이템 정보.
    [SerializeField] private TextMeshProUGUI armorNameText;//ArmorInfoPanel에 표시될 아이템 이름.
    [SerializeField] private TextMeshProUGUI armorInfoText;//ArmorInfoPanel에 표시될 아이템 정보.
    [SerializeField] private Image itemImageSprite;//ItemInfoPanel에 표시될 아이콘 이미지.
    [SerializeField] private Image armorImageSprite;//ArmorInfoPanel에 표시될 아이콘 이미지.
    [SerializeField] private ItemDatabase itemDatabase; // 추가
    
    private List<InventorySlotUI> consumableSlots = new List<InventorySlotUI>();//소비 아이템 인벤토리 슬롯 리스트 생성
    private List<InventorySlotUI> armorSlots = new List<InventorySlotUI>();// 장비 인벤토리 슬롯 리스트 생성
    private void Awake()
    {
        InitThisInstance();
    }

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded-=OnSceneLoaded;//이벤트 구독 해제 추가
    }

    private void Start()
    {   
        InitMainSceneManagerInstance();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//씬이 변경될 때(인벤토리에 접근 가능한 MainScene으로의 전환) 인스턴스를 재참조한다.
    {
        if (scene.name == "MainScene")
        {
            Debug.Log("MainScene 로드됨 - UI 참조 재설정");
             StartCoroutine(InitReferences());
        }
        else
        {
            Debug.Log("InventoryUIManager가 불필요한 씬으로 이동 - 삭제됨.");
        }
    }

    private void InitThisInstance()//싱글톤 안정성을 강화한 인스턴스 초기화 메서드.
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        Debug.Log("InventoryUIManager 초기화 완료");
    }
    private IEnumerator InitReferences()//비동기 로드 후 ui 오브젝트 초기화
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

    public void InitSlots()// 기존 슬롯 삭제 후 초기화
    {
        Debug.Log("Slot Init!");
        ClearAllSlot();
        var currentCategory = InventoryCategoryManager.Instance.currentCategory;// 활성화된 카테고리 타입 확인

        int itemCount = currentCategory == ItemType.Consumable ? itemDatabase.consumableItems.Count : itemDatabase.armorItems.Count; // 해당 타입의 아이템 개수 가져오기

        Transform parent = currentCategory == ItemType.Consumable ? itemListParent : armorListParent;// 해당 타입의 부모 트랜스폼 설정

        for(int i = 0; i < itemCount; i++)// 실제 아이템 개수만큼 슬롯 생성
        {
            var slot = Instantiate(slotPrefab, parent).GetComponent<InventorySlotUI>();                    
            IInventoryItem itemData = currentCategory == ItemType.Consumable ? (IInventoryItem)itemDatabase.consumableItems[i] : (IInventoryItem)itemDatabase.armorItems[i];// 아이템 데이터 할당
            slot.SetUp(itemData);
            
            if(currentCategory == ItemType.Consumable)// 리스트에 추가
                consumableSlots.Add(slot);
            else
                armorSlots.Add(slot);
        }
       Debug.Log($"슬롯 초기화 완료! 소비 슬롯: {consumableSlots.Count}, 장비 슬롯: {armorSlots.Count}");
    }

    public void UpdateInventoryUI()//인벤토리 UI를 현재 선택된 카테고리와 일치하도록 업데이트하는 메서드.
    {
        if(InventoryManager.Instance == null || InventoryCategoryManager.Instance == null)//인스턴스가 null일 경우 에러 출력.
        {
            Debug.LogError(" 매니저 인스턴스가 초기화되지 않았습니다!");
            return;
        }
        List<IInventoryItem> currentItems = InventoryManager.Instance.GetItemsByType(InventoryCategoryManager.Instance.currentCategory);//현재 카테고리 타입에 맞는 아이템을 찾아 IInventoryItem타입 리스트에 저장
        List<InventorySlotUI> activeSlots = InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable ? consumableSlots : armorSlots;//현재 카테고리에 따라 활성화될 슬롯을 결정.
        List<InventorySlotUI> inactiveSlots = InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable ? armorSlots : consumableSlots;//슬롯 활성, 비활성 전환 방식으로 변경하기 위해, 비활성화 슬롯 생성을 추가.

            for (int i = 0; i < activeSlots.Count; i++)//활성 슬롯만 업데이트.
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
            
            foreach (var slot in inactiveSlots) // 비활성 슬롯 숨기기
            {
                slot.gameObject.SetActive(false);
            }
    }

    public void ShowItemInfo(IInventoryItem item)//선택된 아이템 카테고리와 일치하는 아이템의 정보를 InformationPanel에 출력하는 함수.
    {   
        if (item == null) //널체크 추가
        {
            Debug.LogError("아이템 데이터가 null입니다!");
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
        Debug.Log($"아이템 정보 표시: {item.ItemName}, 타입: {item.Type}");
    }

    private void ShowConsumableItem(IInventoryItem item)//소비 아이템의 정보를 동기화하는 메서드.
    {
        itemNameText.text = item.ItemName ?? "Unknown Item";
        itemImageSprite.sprite = item.ItemSprite;

        if (itemNameText == null || itemInfoText == null)//널체크 추가
        {
            Debug.LogError("ItemInfoPanel의 Text 컴포넌트가 연결되지 않았습니다!");
            return;
        }

        if(item is ConsumableItem consumable)
        {
            itemInfoText.text = $"수량 : {consumable.itemAmount}";
        }
        else
        {
            Debug.LogError("ConsumableItem 타입이 아닙니다!");
        }
    }

    private void ShowArmorItem(IInventoryItem item)//장비 아이템의 정보를 동기화하는 메서드.
    {
        armorNameText.text = item.ItemName ?? "Unknown Item";
        armorImageSprite.sprite = item.ItemSprite;
        
        if (armorNameText == null || armorInfoText == null)//널체크 추가
        {
            Debug.LogError("ArmorInfoPanel의 Text 컴포넌트가 연결되지 않았습니다!");
            return;
        }

        if(item is ArmorItem armor)
        {
            armorInfoText.text = $"등급 : {armor.itemGrade.ToString()}\n" +
                                 $"부위 : {armor.itemParts.ToString()}\n" +
                                 $"레벨 : {armor.itemLevel.ToString()}";
        }
        else
        {
            Debug.LogError("ArmorItem 타입이 아닙니다!");
        }
    }

    private void ClearAllSlot()//인벤토리가 닫히면 모든 슬롯을 초기화한다.
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

    private void AssignUIReferences()// 씬 재진입 시 UI 요소를 동적으로 재참조
    {   
        itemListParent = GameObject.FindWithTag("MainScene_ItemListPanel").transform;
        armorListParent = GameObject.FindWithTag("MainScene_ArmorListPanel").transform;
        itemNameText = GameObject.FindWithTag("MainScene_ItemNameText").GetComponent<TextMeshProUGUI>();
        armorNameText = GameObject.FindWithTag("MainScene_ArmorNameText").GetComponent<TextMeshProUGUI>();
        itemInfoText = GameObject.FindWithTag("MainScene_ItemInfoText").GetComponent<TextMeshProUGUI>();
        armorInfoText = GameObject.FindWithTag("MainScene_ArmorInfoText").GetComponent<TextMeshProUGUI>();
        itemImageSprite = GameObject.FindWithTag("MainScene_ItemImageSprite").GetComponent<Image>();
        armorImageSprite = GameObject.FindWithTag("MainScene_ArmorImageSprite").GetComponent<Image>();

        // null 체크
        if (itemListParent == null || armorListParent == null || itemNameText == null || armorNameText == null ||
            itemInfoText == null || armorInfoText == null || itemImageSprite == null || armorImageSprite == null)
        {
            Debug.LogError("UI 참조 중 일부가 null입니다. 태그가 올바르게 설정되었는지 확인하세요.");
        }
    }
}