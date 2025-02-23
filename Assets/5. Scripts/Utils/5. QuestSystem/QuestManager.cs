using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //����Ʈ �ý����� ������ ������ �����ϴ� Ŭ����. �ܺ� �ý��ۿ��� ����Ʈ�� ��ȣ�ۿ��ϱ� ���� ����ȭ�� �������̽��� �����Ѵ�.
    //OOP ��Ģ �� SRP�� ���� ���� ������ �ٸ� Ŭ������ �����ϰ� �� Ŭ���������� ȣ�⸸ �߰�
    //-->Facade�������� �ۼ��Ͽ�, �ܺο��� ����Ʈ ���� �۾��� ȣ���� �� QuestManager�� ���� ���������� ����ϵ��� ��. �̷��� �ؼ� �ܺνý����� QuestValidator, QuestStatusManager �� ���� Ŭ������ ���� �� �ʿ� ������.
    //--> �޼��带 static���� �����Ͽ�, QuestManager.AcceptQuest()���·� ��𼭵� �ٷ� ȣ���� �� �ְ� ��. 

    public static void AcceptQuest(Quest quest) => QuestStatusManager.Instance.AcceptQuest(quest);//����Ʈ ���� �޼��� --> ����Ʈ ���൵ ���°� Available�� ��� ����. �ߺ� ����x
    public static void UpdateQuestProgress(Quest quest, int objectiveIndex, int amount) => QuestStatusManager.Instance.UpdateObjectiveProgress(quest, objectiveIndex, amount);//����Ʈ ���൵ ������Ʈ �޼���
    //quest : ��� ����Ʈ, objectiveIndex : ������Ʈ�� ��ǥ �ε���, amount : ���� ���� �Ǵ� Ƚ��
    public static void CompleteQuest(Quest quest) => QuestStatusManager.Instance.CompleteQuest(quest);
    public static string GetQuestStatus(Quest quest) => QuestStatusManager.Instance.GetQuestStatus(quest);//�ܺο��� ���� ����Ʈ ���¿� ������ �� ����ϴ� �޼���.
    /*
       @����Ʈ �ý��� ���� ����

        - Ŭ���� �� å�� �и�
            QuestManager : �ý��� ȣ�� �߰�
            QuestStatusManager : ���� ���� ���� ����
            QuestValidator : �Ϸ� ���� ����
            RewardHandler : ���� ó��
        
        - Ȯ�强
            ���ο� ��� �߰� �� QuestManager �� �������� �ʰ�, ���� Ŭ������ Ȯ��
            ex) public static void AcceptQuest(Quest quest) {
                                    QuestStateManager.Instance.AcceptQuest(quest);
                                    SaveSystem.SaveQuestProgress(quest); // ���ο� ���
                                }

        - �׽�Ʈ ���Ǽ�
                ���� ��ü�� �����ؼ� �����׽�Ʈ ����
                ex) [Test]
                    public void AcceptQuestTest() {
                        var mockQuest = new Mock<Quest>();
                        QuestManager.AcceptQuest(mockQuest.Object);
                        Assert.AreEqual(QuestStatus.InProgress, mockQuest.Object.status);
                    }

        @ ���� ���ӿ����� ��� �帧
            ����Ʈ ����) UI -> QuestManager�� AcceptQuest(quest) ȣ�� -> QuestStatusManager�� AcceptQuest(quest)�� ���� -> Quest(��ũ���ͺ� ������Ʈ)�� status = InProgress�� ����
            ����Ʈ �Ϸ�) QuestStatusManager�� UpdateObjectiveProgress()�޼��忡�� QuestValidator.CheckCompletion(quest) ȣ�� -> ���� ���� ��� -> QuestStatusManager�� CompleteQuest(quest)�� ȣ���.
            ���� ó�� ) CheckCompletion(quest)�޼��忡�� ���� -> ���� ���� -> �������� ���� �̾������� ȣ����.
    */
}
