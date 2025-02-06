using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //인벤토리 내 ConsumableItem 추가, 제거를 관리하는 인벤토리 매니저
    public static InventoryManager Instance {get;private set;}//인스턴스 선언
    
    [SerializeField] public int maxSlots = 12;//아이템 리스트 패널의 크기가 4*3이므로 최대 슬롯 수를 12로 지정.
    private List<IInventoryItem> items = new List<IInventoryItem>();//인벤토리 시스템 인터페이스 타입의 리스트를 선언.
    public event System.Action OnInventoryUpdated;//인벤토리 업데이트 이벤트
    private void Awake() => Instance = this;
    public void AddItem(IInventoryItem newItem)//아이템 추가 메소드
    {
        if(items.Count >=maxSlots)//아이템 리스트의 원소 수가 맥스슬롯 이상이 되면 인벤토리 포화
        {
            Debug.Log("Inventory is Full!");
            //인벤토리 포화 메세지 추후 출력
            return;
        }

        if(newItem.Type == ItemType.Consumable)//소모성 아이템을 추가하는 경우
        {
            var consumable = newItem as ConsumableItem;
            var existing = items.Find(i=>i.ItemID == newItem.ItemID) as ConsumableItem;//아이템아이디가 존재하면 ConsumableItem타입으로 리턴. 존재하지 않으면 null.
            if(existing!=null)
            {
                existing.itemAmount+=consumable.itemAmount;//아이템이 존재할 경우 기존 아이템에 수량을 증가시킴.
            }
            else
            {
                items.Add(newItem);//인벤토리에 해당 id의 아이템이 없으면 새로운 아이템으로 추가.
            }
        }
        else//소모성 아이템이 아닐 경우(장비 아이템일 경우)-> 장비 아이템은 수량을 체크하지 않는 단일객체.
        {
            items.Add(newItem);
        }

        Debug.Log($"{newItem.ItemName}아이템이 추가되었습니다! 인벤토리를 확인해주세요.");
        OnInventoryUpdated?.Invoke();//실시간 인벤토리 업데이트.
        
        InventoryUIManager.Instance.InitSlots();// 추가 후 UI 강제 갱신
    }

    public List<IInventoryItem> GetItemsByType(ItemType type)
        => items.FindAll(i => i.Type == type);//아이템의 타입으로 탐색

    public void RemoveItem(int itemID)//아이템 제거 메소드
    {
        var item = items.Find(i => i.ItemID == itemID);//제거하려는 아이템의 id가 현재 리스트에 존재하는지 확인.
        
        if(item!=null)//제거 대상 아이템이 존재하면
        {
            Debug.Log($"{item.ItemName}아이템이 삭제되었습니다. 아이템 ID : {itemID}");
            items.Remove(item);//제거
            OnInventoryUpdated?.Invoke();//실시간 인벤토리 업데이트.
            InventoryUIManager.Instance.InitSlots();// 추가 후 UI 강제 갱신
        }
    }
}
