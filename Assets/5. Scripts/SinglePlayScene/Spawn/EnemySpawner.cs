using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    // SinglePlayScene���� ���ʹ� �ν��Ͻ��� �����ϴ� Ŭ����. ���� ���� ��ũ���ͺ� ������Ʈ�� ����Ѵ�.
    // DungeonScene���� Trigger�̺�Ʈ�� ���� SinglePlayScene���� ������ ��, DungeonScene�� DungeonInfoDeliver.cs�� Set__Info()�� ���� ���޹��� ���ʹ�, ���� Ÿ���� �����Ͽ� Ÿ�Կ� �´� �������� �ν��Ͻ��Ѵ�.
    // ��ǥ Ž�� �� �ν��Ͻ��� AutoSpawn__.cs�� ���� ������ ����Ѵ�. ���ʹ̵��� ���� �Է��� ���� ��ǥ �̵��� �Ұ��ϹǷ�, ��ǥ �̵� �޼���� �������� �ʴ´�.

    [SerializeField] private UnitManager unitManager;
    [SerializeField] private Tilemap enemyTilemap;
    [SerializeField] private Tilemap bossTilemap;
    [SerializeField] private Transform enemyPrefabParent;
    [SerializeField] private Transform bossPrefabParent;
    private SingleDungeonInfoRepository singleDungeonInfoRepository;

    //--------------------------------------------------------���ʹ� ������ ����------------------------------------------------------------------------------
    private Queue<Vector3Int> enemySpawnPositions = new Queue<Vector3Int>();//���ʹ̰� ������ Ÿ���� ��ǥ�� ������ ť.
    private int maxAmount = 10;//���ʹ� ��ȯ ���ѷ�. 
    private int spawnedCount = 0;//���ʹ� ��ȯ ���� �� üũ�� ���� ����.
    private GameObject newEnemy;//�ν��Ͻ�ȭ �� myEnemy.
    private GameObject myEnemy;//UnitManager���� �޾ƿ� ���ʹ� ������ ������Ʈ�� ������ ����.
    //--------------------------------------------------------���� ������ ����------------------------------------------------------------------------------
    private GameObject newBoss;//�ν��Ͻ�ȭ �� myBoss.
    private GameObject myBoss;//UnitManager���� �޾ƿ� ���� ������ ������Ʈ�� ������ ����.
    private Vector3Int bossSpawnPosition = new Vector3Int(0,0,0);//���� ������ ������ Ÿ���� �׸��� ��ǥ.
    private int nowSpawnedBossCount = 0;
    private int maxBossAmount = 1;

    private void OnEnable()
    {
        if(singleDungeonInfoRepository==null)
        {
            singleDungeonInfoRepository = GameObject.Find("Managers").GetComponent<SingleDungeonInfoRepository>();
            Debug.Log("SingleDungeonInfoRepository in Managers is Here!");
        }
        if(unitManager==null)
        {
            unitManager = GameObject.Find("SinglePlaySceneManager").GetComponent<UnitManager>();
        }
    }

    private void Start()//���� Ȱ��ȭ�� �� ȣ���ߴ� FindSpawnPosition()�� ������ ���� Start()�� ����� �� ���� �� �Ͽ�, Start()���� �� �� ȣ���ϴ� ������ �����Ͽ� ���� ������ ����ǵ��� ������.
    {
        FindSpawnPosition();
        GeneratePrefabs();
    }

    private void FindSpawnPosition()
    {   
        ReceiveUnitType();//DungeonScene���� ���� Ÿ�� �޾ƿ���
        FindEnemyPosition();//���ʹ� ���� ��ġ ã��
        FindBossPosition();//���� ���� ��ġ ã��
    }

    private void GeneratePrefabs()// DungeonScene���� �Ѱܹ��� Ÿ�԰� ��ġ�ϴ� Ÿ���� ��ü�� �����ϸ� �ν��Ͻ�ȭ �Ѵ�.
    {
        while(spawnedCount < maxAmount && enemySpawnPositions.Count > 0)
        {
            SpawnEnemys();
        }
        SpawnBoss();
    }

    private void ReceiveUnitType()//Enemy, BossDatabase ��ũ���ͺ� ������Ʈ���� __Information ��ü�� __Type �Ӽ��� ���Ѵ�.
    {
        EnemyType enemyType = ReceiveEnemyType();
        BossType bossType = ReceiveBossType();
        
        myEnemy = unitManager.GetEnemyUnit(enemyType);//�Ѱܹ��� ���ʹ� Ÿ���� �Ű������� �����Ͽ�, �ش� Ÿ���� ���ʹ� ��ü�� �޾ƿ´�.
        myBoss = unitManager.GetBossUnit(bossType);//�Ѱܹ��� ���� Ÿ���� �Ű������� �����Ͽ�, �ش� Ÿ���� ���� ��ü�� �޾ƿ´�.
    }

    private void FindEnemyPosition()// ���ʹ� �������� ��ȯ�� ��ġ ������ Ž���ϴ� �޼���.
    {
        BoundsInt bounds = enemyTilemap.cellBounds;//���ʹ� ���̾��� ��ȿ�� cell ������ ����.
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(enemyTilemap.HasTile(position))
            {
                enemySpawnPositions.Enqueue(position);//���ʹ� ��ȯ�� ���̾�� Ÿ���� �����ϴ� ��ġ�� ��ǥ ���� ť�� ����.
            }
        }
        if(enemySpawnPositions.Count == 0)
        {
             Debug.LogWarning("There is no tile to spawn enemy.");
        }
    }

    private void FindBossPosition()//���� �������� ��ȯ�� ��ġ ������ Ž���ϴ� �޼���. �������� ������ 1��ü ���̹Ƿ�, ã�� �������� �ٷ� ���� ���������� ����.
    {
        bool founded = false;
        BoundsInt bounds = bossTilemap.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(bossTilemap.HasTile(position))
            {
                bossSpawnPosition = position;
                founded = true;
                break;
            }
        }
        if(!founded)
        {
            Debug.LogWarning("There is no tile to spawn Boss.");
        }
    }

    private void SpawnEnemys()//���ʹ� ��ȯ ���� ���θ� üũ�ϰ�, ��ȯ ������ Ÿ�Ͽ� ���ʹ� �������� �ν��Ͻ�ȭ�ϴ� �޼���.
    {
        if(spawnedCount >= maxAmount)//��ȯ ���� ������ ��ȯ�ߴٸ�
        {
            Debug.Log("maxCount of enemy is spawned.");
            return;
        }

        if(enemySpawnPositions.Count > 0)//���ʹ� ��ȯ�� ������ ��ǥ�� �����Ѵٸ�
        {
            Vector3Int spawnTile = enemySpawnPositions.Dequeue();//���ʹ̰� ������ Ÿ�� ��ǥ ť���� ��ǥ���� ������.
            Vector3 worldPosition = enemyTilemap.GetCellCenterWorld(spawnTile);//�ش� Ÿ�� ��ǥ�� �߽� ��ġ�� �����´�.

            newEnemy = Instantiate(myEnemy, worldPosition, Quaternion.identity, enemyPrefabParent);//���ʹ� ���̾��� ���ʹ̵��� �÷��̾� ������ ���ֺ��ƾ� �ϹǷ�, �����̼� ���� �⺻���� ����. myEnemy�� ����� ���ʹ� �������� �ν��Ͻ�ȭ �Ѵ�.
            spawnedCount++;//���� ���ѷ� ī��Ʈ�� ����. 10���� ���� ���� �ߴ�.
        }
        else
        {
            Debug.Log("There is no tile to spawn enemy.");
        }
    }

    private void SpawnBoss()//���� ��ȯ ���� ���θ� üũ�ϰ�, ��ȯ ������ Ÿ�Ͽ� ���� �������� �ν��Ͻ�ȭ �ϴ� �޼���.
    {
        if(nowSpawnedBossCount >= maxBossAmount)
        {
            Debug.Log("maxAmount of Boss is spawned.");
            return;
        }
        if(myBoss==null)
        {
            Debug.LogWarning("myBoss is not initialized.");
            return;
        }
        Vector3Int spawnTile = bossSpawnPosition;//FindBossPosition()���� ã�� ���� ��Ҹ� �����´�.
        Vector3 worldPosition = bossTilemap.GetCellCenterWorld(spawnTile);

        newBoss = Instantiate(myBoss, worldPosition, Quaternion.identity, bossPrefabParent);
        nowSpawnedBossCount++;//������ 1��ü�� ��ȯ�Ǿ�� �ϹǷ� ī��Ʈ�� �÷��� �߰� ��ȯ�� ����.
    }
    private EnemyType ReceiveEnemyType()//DungeonScene���� �Ѱܹ��� ���ʹ� Ÿ���� ����.
    {
        Debug.Log($"Received EnemyType : {singleDungeonInfoRepository.enemyType}");
        return singleDungeonInfoRepository.enemyType;
    }

    private BossType ReceiveBossType()//DungeonScene���� �Ѱܹ��� ���� Ÿ���� ����.
    {
        Debug.Log($"Received BossType : {singleDungeonInfoRepository.bossType}");
        return singleDungeonInfoRepository.bossType;
    }

}
