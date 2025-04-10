using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInfoDeliver : MonoBehaviour
{
    // DungeonScene의 DungeonSceneManager에 할당하는 클래스. DungeonInfoPrintToPanel.cs의 SynchronizeDungeonInfo(), ClearDungeonInfo() 호출 시 SingleDungeonInfoRepository.cs에게 던전정보를 넘겨준다.
    private SingleDungeonInfoRepository singleDungeonInfoRepository;

    private void Start()
    {
        if(singleDungeonInfoRepository==null)
        {
            singleDungeonInfoRepository = GameObject.Find("Managers").GetComponent<SingleDungeonInfoRepository>();
            Debug.Log("SingleDungeonInfoRepository in Managers is Here!");
        }
    }

    public void SetEnemyInfo(DungeonInformation dungeonInformation )//트리거 엔터된 던전의 에너미 정보를 세팅한다.
    {
        singleDungeonInfoRepository.enemyType = dungeonInformation.enemyType;
        Debug.Log($"Enemy Type in this dungeon : {singleDungeonInfoRepository.enemyType}");
    }

    public void SetBossInfo(DungeonInformation dungeonInformation)//트리거 엔터된 던전의 보스 정보를 세팅한다.
    {
        singleDungeonInfoRepository.bossType = dungeonInformation.bossType;
        Debug.Log($"Boss Type in this dungeon : {singleDungeonInfoRepository.bossType}");
    }

    public void SetDungeonInfo(DungeonInformation dungeonInformation)// 트리거 엔터된 던전의 id를 가져온다.
    {
        singleDungeonInfoRepository.dungeonInformation.dungeonID = dungeonInformation.dungeonID;
        Debug.Log($"DungeonID in this dungeon : {dungeonInformation.dungeonID}");
    }

    public void ClearEnemyInfo()//던전에 입장하지 않고 팝업패널을 닫았을 경우, 세팅되었던 에너미 정보를 더미정보로 바꾼다.
    {
        singleDungeonInfoRepository.enemyType = EnemyType.Empty;
    }

    public void ClearBossInfo()//던전에 입장하지 않고 팝업패널을 닫았을 경우, 세팅되었던 보스 정보를 더미정보로 바꾼다.
    {
         singleDungeonInfoRepository.bossType = BossType.Empty;
    }

    public void ClearDungeonInfo()//던전에 입장하지 않고 팝업패널을 닫았을 경우, 세팅되었던 던전id 정보를 empty로 바꾼다.
    {
        singleDungeonInfoRepository.dungeonInformation.dungeonID = "";
    }
}
