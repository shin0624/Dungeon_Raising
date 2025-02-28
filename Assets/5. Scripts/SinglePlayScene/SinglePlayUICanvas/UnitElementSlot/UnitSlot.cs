using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlot : MonoBehaviour
{
    // UnitElementSlot �����տ� ����� ��ũ��Ʈ. ����/���� �����Ϳ��� ����ȭ�� ����Ѵ�. 
    // InventorySlotUI.cs�� ������ �������� �����Ѵ�. 
    // UnitElementSlot���� �������� �� ���� ������ �����̹���, ���� ����. ==> __Information ��ü���� ������ ���� ������ �����ϴ� �¾� �޼��� �ʿ�
    // �� ��ũ��Ʈ�� ���� �����տ� �Ҵ�Ǵ� �޼��� �����̹Ƿ�, �� �޼���� �Ű������� ���� PlaceUILowerPanelController.cs�� ����Ѵ�.

    [SerializeField] private Image unitHalfImage;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private HeroDatabase heroDatabase;
    [SerializeField] private SoldierDatabase soldierDatabase;
    
    //__Information ��ü�� Sprite, Level <-> ������ Sprite, Level�� ����ȭ
    public void SetUpHero(GameObject hero, int num)//SoldierInformation, HeroInformation�� �κ��丮 ������ó�� ������ �������� Type�� �����Ƿ�, ������ ������ ���� �ٸ� �޼��带 ���.
    {
        unitHalfImage.sprite= heroDatabase.heroInformationList[num].heroHalfImage;
        level.text = $"Lv {heroDatabase.heroInformationList[num].heroLevel}";
    }

    public void SetUpSoldier(GameObject soldier, int num)
    {
        unitHalfImage.sprite = soldierDatabase.soldierInformationList[num].soldierHalfImage;
        level.text = $"Lv {soldierDatabase.soldierInformationList[num].soldierLevel}";
    }


}
