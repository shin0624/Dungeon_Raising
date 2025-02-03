using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestValidator
{
    //����Ʈ ���� Ŭ����. �Ϸ� ���¸� �����ϰ�, ���� ���� �Լ��� ȣ���Ѵ�. QuestStatusManagerŬ������ UpdateObjectiveProgress���� ȣ���.
    public static void CheckCompletion(Quest quest)//����Ʈ ���� �� ���� ����Ʈ�� �Ϸ�Ǿ����� üũ.
    {
        if(quest.status != QuestStatus.InProgress)
            return;//����Ʈ�� ���� ���� ���� ���°� �ƴ� ��� ����.

        foreach(var objective in quest.objectives)//����Ʈ �޼��� ���� �䱸�Ǵ� ��ǥ ����, Ƚ���� ���� ����, Ƚ�� �̻��� ���(��, ���� �޼� ���� �Ҹ����� ���.)  
        {
            if(objective.currentCount < objective.requiredCount)
                return;
        }
    }
}
