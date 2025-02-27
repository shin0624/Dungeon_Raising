using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //�÷��̾� ������ ����ȭ�Ǵ� ������ ���� ������Ʈ ����Ʈ�� ������ Ŭ����.
    //������ ĳ����, ����, ���� ������ ������.

    [Header("Database")]
    [SerializeField] private HeroDatabase heroDatabase;//���� ������ ������ �����ͺ��̽�.
    [SerializeField] private SoldierDatabase soldierDatabase;//���� ������ ������ �����ͺ��̽�.
    [SerializeField] private EnemyDatabase enemyDatabase;//���ʹ� ��ü ������ ����� �����ͺ��̽�
    [SerializeField] private BossDatabase bossDatabase;//���� ��ü ������ ����� �����ͺ��̽�.

    private GameObject playerCharacterPrefab;//���� �÷��̾��� ������ ������ �´� ĳ���� ������. Managers�� ������Ʈ�� PlayerCharacterManager.cs���� �����´�.
    private List<GameObject> heroPrefabList = new List<GameObject>();//�÷��̾ ���� ���� �������� ����Ʈ. ���ʿ� �����Ǵ� ������ ����Ʈ[0]. UI�� SelectButton���� ������ ��� �ٲ� �� �ִ�.
    private List<GameObject> soldiersPrefabList = new List<GameObject>();//�÷��̾ ���� ���� ���� ����Ʈ.
    private GameObject enemyUnitPrefab;//���ʹ� �����ͺ��̽����� EnemyInformation�� EnemyType������ �̾ƿ� ������ ���� ������ ����.
    private GameObject bossUnitPrefab;//���� �����ͺ��̽����� BossInformation�� BossType������ �̾ƿ� ������ ���� ������ ����.
    private PlayerCharacterManager playerCharacterManager;//�÷��̾� ĳ���͵��� ����Ʈ�� ����ִ� Ŭ���� �ν��Ͻ�.

    public GameObject GetPlayerCharacter()//�÷��̾��� ������ ������ �´� ĳ���� �������� �������� �޼���.
    {
        if(playerCharacterManager==null)
        {
            playerCharacterManager = Managers.Instance.GetComponent<PlayerCharacterManager>();
            Debug.Log("PlayerCharacterManager is initialized.");
        }
        playerCharacterPrefab = playerCharacterManager.SetCharacterProfile();//������ ������ �´� �÷��̾� ĳ���͸� �����Ѵ�.
        Debug.Log($"playerCharacter is {playerCharacterPrefab.name}");

        return playerCharacterPrefab;//���ӿ�����Ʈ Ÿ������ �����Ͽ�, �÷��̾� ������ ����ȭ�� �÷��̾� ĳ���� �������� �ٸ� ��ũ��Ʈ���� ���� �����ϰ� �Ѵ�.
    }

    public List<GameObject> GetHeroUnitList()//�÷��̾ ������ ���� ������ ���� DB���� ������ �� heroPrefabList�� �����ϴ� �޼���.
    {
        if(heroDatabase!=null)
        {
            //HeroInformationSystem ��ü�� �����ϰ�, �� ��ü���� GameObject������ �̾ƿͼ� heroPrefabList�� �־�� �Ѵ�.
            List<HeroInformation> heroInfo = heroDatabase.heroInformationList.FindAll(i => i.heroRaise.ToString() == PlayerInfo.Instance.GetPlayerRace());//���� DB ���� ���� �� �÷��̾ ������ ������ ���� �̸��� ������ ������ �̾ƿͼ� ����Ʈ�� �����Ѵ�.
            if(heroInfo==null)
            {
                Debug.LogError("No heroes corresponded to the selected race.");
            }
            heroPrefabList.Clear();//�ʱ� ����� ������ ����Ʈ �ʱ�ȭ.

            foreach(HeroInformation unit in heroInfo)
            {
                if(unit.heroPrefab!=null)//db���� ������ ���� ���� ������ ����Ʈ�� ��ȸ�ϸ鼭 �������� �ִ� ���� ������ ã�´�. ���� �̸� �غ�� heroPrefabList�� �����Ѵ�.
                {
                    heroPrefabList.Add(unit.heroPrefab);
                    Debug.Log($"{unit.heroName} added to heroPrefabList!");
                }
            }
        }
        else
        {
            Debug.LogWarning("HeroDatabase is Null");
        }
        return heroPrefabList;// ���ӿ�����Ʈ ����Ʈ Ÿ������ ����Ÿ���� �����Ͽ�, �ϼ��� ���� ������ ����Ʈ�� �ٸ� ��ũ��Ʈ���� ���� �����ϵ��� �Ѵ�.
    } 

    public List<GameObject> GetSoliderList()//soldierInformation�� GameObjectŸ�� �������� soldierPrefabList�� �ְ� ���ʷ� �����ϴ� �޼���.
    {
        if(soldierDatabase!=null)
        {
            List<SoldierInformation> soldierInfo = soldierDatabase.soldierInformationList.FindAll(i => i.soldierPrefab!=null);//���� �������� �����ϴ� ���������� ã�Ƽ� ����Ʈ�� ����.
            soldiersPrefabList.Clear();

            foreach(SoldierInformation unit in soldierInfo)
            {
                soldiersPrefabList.Add(unit.soldierPrefab);//ã�� ���� �������� ����Ʈ�� ����.
                Debug.Log($"{unit.soldierType} added to soldierPrefabList!");
            }
        }
        else
        {
            Debug.LogWarning("SoldierDatabase is Null.");
        }
        return soldiersPrefabList;
    }

    public GameObject GetEnemyUnit(EnemyType enemyType)//EnemyInformation�� EnemyType ������ ��ġ�ϴ� �������� �����ϴ� �޼���.
    {
        if(enemyDatabase!=null)
        {
            enemyUnitPrefab = enemyDatabase.enemyInformationList.Find( i => i.enemyType == enemyType).enemyPrefab;//�Ű������� �־��� ���ʹ� Ÿ�԰� ������ Ÿ���� ���ʹ� ��ü�� ã�Ƽ� ����.
            Debug.Log($"{enemyUnitPrefab.name} added to enemyUnitPrefab!");
        }
        else
        {
            Debug.LogWarning("EnemyDatabase is Null");
        }
        return enemyUnitPrefab;
    }

    public GameObject GetBossUnit(BossType bossType)//BossInformation�� BossType ������ ��ġ�ϴ� �������� �����ϴ� �޼���.
    {
        if(bossDatabase!=null)
        {
            bossUnitPrefab = bossDatabase.bossInformationList.Find(i => i.bossType == bossType).bossPrefab;
            Debug.Log($"{bossUnitPrefab.name} added to bossUnitPrefab!");
        }
        else
        {
            Debug.LogWarning("BossDatabase is Null");
        }
        return bossUnitPrefab;
    }
}
