using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUILeftPanelController : MonoBehaviour
{
    /*SinglePlayScene의 왼쪽 패널 버튼 할당과 기능을 담당하는 스크립트.
    ★ 각 버튼 별 기능 설명
        1. 배치 버튼 : 유저가 직접 터치를 통해 유닛을 배치할 수 있는 버튼. 씬이 활성화되면 기본으로 선택되어있는 버튼이다.
        2. 자동 배치 버튼 : 유저 입력 없이 자동으로 유닛이 배치되는 버튼. 클릭 시 유닛의 전투력 순서대로 필드에 자동배치된다.
        3. 초기화 버튼 : 현재 배치된 유닛 위치를 초기화하여 씬 활성화 시의 배치 상태로 되돌린다.
    */
    [Header("Buttons")]
    [SerializeField] private Button placeButton;//배치 버튼
    [SerializeField] private Button autoPlaceButton;//자동 배치 버튼
    [SerializeField] private Button resetPlaceButton;//배치 초기화 버튼

    [Header("Scripts")]
    [SerializeField] private PlaceButtonFunction placeButtonFunction;//배치 버튼의 기능을 담당하는 스크립트.
    [SerializeField] private AutoPlaceButtonFunction autoPlaceButtonFunction;//자동 배치 기능을 담당하는 스크립트.


    private void OnEnable()//캔버스 활성화 시 버튼에 리스너 추가.
    {
        placeButtonFunction.enabled = true;//배치 버튼 기능은 기본으로 활성화되어 있다.
        autoPlaceButtonFunction.enabled = false;//자동 배치 기능은 기본적으로 비활성화. 
        AddListenerToButtons();
    }

    private void AddListenerToButtons()//모든 버튼에 리스너를 추가한다.
    {
        placeButton.onClick.AddListener(OnPlaceButtonClicked);
        autoPlaceButton.onClick.AddListener(OnAutoPlaceButtonClicked);
        resetPlaceButton.onClick.AddListener(OnResetPlaceButtonClicked);
    }

    private void OnPlaceButtonClicked()// 유저가 직접 터치를 통해 유닛을 배치할 수 있는 기능. 서로 다른 레이어 간 이동이 가능하며, 드래그를 통해 캐릭터를 목적지 타일로 위치시킨다. 이 떄 목적지 타일에 다른 유닛이 존재한다면 원래 위치로 복귀한다. 
    {
        placeButtonFunction.enabled = true;//배치 버튼 기능을 활성화한다. 인풋 처리는 PlaceButtonFunction.cs에서 담당한다. 배치 버튼 기능이 비활성화되어 있으면 TilemapOutlineShader.cs도 적용되지 않음.
    }

    private void OnAutoPlaceButtonClicked()// 자동 배치 버튼 : 유저 입력 없이 자동으로 유닛이 배치되는 버튼. 클릭 시 유닛의 전투력 순서대로 필드에 자동배치된다.
    {
        if(!autoPlaceButtonFunction.enabled)
        {
            autoPlaceButtonFunction.enabled = true;//자동 배치 기능을 활성화 한다. 자동 배치 후에도 플레이어가 유닛 위치를 임의로 변경할 수 있도록 placeButtonFunction 컴포넌트는 활성화를 유지한다.
        }
        else
        {
            autoPlaceButtonFunction.StartAutoPlace();//자동배치 기능 활성화 후 다시 자동배치 버튼을 누르면 다시 한 번 유닛들이 자동배치된다.
        }
        
    }

    private void OnResetPlaceButtonClicked()// 초기화 버튼 : 현재 배치된 유닛 위치를 초기화하여 씬 활성화 시의 배치 상태로 되돌린다.
    {

    }
}
