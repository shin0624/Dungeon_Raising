using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class UnitSlot : MonoBehaviour
{
    // UnitElementSlot 프리팹에 적용될 스크립트. 영웅/병사 데이터와의 동기화를 담당한다. 
    // InventorySlotUI.cs와 유사한 로직으로 설계한다. 
    // UnitElementSlot에서 보여져야 할 것은 유닛의 하프이미지, 유닛 레벨. ==> __Information 객체에서 가져온 값을 변수에 적용하는 셋업 메서드 필요
    // 본 스크립트는 슬롯 프리팹에 할당되는 메서드 모음이므로, 각 메서드는 매개변수를 통해 PlaceUILowerPanelController.cs와 통신한다.

    private Image unitHalfImage;
    private int level;


    
    public void SetUpHero(GameObject hero)//SoldierInformation, HeroInformation은 인벤토리 아이템처럼 종류를 구분짓는 Type이 없으므로, 유닛의 종류에 따라 다른 메서드를 사용.
    {
        
    }

    public  void SetUpSoldier(GameObject soldier)
    {

    }


}
