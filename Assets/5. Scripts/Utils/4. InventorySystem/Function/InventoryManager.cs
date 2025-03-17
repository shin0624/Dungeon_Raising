using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //인벤토리 내 ConsumableItem 추가, 제거를 관리하는 인벤토리 매니저
    public static InventoryManager Instance {get;private set;}//인스턴스 선언
    
    [SerializeField] public int maxSlots = 12;//아이템 리스트 패널의 크기가 4*3이므로 최대 슬롯 수를 12로 지정.
    [SerializeField] private ItemDatabase itemDatabase;
    private List<IInventoryItem> items = new List<IInventoryItem>();//인벤토리 시스템 인터페이스 타입의 리스트를 선언.
    public event System.Action OnInventoryUpdated;//인벤토리 업데이트 이벤트
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(IInventoryItem newItem)//아이템 추가 메소드
    {
        if(items.Count >=maxSlots)//아이템 리스트의 원소 수가 맥스슬롯 이상이 되면 인벤토리 포화
        {
            //Debug.Log("Inventory is Full!");//인벤토리 포화 메세지 추후 출력
            return;
        }
        switch (newItem.Type)//아이템 타입에 따라 추가하는 방식을 다르게 한다. 소모성일 경우 수량을 늘리고, 장비의 경우 중복 보유를 허용하지 않을 것이므로 Add()만 실행.
        {
            case ItemType.Consumable://소모성 아이템을 추가하는 경우
                {
                    var consumable = newItem as ConsumableItem;
                    var existing = items.Find(i => i.ItemID == newItem.ItemID) as ConsumableItem;//아이템 아이디가 존재하면 ConsumableItem타입으로 리턴. 존재하지 않으면 null.
                    if (existing != null)
                    {
                        existing.itemAmount += consumable.itemAmount;//아이템이 존재할 경우 기존 아이템에 수량을 증가시킴.
                    }
                    else
                    {
                        items.Add(newItem);//인벤토리에 해당 id의 아이템이 없으면 새로운 아이템으로 추가.
                        itemDatabase.consumableItems.Add((ConsumableItem)newItem);//아이템 데이터베이스에 실제로 추가.
                    }
                    break;
                }

            default://디폴트 : 장비 아이템을 추가하는 경우. 동일 장비는 발생하지 않는다.
                items.Add(newItem);
                itemDatabase.armorItems.Add((ArmorItem)newItem);//아이템 데이터베이스에 실제로 추가.
                break;
        }
        //Debug.Log($"{newItem.ItemName}아이템이 추가되었습니다! 인벤토리를 확인해주세요.");
        OnInventoryUpdated?.Invoke();//실시간 인벤토리 업데이트.
        
        InventoryUIManager.Instance.InitSlots();// 추가 후 UI 강제 갱신
    }

    public List<IInventoryItem> GetItemsByType(ItemType type)
        => items.FindAll(i => i.Type == type);//아이템의 타입으로 탐색

    public void RemoveItem(int itemID)//아이템 제거 메소드
    {
        var item = items.Find(i => i.ItemID == itemID);//제거하려는 아이템의 id가 현재 리스트에 존재하는지 확인
        
        if(item!=null)//제거 대상 아이템이 존재하면
        {
            switch(item.Type)
            {
                case ItemType.Consumable : //소비 아이템의 경우
                itemDatabase.consumableItems.Remove((ConsumableItem)item);
                break;

                case ItemType.Armor :// 장비 아이템의 경우
                itemDatabase.armorItems.Remove((ArmorItem)item);
                break;

                default : //그 외의 경우(오류)
                Debug.LogError("제거 대상 아이템의 타입이 잘못되었습니다. ItemDatabase에서 삭제할 수 없습니다.");
                break;
            }
            //Debug.Log($"{item.ItemName}아이템이 삭제되었습니다. 아이템 ID : {itemID}");
            items.Remove(item);//제거
            OnInventoryUpdated?.Invoke();//실시간 인벤토리 업데이트.
            InventoryUIManager.Instance.InitSlots();// 추가 후 UI 강제 갱신
        }
        else
        {
            Debug.LogError($"제거 대상 아이템 [{item.ItemName}, {itemID}]이 존재하지 않습니다. 아이템 제거 요청이 거부되었습니다.");
        }
    }

}
