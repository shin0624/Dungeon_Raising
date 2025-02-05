using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryCategoryManager : MonoBehaviour
{
    //������ ī�װ�(Consumable, Armor)�� �����ϴ� �Ŵ���
    public static InventoryCategoryManager Instance {get; private set;}
    public ItemType currentCategory {get; private set;} = ItemType.Consumable;//�⺻ ī�װ��� Consumable�� ����. ��, �κ��丮 �г��� ó�� Ȱ��ȭ�Ǹ� �׻� "������"��ư�� Ȧ��ٿ�Ǿ��ִ�.
    public void SwitchCategory(ItemType newCategory)//ī�װ� ���� �޼���. �κ��丮 �г��� ��ư �ν����Ϳ��� OnClick�̺�Ʈ�� �Ҵ�.
    {
        currentCategory = newCategory;//���� ī�װ��� Consumable, Armor �� �ϳ��� ����
        Debug.Log($"ī�װ� ��ȯ: {newCategory}");
        InventoryUIManager.Instance.UpdateInventoryUI();//UI ������Ʈ
        Debug.Log($"itemtype :  {currentCategory}");
    }

    public void SwitchArmorCategory()
    {
        SwitchCategory(ItemType.Armor);
    }

    public void SwitchConsumableCategory()
    {
        SwitchCategory(ItemType.Consumable);
    }
}
