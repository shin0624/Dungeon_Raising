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

    private IEnumerator DelayInit()
    {
        yield return null;
        InitSlots();
    }
    public void InitSlots()// 기존 슬롯 삭제 후 초기화
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

       //새 슬롯을 생성
       int maxSlotSize = 12;
       for(int i=0; i<maxSlotSize; i++)
        {
            var slot = Instantiate(slotPrefab, itemListParent).GetComponent<InventorySlotUI>();
            if(slot!=null) consumableSlots.Add(slot);//슬롯이 존재할 때에만 리스트에 추가
        }

       for(int i = 0; i<maxSlotSize; i++)
        {
            var slot02 = Instantiate(slotPrefab, armorListParent).GetComponent<InventorySlotUI>();
            if(slot02!=null) armorSlots.Add(slot02);//슬롯이 존재할 때에만 리스트에 추가
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
        // for(int i=0; i<activeSlots.Count; i++)
        // {
        //     if(activeSlots[i] == null)//활성화된 슬롯 리스트가 비어있다면
        //     {
        //         Debug.LogWarning($"슬롯 {i}번이 비어있습니다. 재생성 시도 중...");
        //         activeSlots[i] = CreateNewSlot(i);
        //     }
        //     if(i < currentItems.Count)//현재 카테고리 타입의 아이템 리스트 수량까지
        //     {
        //         activeSlots[i].SetUp(currentItems[i]);//아이템 동기화
        //     }
        //     else
        //     {
        //         activeSlots[i].Clear();
        //     }
        // }

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

    private InventorySlotUI CreateNewSlot(int index)//슬롯이 비어있을 때 새로운 슬롯객체를 생성한다.
    {
        var newSlot = Instantiate(slotPrefab, InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable ? itemListParent : armorListParent).GetComponent<InventorySlotUI>();
        if(newSlot != null)
        {
            newSlot.transform.SetSiblingIndex(index);//생성한 newSlot객체의 하이어라키 순서를 index로 바꾼다.
            return newSlot;
        }
        else
        {
            Debug.LogError("새 슬롯 생성 실패!");
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

}
