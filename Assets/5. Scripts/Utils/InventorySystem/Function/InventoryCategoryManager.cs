using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryCategoryManager : MonoBehaviour
{
    //������ ī�װ�(Consumable, Armor)�� �����ϴ� �Ŵ���
    public static InventoryCategoryManager Instance {get; private set;}
    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
            
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }
        Debug.Log("InventoryCategoryManager �ʱ�ȭ �Ϸ�");
    }
    public ItemType currentCategory {get; private set;} = ItemType.Consumable;//�⺻ ī�װ��� Consumable�� ����. ��, �κ��丮 �г��� ó�� Ȱ��ȭ�Ǹ� �׻� "������"��ư�� Ȧ��ٿ�Ǿ��ִ�.

    public void SwitchCategory(ItemType newCategory)//ī�װ� ���� �޼���. �κ��丮 �г��� ��ư �ν����Ϳ��� OnClick�̺�Ʈ�� �Ҵ�.
    {
        //StartCoroutine(SwitchCategoryCoroutine(newCategory));

        if(currentCategory == newCategory) return;//���� ī�װ��� �ݺ�ȣ��� ��� ���� ������� �����ϱ� ���� ����.

        currentCategory = newCategory;//���� ī�װ��� Consumable, Armor �� �ϳ��� ����
        Debug.Log($"ī�װ� ��ȯ: {currentCategory}");

        if(InventoryUIManager.Instance!=null)
        {
            //InventoryUIManager.Instance.InitSlots(); // �߰�
            InventoryUIManager.Instance.UpdateInventoryUI();//UI ������Ʈ�� ����.
        }
        else
        {
            Debug.LogError("InventoryUIManager is NULL");
        }

    }
    
    private IEnumerator SwitchCategoryCoroutine(ItemType newCategory)
    {
        yield return new WaitForSeconds(1.0f);
        currentCategory = newCategory;//���� ī�װ��� Consumable, Armor �� �ϳ��� ����
        Debug.Log($"ī�װ� ��ȯ: {currentCategory}");

        if(InventoryUIManager.Instance!=null)
        {
            InventoryUIManager.Instance.InitSlots(); // �߰�
            InventoryUIManager.Instance.UpdateInventoryUI();//UI ������Ʈ
        }
        else
        {
            Debug.LogError("InventoryUIManager is NULL");
        }

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
