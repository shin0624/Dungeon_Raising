using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystemController : MonoBehaviour
{
    //우측 상단 메뉴 버튼 클릭으로 동작하는 메뉴 시스템을 제어하는 클래스. 
    //MenuPanel은 [공지사항, 메일함, 설정] 3개의 버튼으로 구성됨 -> 각 버튼을 누르면 해당하는 패널이 각각 MenuPanelPivot에서 활성화됨.
    [SerializeField] private Button[] buttonsInMenuPanel;//메뉴 패널에 위치한 3개의 버튼
    [SerializeField] private GameObject[] panelsInPivot;
    void Start()
    {
        DeactiveAllPanels();//열려있는게 있다면 닫고
        AssignToButtons();//버튼에 이벤트 할당
    }

    private void AssignToButtons()//각 버튼에 클릭 이벤트를 할당.
    {
        for(int i=0; i<buttonsInMenuPanel.Length; i++)//버튼 이벤트 동적 할당
        {
            int index = i;//클로저 캡처 방지
            buttonsInMenuPanel[i].onClick.AddListener(() => OnMenuPanelButtonClicked(index));
        }
    }

    private void OnMenuPanelButtonClicked(int index)//버튼에 할당할 클릭 이벤트
    {
        if(index < 0 || index >= panelsInPivot.Length)//인덱스 유효성 검사 실행
        {
            Debug.LogError("Invalid MenuPanel index!");
            return;
        }
        ActiveSpecificPanel(index);//선택된 버튼에 대한 패널 활성화
    }

    private void DeactiveAllPanels()//기존 패널들 모두 비활성화.
    {
        foreach(GameObject panel in panelsInPivot)
        {
            if (panel != null) panel.SetActive(false);
        }
    }

    private void ActiveSpecificPanel(int index)//버튼에 대한 패널을 활성화. 버튼 이벤트 함수에서 넘겨받은 index와 패널 인덱스를 비교하여 맞으면 활성화.
    {
        DeactiveAllPanels();//모든 패널 우선 비활성화.
        if(panelsInPivot[index] != null)//index번지에 해당하는 패널이 존재하면 활성화
        {
            panelsInPivot[index].SetActive(true);
            DOTWeenUIAnimation.PopupAnimationInUI(panelsInPivot[index], panelsInPivot[index].transform.localScale * 1.2f, 0.2f, panelsInPivot[index].transform.localScale, 0.2f);
        }
    }
}
