using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Quest/New Quest")]
public class Quest : ScriptableObject
{
    //퀘스트 데이터를 저장하기 위한 스크립터블 오브젝트

    [Header("Basic Info")]
    public string questID;//퀘스트의 고유한 id
    public string questName;//퀘스트 이름
    public QuestDate questDateType;//일일, 주간 퀘스트 구분

    [Header("Progress")]
    public QuestStatus status;//퀘스트 진행 상태
    public QuestObjective[] objectives;//퀘스트 성공 조건(목표)

    [Header("Reward")]
    public QuestReward reward;//퀘스트 보상
}
