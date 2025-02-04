using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanelController : MonoBehaviour
{
    // Script - Utils - QuestSystem에 정의된 퀘스트 시스템을 MainScene의 QuestPanel - QuestListPanel에 적용하기 위한 클래스.
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

        연결해야 할 것 : 퀘스트 이름(목표), 퀘스트 진행도, 완료/미완료 여부, 보상 재화 수량 및 이미지

        --> 어디에 연결? : 퀘스트리스트 패널 - 일일 퀘스트 리스트 - 일일 퀘스트 엘리먼트
                                            - 주간 퀘스트 리스트 - 주간 퀘스트 엘리먼트
                                            --> 일일, 주간 개체 각각에 연결
                                            --> 스크립트가 연결된 엘리먼트를 프리팹화해서, 추후에 퀘스트 자동 생성 구현
    */
    [SerializeField] private Quest currentQuest;//퀘스트 객체(스크립터블 오브젝트 할당)
    [SerializeField] private TextMeshProUGUI questGoal;// 퀘스트 목표
    [SerializeField] private TextMeshProUGUI progressText;// 진행도
    [SerializeField] private TextMeshProUGUI interactButtonText; // 퀘스트 진행도에 따라 달라지는 버튼의 텍스트. Available : "수락" / InProgress : "진행중", 비활성화 / Completed : "완료" -> 완료 버튼 클릭 시 비활성화.
    [SerializeField] private Button interactButton; //퀘스트 진행도에 따라 버튼에 할당되는 이벤트를 다르게 설정. 하나의 버튼으로 수락, 완료 이벤트를 제어한다.
    [SerializeField] private Image progressImage;//fillAmount를 제어할 진행도  이미지

    private void OnEnable()//퀘스트 패널이 활성화되면 퀘스트 정보를 세팅 
    {
        SetButtonState();
        SetQuest(currentQuest);
        SetButtonListener();
        
    } 
    private void Update() 
    {
         SetButtonState();
         SetQuest(currentQuest);
         SetButtonListener();
         TempFunc();
    }

    private void TempFunc()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            QuestManager.UpdateQuestProgress(currentQuest, 0, 1);
        }
    }

    private void SetQuest(Quest quest)
    {
        if (quest == null) return;
            questGoal.text = quest.questName ?? "Unknown Quest"; // 기본값 설정

        questGoal.text = currentQuest.questName;
        progressText.text = $"{currentQuest.objectives[0].currentCount} / {currentQuest.objectives[0].requiredCount}";
        progressImage.fillAmount = currentQuest.objectives[0].currentCount;
    }

    private void SetButtonState()//현재 퀘스트 진행도에 따라 버튼의 text와 상호작용 가능 여부를 설정.
    {
       switch(currentQuest.status)
       {
        case QuestStatus.Available :
            interactButtonText.text = "수락";
            interactButton.interactable = true;
            break;

        case QuestStatus.InProgress :  
            interactButtonText.text = "진행중";
            interactButton.interactable = false;
            break;

        case QuestStatus.Completed :
            interactButtonText.text = "완료";
            interactButton.interactable = true;
            break;

        default : //혹시 스테이터스 값이 오류일 경우 버튼이 눌리지 않게 설정. 
            interactButtonText.text = "X";
            interactButton.interactable = false;
            break;
       }
    }

    private void SetButtonListener()
    {
        switch(currentQuest.status)
        {
            case QuestStatus.Available :
                interactButton.onClick.AddListener(OnAvailableButtonClicked);
                break;

            case QuestStatus.InProgress :
                interactButton.onClick.RemoveAllListeners();
                break;

            case QuestStatus.Completed :
                interactButton.onClick.AddListener(OnCompleteButtonClicked);
                break;

            default : //혹시 스테이터스 값이 오류일 경우 버튼이 눌리지 않게 설정. 
                interactButton.onClick.RemoveAllListeners();
                break;
        }
    }

    private void OnAvailableButtonClicked()//수락 버튼 클릭 시
    {
        QuestManager.AcceptQuest(currentQuest);//퀘스트 상태 변경 및 현재 진행 중인 리스트에 추가는 모두 구현되어 있음.
    }

    private void OnCompleteButtonClicked()//완료 버튼 클릭 시
    {
        QuestManager.CompleteQuest(currentQuest);//퀘스트 클리어
    }
}
