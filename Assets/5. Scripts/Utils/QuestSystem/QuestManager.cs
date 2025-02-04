using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //퀘스트 시스템의 진입점 역할을 수행하는 클래스. 외부 시스템에서 퀘스트와 상호작용하기 위한 간소화된 인터페이스를 제공한다.
    //OOP 원칙 중 SRP에 따라 실제 로직은 다른 클래스에 위임하고 본 클래스에서는 호출만 중계
    //-->Facade패턴으로 작성하여, 외부에서 퀘스트 관련 작업을 호출할 때 QuestManager를 단일 접근점으로 사용하도록 함. 이렇게 해서 외부시스템은 QuestValidator, QuestStatusManager 등 내부 클래스를 직접 알 필요 없어짐.
    //--> 메서드를 static으로 선언하여, QuestManager.AcceptQuest()형태로 어디서든 바로 호출할 수 있게 함. 

    public static void AcceptQuest(Quest quest) => QuestStatusManager.Instance.AcceptQuest(quest);//퀘스트 수락 메서드 --> 퀘스트 진행도 상태가 Available일 경우 수락. 중복 수락x
    public static void UpdateQuestProgress(Quest quest, int objectiveIndex, int amount) => QuestStatusManager.Instance.UpdateObjectiveProgress(quest, objectiveIndex, amount);//퀘스트 진행도 업데이트 메서드
    //quest : 대상 퀘스트, objectiveIndex : 업데이트할 목표 인덱스, amount : 증가 수량 또는 횟수
    public static void CompleteQuest(Quest quest) => QuestStatusManager.Instance.CompleteQuest(quest);
    public static string GetQuestStatus(Quest quest) => QuestStatusManager.Instance.GetQuestStatus(quest);//외부에서 현재 퀘스트 상태에 접근할 때 사용하는 메서드.
    /*
       @퀘스트 시스템 설계 목적

        - 클래스 별 책임 분리
            QuestManager : 시스템 호출 중계
            QuestStatusManager : 상태 변경 로직 선언
            QuestValidator : 완료 조건 검증
            RewardHandler : 보상 처리
        
        - 확장성
            새로운 기능 추가 시 QuestManager 를 수정하지 않고, 하위 클래스만 확장
            ex) public static void AcceptQuest(Quest quest) {
                                    QuestStateManager.Instance.AcceptQuest(quest);
                                    SaveSystem.SaveQuestProgress(quest); // 새로운 기능
                                }

        - 테스트 용의성
                모의 객체를 생성해서 단위테스트 가능
                ex) [Test]
                    public void AcceptQuestTest() {
                        var mockQuest = new Mock<Quest>();
                        QuestManager.AcceptQuest(mockQuest.Object);
                        Assert.AreEqual(QuestStatus.InProgress, mockQuest.Object.status);
                    }

        @ 실제 게임에서의 사용 흐름
            퀘스트 수령) UI -> QuestManager의 AcceptQuest(quest) 호출 -> QuestStatusManager의 AcceptQuest(quest)로 전달 -> Quest(스크립터블 오브젝트)의 status = InProgress로 변경
            퀘스트 완료) QuestStatusManager의 UpdateObjectiveProgress()메서드에서 QuestValidator.CheckCompletion(quest) 호출 -> 검증 조건 통과 -> QuestStatusManager의 CompleteQuest(quest)가 호출됨.
            보상 처리 ) CheckCompletion(quest)메서드에서 검증 -> 상태 변경 -> 보상지급 으로 이어지도록 호출함.
    */
}
