using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelectManager : MonoBehaviour
{
    //종족 선택 창 매니저 : 버튼으로 종족 패널을 활성화. 종족은 3종류가 있으며, 종족 버튼을 누를 때 마다 playerInfo.playerRace값이 갱신되고, 종족 버튼이 홀드다운된 상태로 "다음"버튼을 누르면 그 종족이 playerRace로 최종 저장됨. 
    // 종족 패널은 RaceWindow - RaceWindowPivot 아래에 미리 비활성화 상태로 로드되어 있음. 버튼을 누를 때 마다 현재 패널이 비활성화되고 버튼에 대응되는 패널이 활성화.
    // 종족 패널과 종족 명 모두 배열 내 원소 값으로 관리. 스크립트 내에서 지정하는 것 보다 인스펙터에서 지정해주는 방식을 사용

    [SerializeField] private GameObject[] racePanels;
    [SerializeField] private string[] raceNameHash;
    [SerializeField] private Button[] raceButtons;

    void Start()
    {
        DeactiveAllPanels();

        for(int i=0; i<raceButtons.Length; i++)//버튼 이벤트 동적 할당
        {
            int index = i;//클로저 캡처 방지
            raceButtons[i].onClick.AddListener(() => OnRaceButtonClicked(index));
        }
        InitActivePanel();
    }

    private void InitActivePanel()//처음 [종족 선택 창]이 열리면 racePanels[0]에 해당하는 패널이 홀드다운 된 상태.
    {
        ActiveSpecificPanel(0);
    }

    private void OnRaceButtonClicked(int index)//버튼에 공통으로 들어갈 이벤트 메서드. 버튼 클릭 시 기존에 열린 패널을 닫고 racePanel[n]을 활성화한 후 raceNameHash값을 playerInfo.Playerrace에 저장한다.
    {
        if(index <0 || index >= racePanels.Length || index >=raceNameHash.Length)//인덱스 유효성 검사 실행
        {
            Debug.LogError("Invalid Race index!");
            return;
        }
        ActiveSpecificPanel(index);//선택된 버튼에 대한 패널 활성화
        PlayerInfo.Instance.SetPlayerRace(raceNameHash[index]);//플레이어 정보의 playerRace값을 갱신.

        Debug.Log($"{raceNameHash[index]} button clicked. ");
    }

    private void DeactiveAllPanels()//기존 패널들 모두 비활성화.
    {
        foreach(GameObject panel in racePanels)
        {
            if (panel != null) panel.SetActive(false);
        }
    }

    private void ActiveSpecificPanel(int index)//버튼에 대한 패널을 활성화. 버튼 이벤트 함수에서 넘겨받은 index와 패널 인덱스를 비교하여 맞으면 활성화.
    {
        DeactiveAllPanels();//모든 패널 우선 비활성화.
        if(racePanels[index] != null)//index번지에 해당하는 패널이 존재하면 활성화
        {
            racePanels[index].SetActive(true);
        }

    }
}
