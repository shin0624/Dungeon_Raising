using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Quest/New Quest")]
public class Quest : ScriptableObject
{
    //����Ʈ �����͸� �����ϱ� ���� ��ũ���ͺ� ������Ʈ

    [Header("Basic Info")]
    public string questID;//����Ʈ�� ������ id
    public string questName;//����Ʈ �̸�
    public QuestDate questDateType;//����, �ְ� ����Ʈ ����

    [Header("Progress")]
    public QuestStatus status;//����Ʈ ���� ����
    public QuestObjective[] objectives;//����Ʈ ���� ����(��ǥ)

    [Header("Reward")]
    public QuestReward reward;//����Ʈ ����
}
