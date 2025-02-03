using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatusManager : MonoBehaviour
{
    //����Ʈ ���� ���� ���� Ŭ����(����/�Ϸ�). �̱������� �����Ͽ� Managers��ü�� ���δ�. ����Ʈ ui���� ����Ʈ ������ư Ŭ�� ��AcceptQuest() ȣ��, �Ϸ� �� CompleteQuest()ȣ��.

    private static QuestStatusManager _instance;
    public static QuestStatusManager Instance => _instance;

    private List<Quest> activeQuests = new();//QuestŸ�� ����Ʈ���� -> ���� Ȱ��ȭ�� ����Ʈ �ν��Ͻ��� ����.
    private void Awake() => _instance = this;//�ν��Ͻ� �ʱ�ȭ

    public void AcceptQuest(Quest quest)//����Ʈ ���� �޼���
    {
        if(quest.status != QuestStatus.Available)
            return;//���� ���� ���� ����Ʈ�� �ƴ� ��� �Լ� ����.
        
        quest.status = QuestStatus.InProgress;//���� ���� -> ���� ������ ���� ����
        activeQuests.Add(quest);//Ȱ��ȭ�� ����Ʈ ����Ʈ�� �߰�.
        Debug.Log($"Quest Accected : {quest.questName}");//����Ʈ ���� �α׷� ǥ��.
    }

    public void CompleteQuest(Quest quest)//����Ʈ �Ϸ�
    {
        if(quest.status != QuestStatus.InProgress)
            return;//����Ʈ�� ���� ���� ���� �ƴ϶�� �Լ� ����. ���� ���� ���� ����Ʈ�� �Ϸ�� �� �ִ�.
        
        quest.status = QuestStatus.Completed;//����Ʈ�� �Ϸ� ���·� �ٲ۴�.
        activeQuests.Remove(quest);//Ȱ��ȭ ����Ʈ ��Ͽ��� ����.
        Debug.Log($"Quest Completed : {quest.questName}");
    }

    public void UpdateObjectiveProgress(Quest quest, int objectiveIndex, int amount)//����Ʈ �����Ȳ ������Ʈ �޼���.
    {   
        if(quest.status != QuestStatus.InProgress)
            return;
        
        quest.objectives[objectiveIndex].currentCount +=amount;//����Ʈ �޼��� �ʿ��� �ּ� Ƚ��, ���� ���� amout���� ���� ������Ʈ.
        QuestValidator.CheckCompletion(quest);//����Ʈ ���� ����.
    }
}
