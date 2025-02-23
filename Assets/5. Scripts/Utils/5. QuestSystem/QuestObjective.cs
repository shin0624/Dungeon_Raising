using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective //����Ʈ�� ���� ��ǥ�� ����. ���� �����͸� ����
{
    public string objectiveName;//����Ʈ ��ǥ �̸�(~�����ϱ�, ~Ŭ�����ϱ� ��) : UI�� Text�� ���, ���� ���� �������� ��ǥ �ĺ��� ���.
    public int requiredCount;//����Ʈ �޼��� �ʿ��� ���� : ~�� ����, ~ȸ �Ϸ� ��
    public int currentCount;//���� ���� ���� : ���� �����Ȳ�� �����ϴ� ī����. �ν����Ϳ��� ���ܼ� ��Ÿ�� �߿��� ���� ����ǵ��� �ϸ�, QuestManager.UpdateQuestProgress()�� ���ؼ��� ������Ʈ��.
}
