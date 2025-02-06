using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    //인벤토리의 ItemSlot UI를 관리하는 스크립트.
    //--슬롯 프리팹에서 할당할 값--
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Button slotButton;
    //---아이템 타입
    private IInventoryItem currentItem;
    private ConsumableItem consumableItem;
    private ArmorItem armorItem;
    //--공통 값--
    private string itemName;
    private ItemType type;
    private int itemId;
    //--Consumable 값--
    private int itemAmount;
    //--Armor 값--
    private ItemGrade[] itemGrade;
    private ItemPart[] itemPart;
    private int itemLevel;
    private int levelUpCost;
    private int gradeUpCost;
    private float offensivePower;
    private float defensivePower;


    public void SetUp(IInventoryItem item)//인벤토리 리스트 셋업. InventoryUIManager의 UpdateInventoryUI()에서 호출.
    {   
        CommonDataSetiing(item);
        gameObject.SetActive(true);
        switch(item.Type)
        {
            case ItemType.Consumable:
                consumableItem = (ConsumableItem)item;
                ConsumableSetting(consumableItem);
                Debug.Log("셋업 메서드 호출 : 아이템 타입 Consumable");
                break;
            case ItemType.Armor:
                armorItem = (ArmorItem)item;
                ArmorSetting(armorItem);
                Debug.Log("셋업 메서드 호출 : 아이템 타입 Armor");
                break;
        }
        slotButton.onClick.AddListener(OnSlotClicked);//슬롯 버튼 클릭 이벤트 할당
        Debug.Log("슬롯 버튼 이벤트 할당 완료, 셋업 메서드 정상 종료");
    }

    private void CommonDataSetiing(IInventoryItem item)//공통 데이터 세팅
    {
        itemName = item.ItemName;
        itemIcon.sprite = item.ItemSprite;//slotPrefab의 스프라이트를 item의 스프라이트로 설정
        type = item.Type;
        itemId = item.ItemID;
        itemIcon.gameObject.SetActive(true);
    }

    private void ConsumableSetting(ConsumableItem a)//소비 아이템일 때의 동기화
    {
       itemAmount =  a.itemAmount;
       amountText.text = itemAmount.ToString();
       amountText.gameObject.SetActive(true);
    }

    private void ArmorSetting(ArmorItem b)// 장비 데이터 세팅
    {
        amountText.text = "";
        itemGrade = b.itemGrade;
        itemPart = b.itemParts;
        itemLevel = b.itemLevel;
        levelUpCost = b.levelUpCost;
        gradeUpCost = b.gradeUpCost;
        offensivePower = b.offensivePower;
        defensivePower = b.defensivePower;
    
        amountText.gameObject.SetActive(false);//ArmorItem이면 수량 출력이 불필요하므로 텍스트를 비활성화.
    }

    public void Clear()//인벤토리 리스트 클리어.
    {
        if (itemIcon != null) itemIcon.gameObject.SetActive(false);
        if (amountText != null) amountText.gameObject.SetActive(false);
        if (slotButton != null) slotButton.onClick.RemoveAllListeners();
    }

    private void OnSlotClicked() => InventoryUIManager.Instance.ShowItemInfo(currentItem);//아이템슬롯 프리팹의 버튼 클릭 시 클릭된 아이템의 정보를 출력.
}
