using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanelController : MonoBehaviour
{
    //Script - Utils - QuestSystem에 정의된 퀘스트 시스템을 MainScene의 QuestPanel - QuestListPanel에 적용하기 위한 클래스.
    // QuestListPanel은 EverydayQuestList, WeekendQuestList라는 하위 패널로 구분된다. 각 리스트의 구성 요소는 동일.
    /*  ___QuestList 객체(부모) : 퀘스트 창
            - ___QuestElement(Image) : 퀘스트 목록 내 각 퀘스트 객체
                - QuestGoal(TMPro text) : 퀘스트 이름(목표)

                - Progress(Image -> 이미지 타입 Fill)
                    - ProgressText(TMPro text)  : 퀘스트 진행도( 50/50 ) <-> (currentCount / RequiredCount)

                - RewardImage(Image) : 보상 재화 이미지
                    - RewardAmount(TMPro text) : 보상 재화 수량 (int currency)

                - CompleteButton(Button)
                    - CompleteText(TMPro text)

        연결해야 할 것 : 퀘스트 이름(목표), 퀘스트 진행도, 일일/주간 여부, 완료/미완료 여부, 보상 재화 수량 및 이미지
    */

    [SerializeField] private TextMeshProUGUI questGoal;//퀘스트 목표
    [SerializeField] private TextMeshProUGUI progressText;//진행도
    [SerializeField] private TextMeshProUGUI completeText; // 퀘스트 진행도에 따라 달라지는 텍스트. Available : "수락" / InProgress : "완료", 비활성화 / Completed : "완료" -> 완료 버튼 클릭 시 비활성화.
    [SerializeField] private Button completeButton; //퀘스트 진행도에 따라 버튼에 할당되는 이벤트를 다르게 설정. 하나의 버튼으로 수락, 완료 이벤트를 제어한다.
    private QuestDate questDate;//일일, 주간 여부
    private Quest currentQuest;//퀘스트 객체

    private void Start() 
    {
        
    }

    private void SetQuest(Quest quest)
    {

    }

    private void OnAvailableButtonClicked()
    {

    }

    private void OnCompleteButtonClicked()
    {
        
    }

    



}
