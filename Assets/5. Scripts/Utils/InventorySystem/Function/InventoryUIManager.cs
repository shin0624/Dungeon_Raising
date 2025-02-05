using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    
    private List<InventorySlotUI> slots = new List<InventorySlotUI>();//인벤토리 슬롯 리스트 생성
    private void Awake() => Instance = this;

    private void Start()
    {
        InventoryManager.Instance.OnInventoryUpdated+=UpdateInventoryUI;//실시간 인벤토리 업데이트에 인벤토리 UI 업데이트 함수를 등록록
        InitSlots();
    }

    private void InitSlots()//슬롯 초기화
    {
        Debug.Log("Slot Init!");
        int maxSlotSize = InventoryManager.Instance.maxSlots;
        if(InventoryCategoryManager.Instance.currentCategory == ItemType.Consumable)
        {
            for(int i=0; i< maxSlotSize; i++)//최대 슬롯 사이즈만큼 슬롯 프리팹을 아이템리스트패널에 동적으로 로드.
            {
                var slot = Instantiate(slotPrefab, itemListParent).GetComponent<InventorySlotUI>();
                slots.Add(slot);
                slot.Clear();
            }
        }
        else
        {
            for(int i=0; i< maxSlotSize; i++)//최대 슬롯 사이즈만큼 슬롯 프리팹을 장비리스트패널에 동적으로 로드.
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
                slots[i].SetUp(currentItems[i]);//인벤토리 리스트에 출력될 아이템의 수량 및 종류를 체크하고 슬롯 이미지 등을 동기화, 버튼 클릭이벤트를 할당.
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
            itemInfoText.text = $"수량 : {consumable.itemAmount}";
        }
        else if(item is ArmorItem armor)
        {
            itemInfoText.text = $"등급 : {armor.itemGrade}\n" +
                                $"부위 : {armor.itemParts}\n" +
                                $"레벨 : {armor.itemLevel}";
        }
    }

}
