using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //�κ��丮 �� ConsumableItem �߰�, ���Ÿ� �����ϴ� �κ��丮 �Ŵ���
    public static InventoryManager Instance {get;private set;}//�ν��Ͻ� ����
    
    [SerializeField] public int maxSlots = 12;//������ ����Ʈ �г��� ũ�Ⱑ 4*3�̹Ƿ� �ִ� ���� ���� 12�� ����.
    private List<IInventoryItem> items = new List<IInventoryItem>();//�κ��丮 �ý��� �������̽� Ÿ���� ����Ʈ�� ����.
    public event System.Action OnInventoryUpdated;//�κ��丮 ������Ʈ �̺�Ʈ
    private void Awake() => Instance = this;
    public void AddItem(IInventoryItem newItem)//������ �߰� �޼ҵ�
    {
        if(items.Count >=maxSlots)//������ ����Ʈ�� ���� ���� �ƽ����� �̻��� �Ǹ� �κ��丮 ��ȭ
        {
            Debug.Log("Inventory is Full!");
            //�κ��丮 ��ȭ �޼��� ���� ���
            return;
        }

        if(newItem.Type == ItemType.Consumable)//�Ҹ� �������� �߰��ϴ� ���
        {
            var consumable = newItem as ConsumableItem;
            var existing = items.Find(i=>i.ItemID == newItem.ItemID) as ConsumableItem;//�����۾��̵� �����ϸ� ConsumableItemŸ������ ����. �������� ������ null.
            if(existing!=null)
            {
                existing.itemAmount+=consumable.itemAmount;//�������� ������ ��� ���� �����ۿ� ������ ������Ŵ.
            }
            else
            {
                items.Add(newItem);//�κ��丮�� �ش� id�� �������� ������ ���ο� ���������� �߰�.
            }
        }
        else//�Ҹ� �������� �ƴ� ���(��� �������� ���)-> ��� �������� ������ üũ���� �ʴ� ���ϰ�ü.
        {
            items.Add(newItem);
        }

        Debug.Log($"{newItem.ItemName}�������� �߰��Ǿ����ϴ�! �κ��丮�� Ȯ�����ּ���.");
        OnInventoryUpdated?.Invoke();//�ǽð� �κ��丮 ������Ʈ.
        
        InventoryUIManager.Instance.InitSlots();// �߰� �� UI ���� ����
    }

    public List<IInventoryItem> GetItemsByType(ItemType type)
        => items.FindAll(i => i.Type == type);//�������� Ÿ������ Ž��

    public void RemoveItem(int itemID)//������ ���� �޼ҵ�
    {
        var item = items.Find(i => i.ItemID == itemID);//�����Ϸ��� �������� id�� ���� ����Ʈ�� �����ϴ��� Ȯ��.
        
        if(item!=null)//���� ��� �������� �����ϸ�
        {
            Debug.Log($"{item.ItemName}�������� �����Ǿ����ϴ�. ������ ID : {itemID}");
            items.Remove(item);//����
            OnInventoryUpdated?.Invoke();//�ǽð� �κ��丮 ������Ʈ.
            InventoryUIManager.Instance.InitSlots();// �߰� �� UI ���� ����
        }
    }
}
