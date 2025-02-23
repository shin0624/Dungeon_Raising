using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //�κ��丮 �� ConsumableItem �߰�, ���Ÿ� �����ϴ� �κ��丮 �Ŵ���
    public static InventoryManager Instance {get;private set;}//�ν��Ͻ� ����
    
    [SerializeField] public int maxSlots = 12;//������ ����Ʈ �г��� ũ�Ⱑ 4*3�̹Ƿ� �ִ� ���� ���� 12�� ����.
    [SerializeField] private ItemDatabase itemDatabase;
    private List<IInventoryItem> items = new List<IInventoryItem>();//�κ��丮 �ý��� �������̽� Ÿ���� ����Ʈ�� ����.
    public event System.Action OnInventoryUpdated;//�κ��丮 ������Ʈ �̺�Ʈ
    

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
                itemDatabase.consumableItems.Add((ConsumableItem)newItem);//������ �����ͺ��̽��� ������ �߰�.
            }
        }
        else//�Ҹ� �������� �ƴ� ���(��� �������� ���)-> ��� �������� ������ üũ���� �ʴ� ���ϰ�ü.
        {
            items.Add(newItem);
            itemDatabase.armorItems.Add((ArmorItem)newItem);//������ �����ͺ��̽��� ������ �߰�.
        }

        Debug.Log($"{newItem.ItemName}�������� �߰��Ǿ����ϴ�! �κ��丮�� Ȯ�����ּ���.");
        OnInventoryUpdated?.Invoke();//�ǽð� �κ��丮 ������Ʈ.
        
        InventoryUIManager.Instance.InitSlots();// �߰� �� UI ���� ����
    }

    public List<IInventoryItem> GetItemsByType(ItemType type)
        => items.FindAll(i => i.Type == type);//�������� Ÿ������ Ž��

    public void RemoveItem(int itemID)//������ ���� �޼ҵ�
    {
        var item = items.Find(i => i.ItemID == itemID);//�����Ϸ��� �������� id�� ���� ����Ʈ�� �����ϴ��� Ȯ��
        
        if(item!=null)//���� ��� �������� �����ϸ�
        {
            switch(item.Type)
            {
                case ItemType.Consumable : 
                itemDatabase.consumableItems.Remove((ConsumableItem)item);
                break;

                case ItemType.Armor :
                itemDatabase.armorItems.Remove((ArmorItem)item);
                break;

                default : 
                Debug.Log("���� ��� �������� Ÿ���� �߸��Ǿ����ϴ�. ItemDatabase���� ������ �� �����ϴ�.");
                break;
            }
            Debug.Log($"{item.ItemName}�������� �����Ǿ����ϴ�. ������ ID : {itemID}");
            items.Remove(item);//����
            OnInventoryUpdated?.Invoke();//�ǽð� �κ��丮 ������Ʈ.
            InventoryUIManager.Instance.InitSlots();// �߰� �� UI ���� ����
        }
        else
        {
            Debug.LogError($"���� ��� ������ [{item.ItemName}, {itemID}]�� �������� �ʽ��ϴ�. ������ ���� ��û�� �źεǾ����ϴ�.");
        }
    }

}
