using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestValidator
{
    //퀘스트 검증 클래스. 완료 상태를 검증하고, 보상 지급 함수를 호출한다. QuestStatusManager클래스의 UpdateObjectiveProgress에서 호출됨.
    public static void CheckCompletion(Quest quest)//퀘스트 종료 전 실제 퀘스트가 완료되었는지 체크.
    {
        if(quest.status != QuestStatus.InProgress)
            return;//퀘스트가 현재 진행 중인 상태가 아닐 경우 종료.

        foreach(var objective in quest.objectives)//퀘스트 달성을 위해 요구되는 목표 수량, 횟수가 현재 수량, 횟수 이상일 경우(즉, 아직 달성 조건 불만족인 경우.) 함수 종료.
        {
            if(objective.currentCount < objective.requiredCount)// ex ) 현재 공격 횟수 40회 < 목표 달성 횟수 50회
                return;
        }
        QuestStatusManager.Instance.CompleteQuest(quest);//퀘스트 진행 중 여부 검증 -> 진행 중이 아니면 퀘스트를 완료 상태로 변경
        //RewardHandler.GiveReward(quest.reward);//보상 지급 함수를 호출.
    }
}
