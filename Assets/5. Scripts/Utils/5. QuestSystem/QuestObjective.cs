using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective //퀘스트의 개별 목표를 관리. 순수 데이터만 관리
{
    public string objectiveName;//퀘스트 목표 이름(~공격하기, ~클리어하기 등) : UI의 Text에 사용, 조건 검증 로직에서 목표 식별에 사용.
    public int requiredCount;//퀘스트 달성에 필요한 수량 : ~번 공격, ~회 완료 등
    public int currentCount;//현재 진행 수량 : 현재 진행상황을 추적하는 카운터. 인스펙터에서 숨겨서 런타임 중에만 값이 변경되도록 하며, QuestManager.UpdateQuestProgress()를 통해서만 업데이트됨.
}
