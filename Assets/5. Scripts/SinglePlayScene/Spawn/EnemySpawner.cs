using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    // SinglePlayScene에서 에너미 인스턴스를 스폰하는 클래스. 던전 정보 스크립터블 오브젝트를 사용한다.
    // DungeonScene에서 Trigger이벤트를 통해 SinglePlayScene으로 입장할 때, DungeonScene의 DungeonInfoDeliver.cs의 Set__Info()를 통해 전달받은 에너미, 보스 타입을 수신하여 타입에 맞는 프리팹을 인스턴싱한다.
    // 좌표 탐색 및 인스턴싱은 AutoSpawn__.cs와 동일 로직을 사용한다. 에너미들은 유저 입력을 통한 좌표 이동이 불가하므로, 좌표 이동 메서드는 구현하지 않는다.

    [SerializeField] private UnitManager unitManager;
    [SerializeField] private Tilemap enemyTilemap;
    [SerializeField] private Tilemap bossTilemap;
    [SerializeField] private Transform enemyPrefabParent;
    [SerializeField] private Transform bossPrefabParent;
    private SingleDungeonInfoRepository singleDungeonInfoRepository;

    //--------------------------------------------------------에너미 스폰용 변수------------------------------------------------------------------------------
    private Queue<Vector3Int> enemySpawnPositions = new Queue<Vector3Int>();//에너미가 스폰될 타일의 좌표를 저장할 큐.
    private int maxAmount = 10;//에너미 소환 제한량. 
    private int spawnedCount = 0;//에너미 소환 제한 량 체크를 위한 정수.
    private GameObject newEnemy;//인스턴스화 된 myEnemy.
    private GameObject myEnemy;//UnitManager에서 받아온 에너미 프리팹 오브젝트를 저장할 변수.
    //--------------------------------------------------------보스 스폰용 변수------------------------------------------------------------------------------
    private GameObject newBoss;//인스턴스화 된 myBoss.
    private GameObject myBoss;//UnitManager에서 받아온 보스 프리팹 오브젝트를 저장할 변수.
    private Vector3Int bossSpawnPosition = new Vector3Int(0,0,0);//보스 유닛이 스폰될 타일의 그리드 좌표.
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

    private void Start()//씬이 활성화될 때 호출했던 FindSpawnPosition()이 끝나기 전에 Start()가 실행될 수 있을 듯 하여, Start()에서 둘 다 호출하는 것으로 변경하여 실행 순서가 보장되도록 변경함.
    {
        FindSpawnPosition();
        GeneratePrefabs();
    }

    private void FindSpawnPosition()
    {   
        ReceiveUnitType();//DungeonScene에서 유닛 타입 받아오기
        FindEnemyPosition();//에너미 스폰 위치 찾기
        FindBossPosition();//보스 스폰 위치 찾기
    }

    private void GeneratePrefabs()// DungeonScene에서 넘겨받은 타입과 일치하는 타입의 객체가 존재하면 인스턴스화 한다.
    {
        while(spawnedCount < maxAmount && enemySpawnPositions.Count > 0)
        {
            SpawnEnemys();
        }
        SpawnBoss();
    }

    private void ReceiveUnitType()//Enemy, BossDatabase 스크립터블 오브젝트에서 __Information 객체의 __Type 속성을 비교한다.
    {
        EnemyType enemyType = ReceiveEnemyType();
        BossType bossType = ReceiveBossType();
        
        myEnemy = unitManager.GetEnemyUnit(enemyType);//넘겨받은 에너미 타입을 매개변수로 전달하여, 해당 타입의 에너미 객체를 받아온다.
        myBoss = unitManager.GetBossUnit(bossType);//넘겨받은 보스 타입을 매개변수로 전달하여, 해당 타입의 보스 객체를 받아온다.
    }

    private void FindEnemyPosition()// 에너미 프리팹을 소환할 위치 정보를 탐색하는 메서드.
    {
        BoundsInt bounds = enemyTilemap.cellBounds;//에너미 레이어의 유효한 cell 영역을 저장.
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(enemyTilemap.HasTile(position))
            {
                enemySpawnPositions.Enqueue(position);//에너미 소환용 레이어에서 타일이 존재하는 위치만 좌표 저장 큐에 삽입.
            }
        }
        if(enemySpawnPositions.Count == 0)
        {
             Debug.LogWarning("There is no tile to spawn enemy.");
        }
    }

    private void FindBossPosition()//보스 프리팹을 소환할 위치 정보를 탐색하는 메서드. 던전에서 보스는 1개체 뿐이므로, 찾은 포지션을 바로 스폰 포지션으로 설정.
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

    private void SpawnEnemys()//에너미 소환 가능 여부를 체크하고, 소환 가능한 타일에 에너미 프리팹을 인스턴스화하는 메서드.
    {
        if(spawnedCount >= maxAmount)//소환 제한 량까지 소환했다면
        {
            Debug.Log("maxCount of enemy is spawned.");
            return;
        }

        if(enemySpawnPositions.Count > 0)//에너미 소환이 가능한 좌표가 존재한다면
        {
            Vector3Int spawnTile = enemySpawnPositions.Dequeue();//에너미가 스폰될 타일 좌표 큐에서 좌표값을 뺴낸다.
            Vector3 worldPosition = enemyTilemap.GetCellCenterWorld(spawnTile);//해당 타일 좌표의 중심 위치를 가져온다.

            newEnemy = Instantiate(myEnemy, worldPosition, Quaternion.identity, enemyPrefabParent);//에너미 레이어의 에너미들은 플레이어 유닛을 마주보아야 하므로, 로테이션 값을 기본으로 유지. myEnemy에 저장된 에너미 프리팹을 인스턴스화 한다.
            spawnedCount++;//스폰 제한량 카운트를 증가. 10까지 가면 스폰 중단.
        }
        else
        {
            Debug.Log("There is no tile to spawn enemy.");
        }
    }

    private void SpawnBoss()//보스 소환 가능 여부를 체크하고, 소환 가능한 타일에 보스 프리팹을 인스턴스화 하는 메서드.
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
        Vector3Int spawnTile = bossSpawnPosition;//FindBossPosition()에서 찾은 스폰 장소를 가져온다.
        Vector3 worldPosition = bossTilemap.GetCellCenterWorld(spawnTile);

        newBoss = Instantiate(myBoss, worldPosition, Quaternion.identity, bossPrefabParent);
        nowSpawnedBossCount++;//보스는 1개체만 소환되어야 하므로 카운트를 올려서 추가 소환을 제한.
    }
    private EnemyType ReceiveEnemyType()//DungeonScene에서 넘겨받은 에너미 타입을 리턴.
    {
        Debug.Log($"Received EnemyType : {singleDungeonInfoRepository.enemyType}");
        return singleDungeonInfoRepository.enemyType;
    }

    private BossType ReceiveBossType()//DungeonScene에서 넘겨받은 보스 타입을 리턴.
    {
        Debug.Log($"Received BossType : {singleDungeonInfoRepository.bossType}");
        return singleDungeonInfoRepository.bossType;
    }

}
