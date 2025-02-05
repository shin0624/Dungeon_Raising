using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryCategoryManager : MonoBehaviour
{
    //아이템 카테고리(Consumable, Armor)를 관리하는 매니저
    public static InventoryCategoryManager Instance {get; private set;}
    public ItemType currentCategory {get; private set;} = ItemType.Consumable;//기본 카테고리는 Consumable로 설정. 즉, 인벤토리 패널이 처음 활성화되면 항상 "아이템"버튼이 홀드다운되어있다.
    public void SwitchCategory(ItemType newCategory)//카테고리 변경 메서드. 인벤토리 패널의 버튼 인스펙터에서 OnClick이벤트로 할당.
    {
        currentCategory = newCategory;//현재 카테고리를 Consumable, Armor 중 하나로 변경
        Debug.Log($"카테고리 전환: {newCategory}");
        InventoryUIManager.Instance.UpdateInventoryUI();//UI 업데이트
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
