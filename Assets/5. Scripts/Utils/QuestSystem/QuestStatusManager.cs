using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatusManager : MonoBehaviour
{
    //퀘스트 상태 변경 관리 클래스(수락/완료). 싱글톤으로 선언하여 Managers객체에 붙인다. 퀘스트 ui에서 퀘스트 수락버튼 클릭 시AcceptQuest() 호출, 완료 시 CompleteQuest()호출.

    private static QuestStatusManager _instance;
    public static QuestStatusManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 자동 생성
                GameObject obj = GameObject.Find("Managers");
                _instance = obj.AddComponent<QuestStatusManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private List<Quest> activeQuests = new();//Quest타입 리스트변수 -> 현재 활성화된 퀘스트 인스턴스를 생성.
    public static event Action<Quest> OnQuestUpdated;//퀘스트 진행도 변경 시 UI에 실시간 반영을 위한 이벤트.

    public void AcceptQuest(Quest quest)//퀘스트 수락 메서드
    {
        if(quest.status != QuestStatus.Available)
            return;//현재 수락 가능 퀘스트가 아닐 경우 함수 종료.
        
        quest.status = QuestStatus.InProgress;//수락 가능 -> 진행 중으로 상태 변경
        activeQuests.Add(quest);//활성화된 퀘스트 리스트에 추가.
        Debug.Log($"Quest Accected : {quest.questName}");//퀘스트 명을 로그로 표시.
    }

    public void CompleteQuest(Quest quest)//퀘스트 완료
    {
        if(quest.status != QuestStatus.InProgress)
            return;//퀘스트가 현재 진행 중이 아니라면 함수 종료. 현재 진행 중인 퀘스트만 완료될 수 있다.
        
        quest.status = QuestStatus.Completed;//퀘스트를 완료 상태로 바꾼다.
        activeQuests.Remove(quest);//활성화 퀘스트 목록에서 제거.
        Debug.Log($"Quest Completed : {quest.questName}");
    }

    public void UpdateObjectiveProgress(Quest quest, int objectiveIndex, int amount)//퀘스트 진행상황 업데이트 메서드.
    {   
        if(quest.status != QuestStatus.InProgress)
            return;
        
        quest.objectives[objectiveIndex].currentCount +=amount;//퀘스트 달성에 필요한 최소 횟수, 수량 등을 amout값을 더해 업데이트.

        OnQuestUpdated?.Invoke(quest);//진행도 업데이트 시 이벤트 발행

        QuestValidator.CheckCompletion(quest);//퀘스트 진행도와 currentCount값이 requiredCount값을 만족 하는지 검증한다. 검증 완료 시 보상을 지급한다.
    }

    public string GetQuestStatus(Quest quest)//외부에서 퀘스트 현재 진행도를 반환할 메서드. QuestManager를 통해 접근한다.
    {
        string currentStatus = "";
        switch(quest.status)
        {
            case QuestStatus.Available :
                currentStatus = "Available";
                break;
            
            case QuestStatus.InProgress :
                currentStatus = "InProgress";
                break;
            
            case QuestStatus.Completed :
                currentStatus = "Completed";
                break;
            
            default : currentStatus = "Unknown Status";
                break;
        }
        return currentStatus;
    }
}
