using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] private ItemDatabase itemDatabase; // 추가
    
    private List<InventorySlotUI> consumableSlots = new List<InventorySlotUI>();//소비 아이템 인벤토리 슬롯 리스트 생성
    private List<InventorySlotUI> armorSlots = new List<InventorySlotUI>();// 장비 인벤토리 슬롯 리스트 생성
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject); // 중복 방지
        } 

        Debug.Log("InventoryUIManager 초기화 완료");
    }

    private void Start()
    {
        InventoryManager.Instance.OnInventoryUpdated+=UpdateInventoryUI;//실시간 인벤토리 업데이트에 인벤토리 UI 업데이트 함수를 등록

        if (InventoryCategoryManager.Instance == null)
            Debug.LogError("InventoryCategoryManager.Instance is NULL!");

        if (InventoryUIManager.Instance == null)
            Debug.LogError("InventoryUIManager.Instance is NULL!");

        InitSlots();
        //StartCoroutine(DelayInit());
    }

    public void InitSlots()// 기존 슬롯 삭제 후 초기화
    {
       Debug.Log("Slot Init!");
        ClearAllSlot();

               // 활성화된 카테고리 타입 확인
            var currentCategory = InventoryCategoryManager.Instance.currentCategory;

            // 해당 타입의 아이템 개수 가져오기
            int itemCount = currentCategory == ItemType.Consumable ? 
                itemDatabase.consumableItems.Count : 
                itemDatabase.armorItems.Count;

            // 해당 타입의 부모 트랜스폼 설정
            Transform parent = currentCategory == ItemType.Consumable ? 
                itemListParent : 
                armorListParent;

            // 실제 아이템 개수만큼 슬롯 생성
            for(int i = 0; i < itemCount; i++)
            {
                var slot = Instantiate(slotPrefab, parent).GetComponent<InventorySlotUI>();
                
                // 아이템 데이터 할당
                IInventoryItem itemData = currentCategory == ItemType.Consumable ?
                    (IInventoryItem)itemDatabase.consumableItems[i] :
                    (IInventoryItem)itemDatabase.armorItems[i];
                
                slot.SetUp(itemData);

                // 리스트에 추가
                if(currentCategory == ItemType.Consumable)
                    consumableSlots.Add(slot);
                else
                    armorSlots.Add(slot);
        }
       
       Debug.Log($"슬롯 초기화 완료! 소비 슬롯: {consumableSlots.Count}, 장비 슬롯: {armorSlots.Count}");
    }

    public void UpdateInventoryUI()
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
            itemInfoText.text = $"수량 : {consumable.itemAmount}";
        }
    }

    private void ShowArmorItem(IInventoryItem item)
    {
        armorNameText.text = item.ItemName;
        if(item is ArmorItem armor)
        {
            armorInfoText.text = $"등급 : {armor.itemGrade}\n" +
                $"부위 : {armor.itemParts}\n" +
                $"레벨 : {armor.itemLevel}";
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