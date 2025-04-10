using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInfoDeliver : MonoBehaviour
{
    // DungeonScene�� DungeonSceneManager�� �Ҵ��ϴ� Ŭ����. DungeonInfoPrintToPanel.cs�� SynchronizeDungeonInfo(), ClearDungeonInfo() ȣ�� �� SingleDungeonInfoRepository.cs���� ���������� �Ѱ��ش�.
    private SingleDungeonInfoRepository singleDungeonInfoRepository;

    private void Start()
    {
        if(singleDungeonInfoRepository==null)
        {
            singleDungeonInfoRepository = GameObject.Find("Managers").GetComponent<SingleDungeonInfoRepository>();
            Debug.Log("SingleDungeonInfoRepository in Managers is Here!");
        }
    }

    public void SetEnemyInfo(DungeonInformation dungeonInformation )//Ʈ���� ���͵� ������ ���ʹ� ������ �����Ѵ�.
    {
        singleDungeonInfoRepository.enemyType = dungeonInformation.enemyType;
        Debug.Log($"Enemy Type in this dungeon : {singleDungeonInfoRepository.enemyType}");
    }

    public void SetBossInfo(DungeonInformation dungeonInformation)//Ʈ���� ���͵� ������ ���� ������ �����Ѵ�.
    {
        singleDungeonInfoRepository.bossType = dungeonInformation.bossType;
        Debug.Log($"Boss Type in this dungeon : {singleDungeonInfoRepository.bossType}");
    }

    public void SetDungeonInfo(DungeonInformation dungeonInformation)// Ʈ���� ���͵� ������ id�� �����´�.
    {
        singleDungeonInfoRepository.dungeonInformation.dungeonID = dungeonInformation.dungeonID;
        Debug.Log($"DungeonID in this dungeon : {dungeonInformation.dungeonID}");
    }

    public void ClearEnemyInfo()//������ �������� �ʰ� �˾��г��� �ݾ��� ���, ���õǾ��� ���ʹ� ������ ���������� �ٲ۴�.
    {
        singleDungeonInfoRepository.enemyType = EnemyType.Empty;
    }

    public void ClearBossInfo()//������ �������� �ʰ� �˾��г��� �ݾ��� ���, ���õǾ��� ���� ������ ���������� �ٲ۴�.
    {
         singleDungeonInfoRepository.bossType = BossType.Empty;
    }

    public void ClearDungeonInfo()//������ �������� �ʰ� �˾��г��� �ݾ��� ���, ���õǾ��� ����id ������ empty�� �ٲ۴�.
    {
        singleDungeonInfoRepository.dungeonInformation.dungeonID = "";
    }
}
