using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryCategoryManager : MonoBehaviour
{
    //아이템 카테고리(Consumable, Armor)를 관리하는 매니저
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
            Destroy(gameObject); // 중복 방지
        }
        Debug.Log("InventoryCategoryManager 초기화 완료");
    }
    public ItemType currentCategory {get; private set;} = ItemType.Consumable;//기본 카테고리는 Consumable로 설정. 즉, 인벤토리 패널이 처음 활성화되면 항상 "아이템"버튼이 홀드다운되어있다.

    public void SwitchCategory(ItemType newCategory)//카테고리 변경 메서드. 인벤토리 패널의 버튼 인스펙터에서 OnClick이벤트로 할당.
    {
        //StartCoroutine(SwitchCategoryCoroutine(newCategory));

        if(currentCategory == newCategory) return;//동일 카테고리가 반복호출될 경우 슬롯 재생성을 방지하기 위해 무시.

        currentCategory = newCategory;//현재 카테고리를 Consumable, Armor 중 하나로 변경
        Debug.Log($"카테고리 전환: {currentCategory}");

        if(InventoryUIManager.Instance!=null)
        {
            //InventoryUIManager.Instance.InitSlots(); // 추가
            InventoryUIManager.Instance.UpdateInventoryUI();//UI 업데이트만 수행.
        }
        else
        {
            Debug.LogError("InventoryUIManager is NULL");
        }

    }
    
    private IEnumerator SwitchCategoryCoroutine(ItemType newCategory)
    {
        yield return new WaitForSeconds(1.0f);
        currentCategory = newCategory;//현재 카테고리를 Consumable, Armor 중 하나로 변경
        Debug.Log($"카테고리 전환: {currentCategory}");

        if(InventoryUIManager.Instance!=null)
        {
            InventoryUIManager.Instance.InitSlots(); // 추가
            InventoryUIManager.Instance.UpdateInventoryUI();//UI 업데이트
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
