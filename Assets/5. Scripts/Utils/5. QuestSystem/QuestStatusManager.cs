using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStatusManager : MonoBehaviour
{
    //����Ʈ ���� ���� ���� Ŭ����(����/�Ϸ�). �̱������� �����Ͽ� Managers��ü�� ���δ�. ����Ʈ ui���� ����Ʈ ������ư Ŭ�� ��AcceptQuest() ȣ��, �Ϸ� �� CompleteQuest()ȣ��.

    private static QuestStatusManager _instance;
    public static QuestStatusManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // �ڵ� ����
                GameObject obj = GameObject.Find("Managers");
                _instance = obj.AddComponent<QuestStatusManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private List<Quest> activeQuests = new();//QuestŸ�� ����Ʈ���� -> ���� Ȱ��ȭ�� ����Ʈ �ν��Ͻ��� ����.
    public static event Action<Quest> OnQuestUpdated;//����Ʈ ���൵ ���� �� UI�� �ǽð� �ݿ��� ���� �̺�Ʈ.

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

        OnQuestUpdated?.Invoke(quest);//���൵ ������Ʈ �� �̺�Ʈ ����

        QuestValidator.CheckCompletion(quest);//����Ʈ ���൵�� currentCount���� requiredCount���� ���� �ϴ��� �����Ѵ�. ���� �Ϸ� �� ������ �����Ѵ�.
    }

    public string GetQuestStatus(Quest quest)//�ܺο��� ����Ʈ ���� ���൵�� ��ȯ�� �޼���. QuestManager�� ���� �����Ѵ�.
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
