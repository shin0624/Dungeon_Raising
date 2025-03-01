using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUILowerPanelController : MonoBehaviour
{
    // UI에서 영웅 버튼, 병사 버튼 클릭 시 플레이어가 보유중인 영웅, 병사 db의 요소들을 보여주고, 클릭하면 현재 스폰된 영웅 객체가 목록에서 클릭한 영웅으로 바뀌는 기능을 제어하는 클래스.
    // UnitManager의 GetHeroUnitList, GetSoldierList를 사용하여 DB에 존재하는 유닛 중 플레이어 조건과 맞는 유닛들을 탐색.
    // 조건에 맞는 유닛 슬롯을 HorizontalLayOutGroup에 생성.(인벤토리와 유사한 구조)

    // 본 스크립트는 LowerPanel에 나열될 유닛 슬롯의 제어를 담당하며, 슬롯과 db정보 간 동기화(레벨, 사진 등)은 다른 스크립트에서 수행.

    [Header("References")]
    [SerializeField] private Button heroButton;// 영웅 리스트 출력 버튼
    [SerializeField] private Button soliderButton;//병사 리스트 출력 버튼
    [SerializeField] private GameObject slotPrefab;//슬롯 프리팹
    [SerializeField] private Transform slotPivot;//슬롯의 부모 트랜스폼 
    private List<GameObject> heroSlots = new List<GameObject>();//영웅 슬롯 리스트.
    private List<GameObject> soldierSlots = new List<GameObject>();//병사 슬롯 리스트.
    private UnitManager unitManager;

    private void Start()
    {
        unitManager = gameObject.GetComponent<UnitManager>();
        heroButton.onClick.AddListener(OnHeroButtonClicked);
        soliderButton.onClick.AddListener(OnSoldierButtonClicked);
    }

    private void OnHeroButtonClicked()//영웅 버튼 클릭 시
    {
       if(soldierSlots.Count!=0)
        {
            ClearSlot(2);
        }
        InitSlots(1);
    }

    private void OnSoldierButtonClicked()//병사 버튼 클릭 시
    {
        if(heroSlots.Count!=0)
        {
            ClearSlot(1);
        }
        InitSlots(2);
    }

    private void InitSlots(int flag)//클릭한 버튼에 따라 (영웅 / 병사) 의 목록이 출력된다.
    {   
        ClearSlot(flag);

        int slotAmount = flag == 1 ? unitManager.GetHeroUnitList().Count : unitManager.GetSoliderList().Count;//이미 DB에서 영웅 정보를 뽑아내어 리스트로 리턴되도록 한 메서드가 있으니, 이 리스트의 Count 만큼 LowerPanel에 생성할 슬롯 개수를 설정.
        //버튼 클릭 시 flag값이 전달되고, 1이면 영웅, 2이면 병사 리스트가 출력될 것.
        
        for(int i=0; i< slotAmount; i++)
        {
            GameObject slotButton = Instantiate(slotPrefab, slotPivot);//슬롯 프리팹의 인스턴스화가 끝나면, UnitSlot.cs의 셋업 메서드를 호출하여 __Information 객체의 Sprite, Level <-> 슬롯의 Sprite, Level을 동기화 한다.
            UnitSlot unitSlot = slotButton.GetComponent<UnitSlot>();//인스턴스에서 UnitSlot 컴포넌트를 가져온다.
            if(unitSlot!=null)
            {
                switch(flag)
                {
                    case 1://영웅 버튼 클릭 시
                        unitSlot.SetUpHero(slotButton, i);
                        heroSlots.Add(slotButton);
                        break;

                    case 2://병사 버튼 클릭 시
                        unitSlot.SetUpSoldier(slotButton, i);
                        soldierSlots.Add(slotButton);
                        break;

                    default:
                        Debug.LogWarning("Unknown Button Clicked !");
                        break;
                }
            }
            else//만약 UnitSlot이 null이라면
            {
                Debug.LogError("slotPrefab not connected to UnitSlot Component.");
            }
        }
    }

    private void ClearSlot(int flag)//기존 슬롯 제거 후 리스트를 비우는 메서드.
    {
        List<GameObject> slotToClear = (flag==1) ? heroSlots : soldierSlots;
        foreach(GameObject slot in slotToClear)
        {
            Destroy(slot);
        }
        slotToClear.Clear();
    }






}
